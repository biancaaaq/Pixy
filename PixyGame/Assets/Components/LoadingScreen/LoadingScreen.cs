using UnityEngine;

public class LoadingScreenBackgroundGenerator : MonoBehaviour
{
    public int chunkSize = 20;
    public int maxHeight = LoadingScreenConstant.maximumHeight;
    public int maximumStep = LoadingScreenConstant.maximumStep;
    public int startWidth = LoadingScreenConstant.startWidth;
    public Sprite grass;
    public Sprite dirt;
    private int nextColumnIndex = 0;
    private float generationThreshold = 15f;
    private int lastHeight = -1;

    void Update()
    {
        float cameraX = Camera.main.transform.position.x;

        while (nextColumnIndex - 15 < cameraX + generationThreshold)
        {
            GenerateColumn(nextColumnIndex);
            nextColumnIndex++;
        }
    }

    void GenerateColumn(int i)
    {
        int height;
        if (lastHeight < 0)
        {
            height = Random.Range(1, maxHeight + 1);
        }
        else
        {
            int step = Random.Range(-1, 2);
            height = Mathf.Clamp(lastHeight + step, 1, maxHeight);
        }

        lastHeight = height;

        for (int j = 0; j < height; j++)
        {
            GameObject block = new GameObject($"Block_{i}_{j}");
            block.transform.position = new Vector3(i - 15, startWidth + j, 0);
            block.transform.parent = transform;
            SpriteRenderer sr = block.AddComponent<SpriteRenderer>();
            sr.sprite = (j == height - 1) ? grass : dirt;
            BoxCollider2D collider = block.AddComponent<BoxCollider2D>();
        }
    }
}
