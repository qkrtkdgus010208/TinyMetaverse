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

            if (asyncOperation.progress < 0.9f)
            {
                slider.value = asyncOperation.progress;
                text.text = $"Loading... {(int)(Mathf.Clamp01(asyncOperation.progress / 0.9f) * 100)}%";
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                slider.value = Mathf.Lerp(0.9f, 1f, timer);

                if (slider.value >= 1f)
                {
                    asyncOperation.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
}
