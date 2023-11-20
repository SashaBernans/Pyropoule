using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Animator animator;
    private bool hasJumped = false;


    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Trigger the jump animation
            animator.SetTrigger("Jump");

            // Set a flag to prevent multiple jump animations
            hasJumped = true;
        }
    }

    private void HorizontalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
        rb.velocity = movement;
    }

    // You may want to reset the jump flag when the player lands or finishes the jump animation
    public void OnLanding()
    {
        // Reset the jump flag
        hasJumped = false;
    }
}
