using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pyropoule : MonoBehaviour, IDamageable
{
    [SerializeField] private AssetRecycler assetRecycler;
    [SerializeField] private float fireRate;
    private float health;

    private float maxHealth;
    [SerializeField] private float baseHealth;
    private HealthBarManager healthBar;
    private SpriteRenderer spriteRenderer;
    private AudioSource audioSource;
    private bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        assetRecycler = AssetRecycler.Instance;
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthBar = GetComponentInChildren<HealthBarManager>();
        canShoot = true;
        health = baseHealth;
        maxHealth = health;
    }

    private void OnEnable()
    {
        canShoot = true;
        if (GameManager.Instance!=null)
        {
            if(healthBar != null)
            {
                healthBar.HealToMax();
            }
            HandleScaling();
        }
    }

    private void HandleScaling()
    {
        if (GameManager.Instance.height > 20)
        {
            float multiplier = GameManager.Instance.height / 20;
            maxHealth = multiplier * baseHealth;
        }

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ManageLookingDirection(collision.gameObject);
            if (canShoot)
            {
                Shoot(collision.gameObject);
                StartCoroutine(ShootDelay());
            }
        }
    }

    IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    private void Shoot(GameObject target)
    {
        GameObject projectile = assetRecycler.PyropouleProjectilePool.Find(p => !p.activeInHierarchy);
        projectile.SetActive(true);
        projectile.transform.position = transform.position;
        audioSource.PlayOneShot(SoundManager.Instance.ShootFlame);
        projectile.GetComponent<ProjectileMovement>().Target = target.transform.position;
        canShoot = false;
    }

    private void ManageLookingDirection(GameObject target)
    {
        Vector2 toTarget = target.transform.position - transform.position;
        float dotProduct = Vector2.Dot(transform.right, toTarget.normalized);
        if (dotProduct > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (dotProduct < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        if (health -damage <= 0)
        {
            gameObject.SetActive(false);
            healthBar.HealToMax();
            health = maxHealth;
        }
        else
        {
            healthBar.TakeDamage(damage / health * 100);
            health = health - damage;
        }
    }
}
