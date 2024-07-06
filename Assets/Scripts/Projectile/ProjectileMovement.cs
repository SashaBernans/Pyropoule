using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector2 direction;
    private Rigidbody2D rb;

    public Vector2 Direction
    {
        get => direction; set
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.y);
            this.direction = (value - position).normalized;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        rb.velocity = direction * speed;
    }
}
