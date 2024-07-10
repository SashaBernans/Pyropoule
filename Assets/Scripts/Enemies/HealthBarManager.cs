using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damagePercentage)
    {
        Vector3 scale = transform.localScale;

        transform.localScale = new Vector3(scale.x - scale.x*(damagePercentage/100), scale.y, scale.z);
    }
}
