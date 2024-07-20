using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private static PanelManager instance = null;
    public static PanelManager Instance { get { return instance; } }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if(instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private OptionText[] GetOptions()
    {
        OptionText[] options = GetComponentsInChildren<OptionText>();
        return options;
    }

    public void SetUpText(List<IUpgradeable> upgradeables)
    {
        PauseManager.Instance.PauseForMenu();
        gameObject.SetActive(true);
        OptionText[] options = GetOptions();
        foreach(OptionText option in options)
        {
            
        }
    }

    public enum Upgrade
    {
        BaseWeaponDamage,
        BaseWeaponSpeed
    }

    private Upgrade GetRandomUpgrade()
    {
        // Create an instance of Random
        System.Random random = new System.Random();

        // Get all values of the Colors enum
        Array values = Enum.GetValues(typeof(Upgrade));

        // Generate a random index based on the number of values in the enum
        Upgrade randomUpgrade = (Upgrade)values.GetValue(random.Next(values.Length));

        return randomUpgrade;
    }
}
