
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameOverScreen gameOver;

    public void EndGame(int nrCoins)
    {
        gameOver.Setup(nrCoins);
    }

}