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

        ///// Later I will have to sort for only activated upgrades
        IUpgradeable[] upgrades = GetRandomUpgradeables(upgradeables, options.Length);
        for (int i = 0; i<upgrades.Length; i++)
        {
            options[i].SetUpText(upgrades[i]);
        }
    }

    public static IUpgradeable[] GetRandomUpgradeables(List<IUpgradeable> upgradeables, int numberOfUpgrades)
    {
        System.Random random = new System.Random();

        if (upgradeables == null || upgradeables.Count == 0)
        {
            throw new ArgumentException("The upgradeables list cannot be null or empty.");
        }

        if (numberOfUpgrades <= 0)
        {
            throw new ArgumentException("The number of upgrades must be greater than zero.");
        }

        int upgradeCount = Math.Min(numberOfUpgrades, upgradeables.Count);
        IUpgradeable[] selectedUpgradeables = new IUpgradeable[upgradeCount];
        HashSet<int> selectedIndices = new HashSet<int>();

        for (int i = 0; i < upgradeCount; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = random.Next(upgradeables.Count);
            } while (selectedIndices.Contains(randomIndex));

            selectedIndices.Add(randomIndex);
            selectedUpgradeables[i] = upgradeables[randomIndex];
        }

        return selectedUpgradeables;
    }
}
