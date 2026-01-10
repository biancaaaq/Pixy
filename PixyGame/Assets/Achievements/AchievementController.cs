using UnityEngine;
using System.Collections;
using TMPro;

public class AchievementController : MonoBehaviour
{
    public SpriteRenderer icon;
    public TMP_Text title;
    public TMP_Text description;
    public float popUpTime = 2f;
    public float moveDistance = 2f;
    public float fadeDuration = 0.5f;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void HandleUnlock(AchievementEntity achievement)
    {
        icon.sprite = achievement.icon;
        title.text = achievement.title;
        description.text = achievement.description;

        StartCoroutine(HandlePopUp());
    }

    public IEnumerator HandlePopUp()
    {
        Color startColor = spriteRenderer.color;
        startColor.a = 1f;
        spriteRenderer.color = startColor;

        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + Vector3.right * moveDistance;

        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            Color c = spriteRenderer.color;
            c.a = Mathf.Lerp(1f, 0f, t);
            spriteRenderer.color = c;

            yield return null;
        }
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

        Destroy(gameObject);
    }
}
