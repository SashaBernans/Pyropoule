using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    private Animator animator;
    private TextMeshPro text;

    private const string POP_UP_STATE = "PopUp";
    private const string IDLE_STATE = "Idle";

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

    public bool PopUp(int damage)
    {

        //text.SetText("");
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(POP_UP_STATE))
        {
            return false;
        }
        if (gameObject.activeSelf)
        {
            text.SetText(damage.ToString());
            animator.Play(POP_UP_STATE);
        }
        return true;
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
