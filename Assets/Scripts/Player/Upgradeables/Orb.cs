using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour, IUpgradeable
{
    private SpriteRenderer spriteRenderer;
    private PlayerUpgradesManager playerUpgradesManager;
    [SerializeField] private float speed;
    private Vector3 playerPosition;

    public string GetUpgradeText()
    {
        throw new System.NotImplementedException();
    }

    public void Upgrade()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        //playerUpgradesManager = GetComponentInParent<PlayerUpgradesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation for this frame
        float rotationThisFrame = speed * Time.deltaTime;

        //Calculate the pivot point
        //Vector3 pivotPoint = spriteRenderer.bounds.max;

        // Apply the rotation around the Z axis
        transform.RotateAround(transform.parent.transform.position, Vector3.forward, rotationThisFrame);
    }
}
