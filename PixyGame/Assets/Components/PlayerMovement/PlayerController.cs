using UnityEngine;
using System.Collections;
using System;
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = PlayerMovementConstants.moveSpeed;
    public float jumpForce = PlayerMovementConstants.jumpForce;

    private CapsuleCollider2D playerCollider;

    public float groundCheckRadius = PlayerMovementConstants.groundCheckRadius;

    private float doubleTapTime = PlayerMovementConstants.doubleTapTime;

    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private bool isDashing = false;

    private TrailRenderer trailRenderer;

    private int lastMoveDirection = 0;

    private float lastTapTime = 0f;

    private int jumpCount = 0;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        rigidBody.gravityScale = PlayerMovementConstants.gravityScale;
        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        trailRenderer = GetComponent<TrailRenderer>();
        lastTapTime = Time.time;
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (!isDashing)
            rigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidBody.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.A))
            CheckDash(-1);

        if (Input.GetKeyDown(KeyCode.D))
            CheckDash(1);

        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        bool isGrounded = IsGrounded();
        if (isGrounded)
            jumpCount = 0;

        if (Input.GetButtonDown("Jump") && jumpCount < 1)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
            jumpCount++;
        }
    }

    private bool IsGrounded()
    {
        Bounds bounds = playerCollider.bounds;
        Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, 0.1f);
        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.center.y - bounds.extents.y);
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);
        return hit != null;
    }

    private void OnDrawGizmos()
    {
        if (playerCollider == null) return;

        float footOffset = 0.05f;
        Vector2 capsuleCenter = (Vector2)playerCollider.bounds.center;
        float halfHeight = playerCollider.bounds.extents.y;

        Vector2 boxCenter = capsuleCenter + Vector2.down * (halfHeight + footOffset);
        Vector2 boxSize = new Vector2(playerCollider.bounds.size.x * 0.9f, 0.1f);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

    private void CheckDash(int direction)
    {
        if (lastMoveDirection == direction && Time.time - lastTapTime <= doubleTapTime)
        {
            StartDashing(direction);
        }

        lastMoveDirection = direction;
        lastTapTime = Time.time;
    }

    private void StartDashing(float direction)
    {
        isDashing = true;
        float dashSpeed = 20f;
        rigidBody.linearVelocity = new Vector2(direction * dashSpeed, rigidBody.linearVelocity.y);
        lastMoveDirection = 0;
        trailRenderer.emitting = true;
        StartCoroutine(EndDash());
    }

    private IEnumerator EndDash()
    {
        yield return new WaitForSeconds(0.2f);
        trailRenderer.emitting = false;
        isDashing = false;
    }

}
