using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartGame()
    {
        UiManager.instance.GameStart();
        ScoreManager.instance.StartScore();
        GameObject.Find("PlatformSpawner").GetComponent<PlatformSpawner>().StartSpawningPlatforms();
    }

    public void GameOver()
    {
        UiManager.instance.GameOver();
        ScoreManager.instance.StopScore();
        gameOver = true;
    }
}
