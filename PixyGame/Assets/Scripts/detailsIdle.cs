using UnityEngine;

public class TilemapIdleEffect : MonoBehaviour
{
    public float swayAmplitudeX = 0.08f;
    public float swaySpeedX = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * swaySpeedX) * swayAmplitudeX;
        transform.position = initialPosition + new Vector3(offsetX, 0, 0);
    }
}
