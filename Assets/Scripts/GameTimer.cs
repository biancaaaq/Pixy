using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private float timeRemaining = 30f;

    private bool timerRunning = true;

    void Start()
    {
        if (timerText == null)
        {
            Debug.LogError("TimerText nu este asignat!");
        }
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (!timerRunning) return;

        timeRemaining -= Time.deltaTime;
        UpdateTimerDisplay();

        
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }

        
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            timerRunning = false;
            RestartLevel();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeRemaining).ToString();
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}