using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource scoreSFX;
    public AudioSource fallSFX;

    [Header("Objects to Get")]
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject restartUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private int maxScore;
    [SerializeField] private bool progressNextLevel;

    private bool isPaused;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        StartScene();
    }

    public void AddScore(int barScore) {
        LevelTransition.instance.totalScore += barScore;
        UpdateScore();

        if (LevelTransition.instance.totalScore >= maxScore)
        {
            if (progressNextLevel)
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void DeductLives()
    {
        Pause();
        LevelTransition.instance.livesLeft -= 1;
        UpdateLives();
        fallSFX.Play();

        if (LevelTransition.instance.livesLeft > 0)
            continueButton.SetActive(true);
        else
            restartUI.SetActive(true);
    }

    public void StartAndContinueGame()
    {
        Pause();
        ball.GetComponent<BallAwake>().StartBall();

        if (startButton != null)
            Destroy(startButton);
        else
            continueButton.SetActive(false);
    }

    public void Restart() => SceneManager.LoadScene("Level 1");

    private void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    private void UpdateScore() {
        int currScore = 0;

        if (LevelTransition.instance != null)
            currScore = LevelTransition.instance.totalScore;

        scoreText.text = "Score:  " + currScore.ToString();
    }

    private void UpdateLives() {
        int currLives = 3;

        if (LevelTransition.instance != null)
            currLives = LevelTransition.instance.livesLeft;

        livesText.text = "Lives: " + currLives.ToString();
    }

    private void StartScene()
    {
        Pause();
        UpdateScore();
        UpdateLives();
    }
}
