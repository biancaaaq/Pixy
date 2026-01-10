using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonAudioManager : MonoBehaviour
{
    public static ButtonAudioManager Instance;
    public AudioSource clickSound;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            RegisterAllButtons();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RegisterAllButtons();
    }

    public void RegisterAllButtons()
    {
        Button[] buttons = FindObjectsByType<Button>(FindObjectsSortMode.None);
        foreach (Button button in buttons)
            button.onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        clickSound.PlayOneShot(clickSound.clip);
    }
}
