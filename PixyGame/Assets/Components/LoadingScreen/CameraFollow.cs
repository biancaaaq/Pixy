using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    void Start()
    {
        transform.position = new Vector3(transform.position.x, -0.5f, transform.position.z);
    }

    void Update()
    {
        if (player == null) return;
        float newX = player.position.x;
        transform.position = new Vector3(newX, -0.5f, transform.position.z);
    }
}
