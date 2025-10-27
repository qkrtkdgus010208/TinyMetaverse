using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject gameOver;
    public Button GoToMainButton;

    public void Start()
    {
        if (gameOver == null)
        {
            Debug.LogError("restart text is null");
        }

        if (scoreText == null)
        {
            Debug.LogError("scoreText is null");
            return;
        }

        GoToMainButton.onClick.AddListener(GoToMain);

        gameOver.SetActive(false);
    }

    public void SetRestart()
    {
        gameOver.SetActive(true);
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void GoToMain()
    {
        GameManager.Instance.LoadMainScene();
    }
}