using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float horizontalMargin = 2.5f;
    public float verticalMargin = 1.5f;
    public float smoothSpeed = 0.1f;
    public Vector3 offset = new Vector3(0, 0, -10);
    public bool limitY = true;
    public float minY = -1f;
    public float maxY = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = transform.position;

        float deltaX = player.position.x - transform.position.x;
        if (Mathf.Abs(deltaX) > horizontalMargin)
        {
            targetPos.x = player.position.x - Mathf.Sign(deltaX) * horizontalMargin;
        }

        float deltaY = player.position.y - transform.position.y;
        if (Mathf.Abs(deltaY) > verticalMargin)
        {
            targetPos.y = player.position.y - Mathf.Sign(deltaY) * verticalMargin;
        }

        if (limitY)
            targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        targetPos.z = offset.z;

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
    }
}
