using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradesManager : MonoBehaviour
{
    private List<IUpgradeable> upgradeables = new List<IUpgradeable>();

    public List<IUpgradeable> Upgradeables { get => upgradeables;}

    // Start is called before the first frame update
    void Start()
    {
        HealthSystem.Instance.playerUpgradesManager = this;
        upgradeables.Add(HealthSystem.Instance);
        AddUpgradeables(GetComponentsInChildren<IUpgradeable>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUpgradeable(IUpgradeable upgradeable)
    {
        upgradeables.Add(upgradeable);
    }

    public void AddUpgradeables(IUpgradeable[] upgradeables)
    {
        foreach(IUpgradeable upgrade in upgradeables)
        {
            this.upgradeables.Add(upgrade);
        }
    }
}
