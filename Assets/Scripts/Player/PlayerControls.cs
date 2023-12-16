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

    private const int hardBlockToTheRight = 1;
    private const int hardBlockToTheLeft = -1;
    private int collisionFailSafe = 0;

    private float horizontal;

    private const float coyoteTimeMax = 0.2f;
    private float coyoteTime = coyoteTimeMax;

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
        VerticalMovement();
    }

    private bool IsGrounded()
    {
        return nbBlocksUnderPlayer != 0;
    }

    private void VerticalMovement()
    {
        //Si pas au sol et qu'il y a du coyote time, on décrémente le coyote time.
        if (!IsGrounded() && coyoteTime > 0f) coyoteTime -= Time.deltaTime;

        //Si on est grounded, le coyoteTme est nécessairement plus grand que 0
        if (Input.GetButtonDown("Jump") && coyoteTime > 0f) InitJumpMovement();
    }

    private void InitJumpMovement()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        //Trigger l'animation Jump
        animator.SetTrigger("Jump");

        coyoteTime = 0f;
    }

    private void HorizontalMovement()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (collisionFailSafe < 0 && horizontal < 0f)  //Si le bloc est à gauche et qu'on pousse vers la gauche
            horizontal = 0f;
        else if (collisionFailSafe > 0 && horizontal > 0f)  //Si le bloc est à droite et qu'on pousse vers la droite
            horizontal = 0f;

        Vector2 movement = new Vector2(horizontal * speed, rb.velocity.y);
        rb.velocity = movement;

        ManageLookingDirection();
        ManageRunAnimation();
    }

    private void ManageFailSafeForContactWithBlock(Collision2D collision)
    {
        if (collision.gameObject.tag == "HardBlock" | collision.gameObject.tag == "Wall" && !IsGrounded())
        {
            if (collision.transform.position.x > transform.position.x)
                collisionFailSafe = hardBlockToTheRight;
            else if (collision.transform.position.x < transform.position.x)
                collisionFailSafe = hardBlockToTheLeft;
            else
                collisionFailSafe = 0;
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
        if (Math.Abs(rb.velocity.x) > 1 && IsGrounded())
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HardBlock" | other.gameObject.tag == "Platform")
        {
            nbBlocksUnderPlayer++;
            collisionFailSafe = 0;
            coyoteTime = coyoteTimeMax;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "HardBlock" | other.gameObject.tag == "Platform")
        {
            nbBlocksUnderPlayer--;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ManageFailSafeForContactWithBlock(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        NullifyFailSafeForContactWithBlock(collision);
    }

    private void NullifyFailSafeForContactWithBlock(Collision2D collision)
    {
        if (collision.gameObject.tag == "HardBlock" | collision.gameObject.tag == "Wall" && !IsGrounded() && collisionFailSafe != 0)
            collisionFailSafe = 0;
    }
}
