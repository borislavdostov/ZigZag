using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score;
    public int highScore;

    private const string Score = "score";
    private const string HighScore = "highScore";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        PlayerPrefs.SetInt(Score, score);
    }

    void IncrementScore()
    {
        score += 1;
        UiManager.instance.scoreText.text = score.ToString();
    }

    public void IncrementScore(int points)
    {
        score += points;
        UiManager.instance.scoreText.text = score.ToString();
    }

    public void StartScore()
    {
        InvokeRepeating("IncrementScore", 0.1f, 0.5f);
    }

    public void StopScore()
    {
        //Stops incrementing the score
        CancelInvoke("IncrementScore");
        PlayerPrefs.SetInt(Score, score);

        if (PlayerPrefs.HasKey(HighScore))
        {
            if (score > GetHighScore())
            {
                PlayerPrefs.SetInt(HighScore, score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(HighScore, score);
        }
    }

    public int GetScore()
    {
        return PlayerPrefs.GetInt(Score);
    }

    public int GetHighScore()
    {
        return PlayerPrefs.GetInt(HighScore);
    }
}
