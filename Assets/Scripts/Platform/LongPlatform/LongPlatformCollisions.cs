using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongPlatformCollisions : MonoBehaviour
{
    private LongPlatformLightningManager lightningManager;
    internal bool isLightningActive = false;
    ShockManager[] shockManagers;

    // Start is called before the first frame update
    void Start()
    {
        lightningManager = GetComponent<LongPlatformLightningManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        //shockManagers = GetComponentsInChildren<ShockManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Lightning")
        {
            if (!isLightningActive && lightningManager.isWaterPlatform)
            {
                StartCoroutine(ManageElectrifyTime());
                lightningManager.ShootLightning();
                ManageShocks();
                isLightningActive = true;
            }
        }
    }
    private void OnDisable()
    {
        isLightningActive = false;
    }

    private void ManageShocks()
    {
        shockManagers = GetComponentsInChildren<ShockManager>();

        shockManagers[0].ShockLeft();
        shockManagers[shockManagers.Length-1].ShockRight();

        for (int i = 0; i < shockManagers.Length; i++)
        {
            shockManagers[i].ShockUp();
            shockManagers[i].ShockDown();
        }
    }

    internal IEnumerator ManageElectrifyTime()
    {
        yield return new WaitForSeconds(1);
        if (lightningManager.currentLightning!=null)
        {
            lightningManager.currentLightning.SetActive(false);
        }
        isLightningActive = false;
        yield return new WaitForSeconds(1);
        ResetShocks();
    }

    private void ResetShocks()
    {
        foreach (ShockManager shockManager in shockManagers)
        {
            shockManager.ResetShocks();
        }
    }
}
