using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleTimer : MonoBehaviour
{
    public Text timerText;          
    public float timeLeft = 30f;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Ceil(timeLeft).ToString();

        if (timeLeft <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}