using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PanelManager;

public class OptionText : MonoBehaviour
{
    private TMP_Text upgradeText;
    private TMP_Text upgradeTitle;

    private IUpgradeable upgrade;

    public IUpgradeable Upgrade { get => upgrade; set => upgrade = value; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUpText(IUpgradeable upgradeable)
    {
        upgrade = upgradeable;
        TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

        if (texts[0].gameObject.tag=="OptionTitle")
        {
            upgradeTitle = texts[0];
            upgradeText = texts[1];
        }
        else
        {
            upgradeTitle = texts[1];
            upgradeText = texts[0];
        }
        upgradeText.text = upgrade.GetUpgradeText();
        upgradeTitle.text = upgrade.GetUpgradeTitle();
    }

    public void OnClick()
    {
        upgrade.Upgrade();
        this.gameObject.GetComponentInParent<PanelManager>().gameObject.SetActive(false);
        PauseManager.Instance.PauseForMenu();
    }

}
