using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    //We can access instance by every script in this project
    public static UiManager instance;

    public GameObject zigzagPanel;
    public GameObject gameOverPanel;

    public GameObject tapText;

    public Text score;
    public Text highScore1;
    public Text highScore2;
    public Text scoreText;

    private void Awake()
    {
        //Singleton pattern - this will ensure that there will be only 1 instance of UiManager
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Set the high score text at the start menu
        highScore1.text = "HIGH SCORE: " + ScoreManager.instance.GetHighScore();
    }

    public void GameStart()
    {
        //Tap text will dissapear.
        tapText.SetActive(false);
        //We get Animator, call it method Play() and pass the name of the animation
        zigzagPanel.GetComponent<Animator>().Play("panelUp");
        scoreText.GetComponent<Animator>().Play("scoreTextAppear");
    }

    public void GameOver()
    {
        //Set the score text
        score.text = ScoreManager.instance.GetScore().ToString();
        //Set the high score text
        highScore2.text = ScoreManager.instance.GetHighScore().ToString();
        //When we set active this panel, his animation will call automatically
        gameOverPanel.SetActive(true);
        //Score text dissapear
        scoreText.GetComponent<Animator>().Play("scoreTextDissapear");
    }

    public void Restart()
    {
        //We load our current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
