using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpParent : MonoBehaviour
{
    PopUpManager[] children;
    // Start is called before the first frame update
    void Start()
    {
        children = GetComponentsInChildren<PopUpManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PopUp(int damage)
    {
        foreach(PopUpManager child in children)
        {
            if (child.PopUp(damage))
            {
                break;
            }
        }
    }
}
