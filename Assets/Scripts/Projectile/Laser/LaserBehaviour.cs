using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float speed;
    private SpriteRenderer spriteRenderer;
    private bool canBeHit = true;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotationThisFrame = speed * Time.deltaTime;

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
