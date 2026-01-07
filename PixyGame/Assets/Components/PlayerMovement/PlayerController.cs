using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = PlayerMovementConstants.moveSpeed;
    public float jumpForce = PlayerMovementConstants.jumpForce;
    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    private int lastMoveDirection = 0;
    private float lastTapTime = 0f;
    private float doubleTapTime = PlayerMovementConstants.doubleTapTime;
    private int jumpCount = 0;
    public Transform groundCheck;
    public LayerMask groundLayer;
    private CapsuleCollider2D playerCollider;
    private bool isHit = false;
    public LayerMask slimeLayer;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private TrailRenderer trailRenderer;
    private int lives = 3;
    private bool isDead = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();

        rigidBody.gravityScale = PlayerMovementConstants.gravityScale;
        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        lastTapTime = Time.time;
    }

    private void Update()
    {
        if (isHit) return;
        animator.SetBool("isHit", false);

        float moveInput = Input.GetAxisRaw("Horizontal");

        if (!isDashing)
            rigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidBody.linearVelocity.y);

        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;

        bool isGrounded = IsGrounded();
        if (isGrounded) jumpCount = 0;

        if (Input.GetButtonDown("Jump") && jumpCount < 1)
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
            jumpCount++;
        }

        if (Input.GetKeyDown(KeyCode.A)) CheckDash(-1);
        if (Input.GetKeyDown(KeyCode.D)) CheckDash(1);

        UpdateAnimation(moveInput, isGrounded);
    }

    private void UpdateAnimation(float moveInput, bool isGrounded)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AvatarHit")) return;
        bool isWalking = Mathf.Abs(moveInput) > 0.01f && isGrounded;
        animator.SetBool("isWalking", isWalking);
    }

    private bool IsGrounded()
    {
        Bounds bounds = playerCollider.bounds;
        Vector2 boxSize = new Vector2(bounds.size.x * 0.9f, 0.1f);
        Vector2 boxCenter = new Vector2(bounds.center.x, bounds.center.y - bounds.extents.y);
        Collider2D hit = Physics2D.OverlapBox(boxCenter, boxSize, 0f, groundLayer);
        return hit != null;
    }

    private void CheckDash(int direction)
    {
        if (lastMoveDirection == direction && Time.time - lastTapTime <= doubleTapTime)
        {
            StartCoroutine(StartDashing(direction));
        }

        lastMoveDirection = direction;
        lastTapTime = Time.time;
    }

    private IEnumerator StartDashing(float direction)
    {
        isDashing = true;
        trailRenderer.emitting = true;

        rigidBody.linearVelocity = new Vector2(direction * dashSpeed, rigidBody.linearVelocity.y);
        yield return new WaitForSeconds(dashDuration);

        trailRenderer.emitting = false;
        isDashing = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Slime") && !isHit)
        {
            StartCoroutine(HandleHit(collision.transform));
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Lava"))
        {
            if (!isDead)
            {
                isDead = true;
                animator.SetTrigger("isDead");
                rigidBody.linearVelocity = Vector2.zero;
                isHit = true;
            }
        }
    }

    private IEnumerator HandleHit(Transform slime)
    {
        isHit = true;
        lives--;

        if (lives <= 0)
        {
            animator.SetTrigger("isDead");
        }

        animator.SetTrigger("isHit");

        Vector2 knockback = (transform.position - slime.position).normalized * 5f;
        rigidBody.linearVelocity = new Vector2(knockback.x, 5f);

        float hitDuration = 0.5f;
        yield return new WaitForSeconds(hitDuration);

        if (lives > 0)
            isHit = false;
    }
}
