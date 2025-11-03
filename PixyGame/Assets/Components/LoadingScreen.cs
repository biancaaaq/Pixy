using UnityEngine;

public class LoadingScreenBackgroundGenerator : MonoBehaviour
{
    public int length = LoadingScreenConstant.length;
    public int startingHeight = LoadingScreenConstant.startingHeight;
    public int maxHeight = LoadingScreenConstant.maximumHeight;
    public int maximumStep = LoadingScreenConstant.maximumStep;

    void Start()
    {
        int[] heights = generateHeightsArray(length, startingHeight, maxHeight, maximumStep);
    }

    int[] generateHeightsArray(int length, int min, int max, int step)
    {
        int[] result = new int[length];
        result[0] = Random.Range(min, max + 1);
        for (int i = 1; i < length; i++)
        {
            int change = Random.Range(-step, step + 1);
            int nextValue = result[i - 1] + change;
            nextValue = Mathf.Clamp(nextValue, min, max);
            result[i] = nextValue;
        }
        return result;
    }
}
