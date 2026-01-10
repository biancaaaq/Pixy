using UnityEngine;

public class ProximityMobAnimator : MonoBehaviour
{
    public Sprite[] frames;
    public float frameRate = 6f;
    public Transform player;
    public float triggerDistance = 5f;
    private SpriteRenderer spriteRenderer;
    private int currentFrame = 0;
    private float timer = 0f;
    private bool playerNear = false;

    private int direction = 1;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (!spriteRenderer)
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = frames[0];
    }

    void Update()
    {
        if (player == null || frames.Length == 0) return;

        if (player != null)
        {
            if (player.position.x < transform.position.x)
                spriteRenderer.flipX = true;
            else
                spriteRenderer.flipX = false;
        }

        float distance = Vector2.Distance(transform.position, player.position);
        playerNear = distance <= triggerDistance;

        if (!playerNear)
        {
            timer += Time.deltaTime;
            if (timer >= 1f / frameRate)
            {
                timer = 0f;

                if (currentFrame > 0)
                {
                    currentFrame--;
                    spriteRenderer.sprite = frames[currentFrame];
                }
                else
                {
                    currentFrame = 0;
                    spriteRenderer.sprite = frames[0];
                }
            }

            direction = 1;
            return;
        }

        timer += Time.deltaTime;
        if (timer >= 1f / frameRate)
        {
            timer -= 1f / frameRate;

            if (currentFrame == 0)
            {
                currentFrame = 1;
            }
            else if (currentFrame == frames.Length - 1)
            {
                direction = -1;
                currentFrame = frames.Length - 2;
            }
            else if (currentFrame == frames.Length - 3 && direction == -1)
            {
                direction = 1;
                currentFrame = frames.Length - 2;
            }
            else
            {
                currentFrame += direction;
            }

            spriteRenderer.sprite = frames[currentFrame];
        }
    }
}
