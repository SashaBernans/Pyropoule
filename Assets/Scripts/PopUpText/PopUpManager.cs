using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    private Animator animator;
    private TextMeshPro text; 

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        text = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void popUp(int damage)
    {
        text.SetText(damage.ToString());
        animator.Play("PopUp");
    }

    //Called in animation 
    public void EraseTextEvent()
    {
        text.SetText("");
    }

    private void OnEnable()
    {
        if (text != null)
        {
            text.SetText("");
        }
    }
}
