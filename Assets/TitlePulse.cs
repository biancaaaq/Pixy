using UnityEngine;

public class TitlePulse : MonoBehaviour

{

    void Update()

    {

        float pulse = 1f + Mathf.Sin(Time.time * 2f) * 0.1f; 

        transform.localScale = Vector3.one * pulse;

    }

}