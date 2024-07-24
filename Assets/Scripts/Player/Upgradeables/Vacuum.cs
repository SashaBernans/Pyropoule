using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vacuum : MonoBehaviour, IUpgradeable
{
    private ParticleSystemForceField forceField;
    private int level = 1;
    [SerializeField] private int upgradePercentage;
    private string upgradeTitle = "Vacuum level ";
    private string upgradeText = "Increase pick up range by ";
    public string GetUpgradeText()
    {
        return upgradeText + upgradePercentage.ToString()+"%";
    }

    public string GetUpgradeTitle()
    {
        return upgradeTitle + level.ToString();
    }

    public void Upgrade()
    {
        forceField.endRange += forceField.endRange * upgradePercentage / 100;
        level += 1;
        print("upgrade pick up range");
    }

    // Start is called before the first frame update
    void Start()
    {
        forceField = GetComponent<ParticleSystemForceField>();
        if(upgradePercentage == 0)
        {
            upgradePercentage = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool isActivated()
    {
        return gameObject.activeSelf;
    }

    public Image GetIcon()
    {
        throw new System.NotImplementedException();
    }
}
