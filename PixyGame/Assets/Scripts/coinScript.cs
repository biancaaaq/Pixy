using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Coin : MonoBehaviour
{
    public AudioClip sound;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator HandleCoinPick()
    {

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        ButtonAudioManager.Instance.PlaySound(sound);

        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.up;

        Color startColor = spriteRenderer.color;

        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / 0.5f;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            Color c = startColor;
            c.a = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = c;
            yield return null;
        }
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 0f);

        Destroy(gameObject);
    }
}
