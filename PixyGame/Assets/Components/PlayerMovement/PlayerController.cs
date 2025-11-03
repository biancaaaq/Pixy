using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = PlayerMovementConstants.moveSpeed;
    public float jumpForce = PlayerMovementConstants.jumpForce;

    public float groundCheckRadius = PlayerMovementConstants.groundCheckRadius;
    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody.gravityScale = PlayerMovementConstants.gravityScale;
        rigidBody.freezeRotation = true;
        rigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rigidBody.linearVelocity = new Vector2(moveInput * moveSpeed, rigidBody.linearVelocity.y);
        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;
        if (Input.GetButtonDown("Jump"))
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
        }
    }
}
