using System.Threading.Tasks; // Task 사용을 위해 필요
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
        // 로딩 씬으로 전환
        SceneManager.LoadScene(SceneNames.LoadScene.ToString());
    }

    private void Start()
    {
        LoadSceneProgressAsync();
    }

    private async void LoadSceneProgressAsync()
    {
        // 비동기 로드 시작
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nextScene.ToString());
        asyncOperation.allowSceneActivation = false; // 자동 활성화 방지

        float timer = 0f;

        // 로딩 완료까지 대기 (asyncOperation.isDone이 false인 동안)
        while (!asyncOperation.isDone)
        {
            await Task.Yield(); // 다음 프레임까지 대기

            float targetProgress;

            // 로딩 진행률 반영 및 슬라이더 스무딩 로직
            if (asyncOperation.progress < 0.9f)
            {
                targetProgress = asyncOperation.progress;
            }
            else
            {
                // 씬 로드가 90% 완료된 후, 타이머로 남은 10%를 부드럽게 채움
                timer += Time.unscaledDeltaTime;
                targetProgress = Mathf.Lerp(0.9f, 1f, timer);
            }

            // 슬라이더 및 텍스트 업데이트
            slider.value = targetProgress;
            int percent = (int)(Mathf.Clamp01(slider.value) * 100);
            text.text = $"Loading... {percent}%";

            // 씬 활성화 조건 체크
            if (asyncOperation.progress >= 0.9f && slider.value >= 1f)
            {
                // 로딩 완료 후 씬 활성화 및 함수 종료
                asyncOperation.allowSceneActivation = true;
                return; // yield break 대신 return으로 Task 종료
            }
        }
    }
}