using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class LongPlatformLightningManager : MonoBehaviour
{
    public bool isWaterPlatform = false;

    public BoxCollider2D colli;
    public GameObject bound;
    private Bounds newbound;
    private Transform self;
    private LongPlatformCollisions longPlatformCollisions;
    internal GameObject currentLightning;

    // Start is called before the first frame update
    void Start()
    {
        colli = GetComponent<BoxCollider2D>();
        longPlatformCollisions = GetComponent<LongPlatformCollisions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private GameObject CheckForClosestLongPlatform()
    {
        float closestDistance = float.MaxValue;
        GameObject closestPlatform = null;
        foreach (GameObject platform in WorldGenerator.Instance.activeWaterLongPlatforms)
        {
            if (platform.activeSelf && platform.transform.position != transform.position)
            {
                float currentDistance = Vector2.Distance(platform.transform.position, transform.position);
                if (currentDistance < closestDistance)
                {
                    if (!platform.GetComponent<LongPlatformCollisions>().isLightningActive)
                    {
                        closestDistance = currentDistance;
                        closestPlatform = platform;
                    }
                }
            }
        }
        return closestPlatform;
    }

    private void OnDisable()
    {
        if(isWaterPlatform)
        {
            WorldGenerator.Instance.activeWaterLongPlatforms.Remove(gameObject);
            isWaterPlatform = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isWaterPlatform)
        {
            if (collision.tag == "Player")
            {
                if (!longPlatformCollisions.isLightningActive)
                {
                    StartCoroutine(longPlatformCollisions.ManageElectrifyTime());
                    ShootLightning();
                    longPlatformCollisions.isLightningActive = true;
                    print("On lONGPLATFORM");
                }
            }
        }
    }

    public void ShootLightning()
    {
        GameObject g = CheckForClosestLongPlatform();
        if (g != null)
        {
            Vector3 target = g.transform.GetChild(g.transform.childCount / 2).transform.position;

            GameObject lightning = AssetRecycler.Instance.LightningPool.Find(p => !p.activeInHierarchy);
            currentLightning = lightning;
            BoxCollider2D lightningColli = lightning.GetComponent<BoxCollider2D>();
            lightning.transform.position = transform.GetChild(0).position;

            lightning.transform.rotation = Quaternion.LookRotation(Vector3.forward, target - lightning.transform.position);

            lightning.SetActive(true);
            AjustScale(target, lightning, lightningColli);
        }
    }

    private static void AjustScale(Vector3 target, GameObject lightning, BoxCollider2D lightningColli)
    {
        float ySizeToBe = Vector2.Distance(lightning.transform.position, target);

        if (ySizeToBe < lightningColli.size.y)
        {
            float yScale = ySizeToBe / lightningColli.size.y;
            lightning.transform.localScale = new Vector3(lightning.transform.localScale.x, yScale, lightning.transform.localScale.z);
        }
        else
        {
            float yScale = ySizeToBe / lightningColli.size.y;
            lightning.transform.localScale = new Vector3(lightning.transform.localScale.x, yScale, lightning.transform.localScale.z);
        }
    }

    public void SetColliderBounds()
    {
        colli = GetComponent<BoxCollider2D>();
        GameObject leftBound = transform.GetChild(0).gameObject;
        GameObject rightBound = transform.GetChild(transform.childCount-1).gameObject;
        BoxCollider2D left = leftBound.GetComponent<BoxCollider2D>();
        BoxCollider2D right = rightBound.GetComponent<BoxCollider2D>();

        //This just works
        colli.offset = new Vector2((right.size.x * transform.childCount /2  + right.size.x/2)*1.20f, 0);

        colli.size = new Vector2(right.size.x * transform.childCount*1.20f, right.size.y * right.transform.localScale.y);
    }
}
