using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPlatformCollisions : MonoBehaviour
{
    private LongPlatformLightningManager lightningManager;
    internal bool isLightningActive = false;
    // Start is called before the first frame update
    void Start()
    {
        lightningManager = GetComponent<LongPlatformLightningManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lightning")
        {
            if (!isLightningActive && lightningManager.isWaterPlatform)
            {
                lightningManager.ShootLightning();
                isLightningActive = true;
            }
            print("Lightning hit long platform");
        }
    }
    private void OnDisable()
    {
        isLightningActive = false;
    }
}
