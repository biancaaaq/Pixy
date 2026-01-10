using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TMP_Text coinsText;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Setup(int nrCoins)
    {
        gameObject.SetActive(true);
        coinsText.text = nrCoins.ToString() + " coins";
    }

    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
