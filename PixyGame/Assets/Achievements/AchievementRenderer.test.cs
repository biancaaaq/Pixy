using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class AchievementRendererTest : MonoBehaviour
{
    public Image icon;
    public TMP_Text title;
    public TMP_Text description;
    public float moveDistance = 100f;
    public float fadeDuration = 2f;

    public void Awake()
    {
        Hide();
    }

    public void Hide()
    {
        icon.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        description.gameObject.SetActive(false);
        Transform background = icon.transform.parent;
        background.gameObject.SetActive(false);
    }

    public void Show()
    {
        icon.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        description.gameObject.SetActive(true);
        Transform background = icon.transform.parent;
        background.gameObject.SetActive(true);
    }
    public void Initialize(AchievementEntity achievement)
    {
        icon.sprite = achievement.icon;
        title.text = achievement.title;
        description.text = achievement.description;
        Show();
        StartCoroutine(WaitAndDisplay());
    }

    private IEnumerator WaitAndDisplay()
    {
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        float elapsed = 0f;

        Color iconColor = icon.color;
        Color titleColor = title.color;
        Color descColor = description.color;

        iconColor.a = 1f;
        titleColor.a = 1f;
        descColor.a = 1f;

        icon.color = iconColor;
        title.color = titleColor;
        description.color = descColor;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, alpha);
            title.color = new Color(title.color.r, title.color.g, title.color.b, alpha);
            description.color = new Color(description.color.r, description.color.g, description.color.b, alpha);

            yield return null;
        }

        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, 0f);
        title.color = new Color(title.color.r, title.color.g, title.color.b, 0f);
        description.color = new Color(description.color.r, description.color.g, description.color.b, 0f);

        Hide();
    }
}
