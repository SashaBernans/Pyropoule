using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turkey : MonoBehaviour, IDamageable
{
    [SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float health;

    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private GameObject laser;
    private HealthBarManager healthBar;
    private float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        laser = assetRecycler.LaserPool.Find(p => !p.activeInHierarchy);
        laser.transform.position = transform.position;
        laser.SetActive(true);
        healthBar = GetComponentInChildren<HealthBarManager>();
    }
    private void OnEnable()
    {
        if (laser!=null)
        {
            laser.transform.position = transform.position;
            laser.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            //gameObject.SetActive(false);
        }
    }

    //Disable laser when turkey dies
    private void OnDisable()
    {
        if (laser != null)
        {
            laser.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health - damage <= 0)
        {
            gameObject.SetActive(false);
            health = maxHealth;
            healthBar.HealToMax();
        }
        else
        {
            healthBar.TakeDamage(damage / health * 100);
            health = health - damage;
        }
    }
}
