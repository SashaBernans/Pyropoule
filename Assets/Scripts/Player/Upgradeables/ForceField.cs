using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour, IUpgradeable
{

    private int level = 1;
    private string upgradeTitle = "Vaccuum level ";
    private string upgradeText = "Increase pick up range by 20%";
    public string GetUpgradeText()
    {
        return upgradeText;
    }

    public string GetUpgradeTitle()
    {
        return upgradeTitle + level.ToString();
    }

    public void Upgrade()
    {
        print("upgrade pick up range");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
