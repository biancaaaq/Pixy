using UnityEngine;
using UnityEngine.UI;

public class LoopBackground : MonoBehaviour
{
    public float speed = 0.01f;
    private RawImage rawImage;
    private Rect uvRect;

    void Start()
    {
        rawImage = GetComponent<RawImage>();
        uvRect = rawImage.uvRect;
    }

    void Update()
    {
        uvRect.x += speed * Time.deltaTime;
        rawImage.uvRect = uvRect;
    }
}