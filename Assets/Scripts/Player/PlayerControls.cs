using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool godMode;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private int nbBlocksUnderPlayer = 0;

    private const int hardBlockToTheRight = 1;
    private const int hardBlockToTheLeft = -1;
    private int collisionFailSafe = 0;

    private float vertical;

    private const float coyoteTimeMax = 0.2f;
    private float coyoteTime = coyoteTimeMax;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        HorizontalMovement();
        if (godMode)
        {
            GodModeVerticalMovement();
        }
        else
        {
            VerticalMovement();
        }
    }

    private bool IsGrounded()
    {
        return nbBlocksUnderPlayer != 0;
    }

    private void VerticalMovement()
    {
        //Si pas au sol et qu'il y a du coyote time, on d�cr�mente le coyote time.
        if (!IsGrounded() && coyoteTime > 0f) coyoteTime -= Time.deltaTime;

        //Si on est grounded, le coyoteTme est n�cessairement plus grand que 0
        if (Input.GetButtonDown("Jump") && coyoteTime > 0f) InitJumpMovement();
    }

    private void GodModeVerticalMovement()
    {
        vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(rb.velocity.x, vertical * speed);
        rb.velocity = movement;
    }

    private void InitJumpMovement()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        audioSource.PlayOneShot(SoundManager.Instance.PlayerJump);
        //Trigger l'animation Jump
        animator.SetTrigger("Jump");

        coyoteTime = 0f;
    }

    private void HorizontalMovement()
    {
        vertical = Input.GetAxis("Horizontal");

        if (collisionFailSafe < 0 && vertical < 0f)  //Si le bloc est � gauche et qu'on pousse vers la gauche
            vertical = 0f;
        else if (collisionFailSafe > 0 && vertical > 0f)  //Si le bloc est � droite et qu'on pousse vers la droite
            vertical = 0f;

        Vector2 movement = new Vector2(vertical * speed, rb.velocity.y);
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
