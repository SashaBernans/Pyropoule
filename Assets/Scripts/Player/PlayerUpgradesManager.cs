using Assets.Scripts.Player.Upgradeables;
using Assets.Scripts.Player.Upgradeables.GlobalUpgrades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerUpgradesManager : MonoBehaviour
{
    private List<IUpgradeable> upgradeables = new List<IUpgradeable>();
    private List<Weapon> weapons = new List<Weapon>();

    public List<IUpgradeable> Upgradeables { get => upgradeables;}

    // Start is called before the first frame update
    void Start()
    {
        HealthSystem.Instance.playerUpgradesManager = this;
        upgradeables.Add(HealthSystem.Instance);
        AddUpgradeables(GetComponentsInChildren<IUpgradeable>());

        weapons = GetWeapons(upgradeables);
        AddGlobalUpgrades();

        PlayerBaseWeapon b = GetComponent<PlayerBaseWeapon>();
        upgradeables.Add(b);
        weapons.Add(b);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddGlobalUpgrades()
    {
        ProjectileSpeedUpgrade projectileSpeedUpgrade = new ProjectileSpeedUpgrade(weapons);
        upgradeables.Add(projectileSpeedUpgrade);
        AreaUpgrade areaUpgrade = new AreaUpgrade(weapons);
        upgradeables.Add(areaUpgrade);
        AttackSpeedUpgrade attackSpeed = new AttackSpeedUpgrade(weapons);
        upgradeables.Add(attackSpeed);
        DamageUpgrade damageUpgrade = new DamageUpgrade(weapons);
        upgradeables.Add(damageUpgrade);
    }

    public void AddUpgradeables(IUpgradeable[] upgradeables)
    {
        foreach(IUpgradeable upgrade in upgradeables)
        {
            this.upgradeables.Add(upgrade);
        }
    }

    private List<Weapon> GetWeapons(List<IUpgradeable> upgradeables)
    {
        if (upgradeables == null)
        {
            throw new ArgumentNullException(nameof(upgradeables));
        }

        // Use LINQ to filter the upgradeables list and return only those that are of type Weapon
        List<Weapon> weapons = upgradeables.OfType<Weapon>().ToList();

        return weapons;
    }
}
