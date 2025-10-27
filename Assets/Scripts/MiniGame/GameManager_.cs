using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_ : MonoBehaviour
{
    private const string BEST_SCORE_KEY = "BestScore";
    private const string LAST_SCORE_KEY = "LastScore";

    static GameManager_ gameManager;

    public static GameManager_ Instance
    {
        get { return gameManager; }
    }

    UIManager uiManager;

    public UIManager UIManager
    {
        get { return uiManager; }
    }

    private bool isAlive;

    private int bestScore = 0;
    private int currentScore = 0;

    private void Awake()
    {
        gameManager = this;
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        isAlive = true;

        uiManager.UpdateScore(0);

        bestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
    }

    public void GameOver()
    {
        isAlive = false;

        if (bestScore < currentScore)
        {
            Debug.Log("최고 점수 갱신");

            bestScore = currentScore;
            PlayerPrefs.SetInt(BEST_SCORE_KEY, bestScore);
        }

        Debug.Log("최근 점수 갱신");
        PlayerPrefs.SetInt(LAST_SCORE_KEY, currentScore);

        Debug.Log("Game Over");
        uiManager.SetRestart();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        if (!isAlive)
            return;

        currentScore += score;
        uiManager.UpdateScore(currentScore);
        Debug.Log("Score: " + currentScore);
    }
}