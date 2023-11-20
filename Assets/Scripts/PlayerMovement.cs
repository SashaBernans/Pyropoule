using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int nbBlocksUnderPlayer = 0;
    private bool isCollidingWithPlatform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        Jump();
    }

    private void Jump()
    {
        if (nbBlocksUnderPlayer>0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                //Trigger l'animation Jump
                animator.SetTrigger("Jump");
            }
        }
    }

    private void HorizontalMovement()
    {
        if (nbBlocksUnderPlayer==0 && isCollidingWithPlatform)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        else
        {
            float horizontalInput = Input.GetAxis("Horizontal");

            Vector2 movement = new Vector2(horizontalInput * speed, rb.velocity.y);
            rb.velocity = movement;

            ManageLookingDirection();
            ManageRunAnimation();
        }
    }

    private void ManageLookingDirection()
    {
        if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void ManageRunAnimation()
    {
        if (Math.Abs(rb.velocity.x) > 1)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "HardBlock" | other.gameObject.tag == "Platform")
        {
            animator.SetBool("isJumping", false);
            isCollidingWithPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "HardBlock" | other.gameObject.tag == "Platform")
        {
            animator.SetBool("isJumping", true);
            isCollidingWithPlatform = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HardBlock" | other.gameObject.tag == "Platform")
        {
            nbBlocksUnderPlayer++;
        }
        Debug.Log(nbBlocksUnderPlayer);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "HardBlock" | other.gameObject.tag == "Platform")
        {
            nbBlocksUnderPlayer--;
        }
        Debug.Log(nbBlocksUnderPlayer);
    }
}
