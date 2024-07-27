using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class LongPlatformLightningManager : MonoBehaviour
{
    private bool isLightningActive = false;
    public bool isWaterPlatform = false;

    public BoxCollider2D colli;
    public GameObject bound;
    private Bounds newbound;
    private Transform self;

    // Start is called before the first frame update
    void Start()
    {
        colli = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        /*if (transform.childCount>0 && transform.GetChild(0).tag == "Platform")
        {
            isWaterPlatform = true;
            //CheckForClosestLongPlatform().transform.GetChild(0).GetComponent<SpriteRenderer>().color = new UnityEngine.Color(23,23,23);
        }*/
    }

    /*private GameObject CheckForClosestLongPlatform()
    {
        float previousDistance = 0;
        GameObject closestPlatform = null;
        print(WorldGenerator.Instance.activeWaterLongPlatforms.Count);
        foreach (GameObject platform in WorldGenerator.Instance.activeWaterLongPlatforms)
        {
            closestPlatform = platform;
            if (platform.activeSelf)
            {
                float currentDistance = Vector2.Distance(platform.transform.position, transform.position);
                if (currentDistance < previousDistance)
                {
                    previousDistance = currentDistance;
                    closestPlatform = platform;
                }
            }
        }
        return closestPlatform;
    }*/

    private GameObject CheckForClosestLongPlatform()
    {
        float closestDistance = float.MaxValue;
        GameObject closestPlatform = null;
        print(WorldGenerator.Instance.activeWaterLongPlatforms.Count);
        foreach (GameObject platform in WorldGenerator.Instance.activeWaterLongPlatforms)
        {
            if (platform.activeSelf && platform.transform.position != transform.position)
            {
                float currentDistance = Vector2.Distance(platform.transform.position, transform.position);
                if (currentDistance < closestDistance)
                {
                    closestDistance = currentDistance;
                    closestPlatform = platform;
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
                GameObject g = CheckForClosestLongPlatform();
                print(CheckForClosestLongPlatform());
                Transform t = g.transform.GetChild(0);
                t.GetComponent<SpriteRenderer>().color = new UnityEngine.Color(255, 255, 255, 255);
                t.position = new Vector2(t.position.x, t.position.y - 0.5f);
                print("On lONGPLATFORM");
            }
        }
    }

    public void SetColliderBounds()
    {
        colli = GetComponent<BoxCollider2D>();
        GameObject leftBound = transform.GetChild(0).gameObject;
        GameObject rightBound = transform.GetChild(transform.childCount-1).gameObject;
        BoxCollider2D left = leftBound.GetComponent<BoxCollider2D>();
        BoxCollider2D right = rightBound.GetComponent<BoxCollider2D>();

        

        colli.size = new Vector2(right.size.x * transform.childCount, right.size.y * right.transform.localScale.y);
    }
}
