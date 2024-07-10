using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;
    public float rotationSpeed = 90f;
    private SpriteRenderer spriteRenderer;
    private Vector3 pivotPoint;
    private bool canBeHit = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the rotation for this frame
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        //Calculate the pivot point
        //Vector3 pivotPoint = spriteRenderer.bounds.max;

        // Apply the rotation around the Z axis
        transform.RotateAround(transform.position, Vector3.forward, rotationThisFrame);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
        }
    }
    */
}
