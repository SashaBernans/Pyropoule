using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public void ManageRotation(Vector3 target)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, target - transform.position);
    }*/

    private void OnDisable()
    {
        transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
    }
}
