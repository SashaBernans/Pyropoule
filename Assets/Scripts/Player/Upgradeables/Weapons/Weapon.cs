using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public abstract float ProjectileSpeed { get; set; }
    public abstract float AttackSpeed { get; set; }
    public abstract int Damage { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpgradeProjectileSpeed(int percent)
    {
        ProjectileSpeed += ProjectileSpeed * percent / 100;
    }

    public void UpgradeAttackSpeed(int percent)
    {
        AttackSpeed += AttackSpeed * percent / 100;
    }

    public void UpgradeDamage(int percent)
    {
        Damage += Damage * percent / 100;
    }

    virtual public void UpgradeArea(int percent)
    {
        Vector2 current = transform.localScale;
        Vector2 newScale = current+(current * percent/100);
        transform.localScale = newScale;
        //print(current);
        //print(newScale);
    }
}
