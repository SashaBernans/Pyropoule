using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Vacuum : MonoBehaviour, IUpgradeable
{
    private ParticleSystemForceField forceField;
    private int level = 1;
    [SerializeField] private int upgradePercentage;
    [SerializeField] private GameObject vacuum1;
    [SerializeField] private GameObject vacuum2;
    [SerializeField] private GameObject vacuum3;
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
        //print("upgrade pick up range");
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
        if(level == 1)
        {
            return vacuum1.GetComponent<Image>();
        }
        else if(level == 2)
        {
            return vacuum2.GetComponent<Image>();
        }
        else
        {
            return vacuum3.GetComponent<Image>();
        }
    }
}
