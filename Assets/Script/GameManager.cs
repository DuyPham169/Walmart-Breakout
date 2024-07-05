using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Audio")]
    public AudioSource scoreSFX;
    public AudioSource fallSFX;
    public AudioSource victorySFX;

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
            StartCoroutine(NextLevelRoutine());
        }
    }

    private IEnumerator NextLevelRoutine()
    {
        victorySFX.Play();
        Pause();

        yield return new WaitForSecondsRealtime(1.5f);

        Pause();
        if (progressNextLevel)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else
            SceneManager.LoadScene("VictoryScene");
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

    public void Restart() {
        LevelTransition.instance.DestroyTransition();
        SceneManager.LoadScene("Level 1");
    }

    private void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    private void UpdateScore() {
        int currScore = LevelTransition.instance != null ? LevelTransition.instance.totalScore : 0;

        scoreText.text = "Score:  " + currScore.ToString();
    }

    private void UpdateLives() {
        int currLives = LevelTransition.instance != null ? LevelTransition.instance.livesLeft : 3;

        livesText.text = "Lives: " + currLives.ToString();
    }

    private void StartScene()
    {
        Pause();
        UpdateScore();
        UpdateLives();
    }
}
