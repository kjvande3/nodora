using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatformingPhysics : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 300.0f;
    [SerializeField]
    private float floorMoveForce = 5.0f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private float gravityScale = 3.0f;
    [SerializeField]
    private float slashingForce = 1000.0f;

    public enum SlashingState
    {
        NOT_SLASHING, AIMING, SLASHING
    }
    private SlashingState slashingState = SlashingState.NOT_SLASHING;
    private int slashingFrames = 0;
    private float slashingAngle = 0;

    private Animator animator;

    private Transform floorChecker;
    private Rigidbody2D rb2D;
    private bool onFloor = false;
    private bool facingRight = true;

    private const float checkerRadius = 0.2f;

    private void Start()
    {
        floorChecker = transform.Find("FloorChecker");
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        onFloor = false;

        // Check if we're colliding with a floor
        Collider2D[] floors = Physics2D.OverlapCircleAll(
                floorChecker.position,
                checkerRadius,
                groundLayer);
        foreach (Collider2D floor in floors)
        {
            if (floor.gameObject != gameObject)
            {
                onFloor = true;
            }
        }

        if (slashingState == SlashingState.SLASHING)
        {
            if (slashingFrames > 0)
            {
                slashingFrames--;
                if (slashingFrames == 0)
                {
                    slashingState = SlashingState.NOT_SLASHING;
                    rb2D.gravityScale = gravityScale;
                    rb2D.velocity = new Vector2(0.0f, 0.0f);
                }
            }
        }

        if (slashingState == SlashingState.NOT_SLASHING)
        {
            if (rb2D.velocity.y != 0)
            {
                animator.Play("PlayerJumping");
            }
            else if (rb2D.velocity.x != 0)
            {
                animator.Play("PlayerWalking");
            }
            else
            {
                animator.Play("PlayerIdle");
            }
        }
        else if (slashingState == SlashingState.SLASHING)
        {
            animator.Play("PlayerSlashing");
        }
    }

    /// <summary>
    /// Starts moving the player left if appropriate. The actual movement will
    /// happen on the next fixed update frame.
    /// </summary>
    public void MoveLeft()
    {
        if (slashingState != SlashingState.NOT_SLASHING)
        {
            return;
        }

        if (facingRight)
        {
            Flip();
        }

        rb2D.velocity = new Vector2(
                -floorMoveForce,
                rb2D.velocity.y);
    }

    /// <summary>
    /// Starts moving the player right if appropriate. The actual movement will
    /// happen on the next fixed update frame.
    /// </summary>
    public void MoveRight()
    {
        if (slashingState != SlashingState.NOT_SLASHING)
        {
            return;
        }

        if (!facingRight)
        {
            Flip();
        }

        rb2D.velocity = new Vector2(
                floorMoveForce,
                rb2D.velocity.y);
    }

    /// <summary>
    /// Has the player jump if appropriate. The actual jumping will happen on
    /// the next fixed update frame.
    /// </summary>
    public void Jump()
    {
        if (onFloor)
        {
            onFloor = false;
            rb2D.AddForce(new Vector2(0.0f, jumpForce));
        }
    }

    /// <summary>
    /// Has the player take aim before the slash if appropriate in the next
    /// fixed update frame.
    /// </summary>
    public void Aim()
    {
        if (slashingState != SlashingState.NOT_SLASHING)
        {
            return;
        }
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(0.0f, 0.0f);

        slashingState = SlashingState.AIMING;
    }

    /// <summary>
    /// Has the player slash if appropriate in the next fixed update frame.
    /// </summary>
    public void Slash(float angle)
    {
        if (slashingState != SlashingState.AIMING)
        {
            return;
        }

        float xForce = slashingForce * Mathf.Cos(angle);
        float yForce = slashingForce * Mathf.Sin(angle);
        rb2D.AddForce(new Vector2(xForce, yForce));

        slashingState = SlashingState.SLASHING;
        slashingFrames = 10;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.localScale = new Vector3(
                transform.localScale.x * -1,
                transform.localScale.y,
                transform.localScale.z);
    }
}
