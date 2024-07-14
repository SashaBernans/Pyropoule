using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable,IScaleable
{
    [SerializeField] public abstract float enemyBaseHealth { get; }
    public float health { get; set; }
    public float maxHealth { get; set; }
    public HealthBarManager healthBar { get; set; }

    public PopUpManager damagePopUp;


    // Start is called before the first frame update
    virtual public void Start()
    {
        damagePopUp = GetComponentInChildren<PopUpManager>();
        damagePopUp.gameObject.SetActive(false);
        healthBar = GetComponentInChildren<HealthBarManager>();
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
            gameObject.SetActive(false);
            health = maxHealth;
        }
        else
        {
            healthBar.TakeDamage(damage / health * 100);
            health = health - damage;
        }
    }
}
