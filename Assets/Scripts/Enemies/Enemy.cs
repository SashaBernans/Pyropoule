using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable,IScaleable
{
    [SerializeField] public abstract float enemyBaseHealth { get; }
    public float health { get; set; }
    public float maxHealth { get; set; }
    public HealthBarManager healthBar;

    public PopUpParent damagePopUp;

    public AssetRecycler assetRecycler;

    public SpriteRenderer spriteRenderer;

    public GameManager gameManager;

    public Material defaultMaterial;


    // Start is called before the first frame update
    virtual public void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        damagePopUp = GetComponentInChildren<PopUpParent>();
        healthBar = GetComponentInChildren<HealthBarManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = GameManager.Instance;
        defaultMaterial = spriteRenderer.material;
        health = enemyBaseHealth;
        maxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healToMaxHealth()
    {
        health = maxHealth;
    }

    public void multiplyMaxHealth(float multiplier)
    {
        maxHealth = multiplier * enemyBaseHealth;
    }

    virtual public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            releaseExp();
            gameObject.SetActive(false);
            health = maxHealth;
        }
        else
        {
            healthBar.TakeDamage(damage / health * 100);
            health = health - damage;
            StartCoroutine(Flash());
        }
        damagePopUp.PopUp(damage);
    }

    private void releaseExp()
    {
        GameObject exp =assetRecycler.ExpPool.Find(p => !p.activeInHierarchy);
        exp.transform.position = gameObject.transform.position;
        exp.transform.position = new Vector2(exp.transform.position.x, exp.transform.position.y+0.5f);
        exp.SetActive(true);
    }

    virtual public void OnDisable()
    {
        if (spriteRenderer !=null)
        {
            spriteRenderer.material = defaultMaterial;
        }
    }

    IEnumerator Flash()
    {
        spriteRenderer.material = gameManager.FlashMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.material = defaultMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.material = gameManager.FlashMaterial;
        yield return new WaitForSeconds(0.125f);
        spriteRenderer.material = defaultMaterial;
    }
}
