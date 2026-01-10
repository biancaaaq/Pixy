using UnityEngine;
public class AvatarWalk : MonoBehaviour
{
    public Sprite[] frames;
    public float framesPerSecond = 10f;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private int currentFrame;
    private float timer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (frames != null && frames.Length > 0)
            spriteRenderer.sprite = frames[0];
    }

    void Update()
    {
        if (frames == null || frames.Length == 0)
            return;

        bool isMoving = rb.linearVelocity.sqrMagnitude > 0.01f;

        if (isMoving)
        {
            timer += Time.deltaTime;
            if (timer >= 1f / framesPerSecond)
            {
                timer -= 1f / framesPerSecond;
                currentFrame = (currentFrame + 1) % frames.Length;
                spriteRenderer.sprite = frames[currentFrame];
            }
        }
        else
        {
            timer = 0f;
            currentFrame = 0;
            spriteRenderer.sprite = frames[0];
        }
    }
}
