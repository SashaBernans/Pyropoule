using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PanelManager;

public class OptionText : MonoBehaviour
{
    private TMP_Text t;

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText(string text)
    {
        t.text = text;
    }

    public void OnClick()
    {
        
    }

}
