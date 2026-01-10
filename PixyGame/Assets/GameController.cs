
using UnityEngine;
using UnityEngine.Tilemaps;
public class GameController : MonoBehaviour
{
    public GameOverScreen gameOver;
    public Tilemap surface;
    public Tilemap cave;
    public AudioSource surfaceMusic;
    public AudioSource caveMusic;
    public Transform Player;

    public AchievementRendererTest achievementRenderer;
    private bool inCave = false;
    public void Start()
    {
        caveMusic.loop = true;
        caveMusic.Stop();
    }

    public void EndGame(int nrCoins)
    {
        caveMusic.Stop();
        gameOver.Setup(nrCoins);
    }

    private void Update()
    {
        Vector3Int playerCell = cave.WorldToCell(Player.position);

        Vector3Int belowCell = new Vector3Int(playerCell.x, playerCell.y - 1, playerCell.z);

        bool currentlyInCave = cave.HasTile(belowCell);

        if (currentlyInCave && !inCave)
        {
            inCave = true;
            PlayCaveMusic();
        }
    }

    private void PlayCaveMusic()
    {
        caveMusic.Play();
    }

    public void DisplayAchivement(AchievementEntity achivement)
    {
        achievementRenderer.Initialize(achivement);
    }

}