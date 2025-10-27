using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    private const string BEST_SCORE_KEY = "BestScore";
    private const string LAST_SCORE_KEY = "LastScore";

    [SerializeField] GameObject scorePopup;
    [SerializeField] Button scoreButton;

    [SerializeField] TextMeshProUGUI bestScoreText;
    [SerializeField] TextMeshProUGUI lastScoreText;

    private void Awake()
    {
        scoreButton.onClick.AddListener(ClickButton);
    }

    private void ClickButton()
    {
        UpdateUI();
        scorePopup.SetActive(!scorePopup.activeSelf);
    }

    private void UpdateUI()
    {
        int bestScore = PlayerPrefs.GetInt(BEST_SCORE_KEY, 0);
        int lastScore = PlayerPrefs.GetInt(LAST_SCORE_KEY, 0);

        bestScoreText.text = $"Best Score: {bestScore}";
        lastScoreText.text = $"Last Score: {lastScore}";
    }
}
