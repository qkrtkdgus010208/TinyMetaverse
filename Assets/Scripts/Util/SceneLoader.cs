using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneNames { StartScene = 0, MainScene, MiniGameScene, LoadScene }

public class SceneLoader : MonoBehaviour
{
    static SceneNames nextScene;

    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI text;

    public static void LoadScene(SceneNames sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene(SceneNames.LoadScene.ToString());
    }

    private void Start()
    {
        StartCoroutine(LoadSceneProgress());
    }

    IEnumerator LoadSceneProgress()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene.ToString());
        asyncOperation.allowSceneActivation = false;

        float timer = 0f;
        while (!asyncOperation.isDone)
        {
            yield return null;

            float targetProgress;

            if (asyncOperation.progress < 0.9f)
            {
                targetProgress = asyncOperation.progress;
                slider.value = targetProgress;
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                targetProgress = Mathf.Lerp(0.9f, 1f, timer);
                slider.value = targetProgress;
            }

            int percent = (int)(Mathf.Clamp01(slider.value) * 100);
            text.text = $"Loading... {percent}%";

            // 씬 활성화 로직
            if (asyncOperation.progress >= 0.9f && slider.value >= 1f)
            {
                asyncOperation.allowSceneActivation = true;
                yield break;
            }
        }
    }
}
