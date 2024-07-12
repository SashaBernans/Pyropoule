using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    private Vector3 initialScale;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = transform.localScale;
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

    public void HealToMax()
    {
        transform.localScale = initialScale;
    }
}
