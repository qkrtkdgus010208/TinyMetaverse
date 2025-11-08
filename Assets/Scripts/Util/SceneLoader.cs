using System.Collections.Generic;
using System.Threading.Tasks; // Task 사용을 위해 필요
using TMPro;
using Unity.VisualScripting;
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

    #region 유니테스크
    //private async void LoadSceneProgressAsync()
    //{
    //    // 1. 다음 씬에서 필요한 Addressables 에셋 주소 목록을 가져옵니다.
    //    List<string> assetKeys = GetRequiredAssets(nextScene);

    //    // 2. 씬 로드 작업 시작
    //    AsyncOperation sceneOp = SceneManager.LoadSceneAsync(nextScene.ToString());
    //    sceneOp.allowSceneActivation = false; // 자동 활성화 방지

    //    // 3. Addressables 로드 작업 시작
    //    AsyncOperationHandle<IList<GameObject>> addressablesHandle = default;
    //    if (assetKeys.Count > 0)
    //    {
    //        addressablesHandle = Addressables.LoadAssetsAsync<GameObject>(assetKeys, null, Addressables.MergeMode.Union);
    //    }

    //    float timer = 0f;

    //    // 4. 병합 로딩 루프
    //    // 🌟 sceneOp.isDone 대신 UniTask의 yield 대체 함수 사용 🌟
    //    while (!sceneOp.isDone)
    //    {
    //        // 🌟 await Task.Yield() 대신 await UniTask.Yield() 또는 UniTask.NextFrame() 사용 🌟
    //        await UniTask.Yield(); // 힙 할당이 없는(Zero-Allocation) 다음 프레임 대기
    //                               // 혹은 await UniTask.NextFrame(); 사용 가능

    //        // 5. 진행률 계산 로직 (이하 로직은 C# 표준이므로 Task/UniTask 모두 동일)

    //        float sceneProgress = sceneOp.progress; // 0.0f ~ 0.9f
    //        float addressablesProgress = 1f;

    //        if (addressablesHandle.IsValid())
    //        {
    //            addressablesProgress = addressablesHandle.PercentComplete;
    //        }

    //        float totalProgress = (sceneProgress * 0.6f) + (addressablesProgress * 0.4f);

    //        // 6. 슬라이더 스무딩 로직 적용
    //        float targetProgress;
    //        if (totalProgress < 0.99f)
    //        {
    //            targetProgress = totalProgress;
    //        }
    //        else
    //        {
    //            timer += Time.unscaledDeltaTime;
    //            targetProgress = Mathf.Lerp(totalProgress, 1f, timer);
    //        }

    //        // 7. 슬라이더 및 텍스트 업데이트
    //        slider.value = targetProgress;
    //        int percent = (int)(Mathf.Clamp01(slider.value) * 100);
    //        text.text = $"Loading... {percent}%";

    //        // 8. 씬 활성화 조건 체크
    //        if (sceneOp.progress >= 0.9f &&
    //            (!addressablesHandle.IsValid() || addressablesHandle.IsDone) &&
    //            slider.value >= 1f)
    //        {
    //            // 씬 활성화 및 함수 종료
    //            sceneOp.allowSceneActivation = true;
    //            return;
    //        }
    //    }
    //}

    //// 🌟 nextScene 값에 따라 로드할 에셋 목록을 결정하는 함수 🌟
    //private List<string> GetRequiredAssets(SceneNames scene)
    //{
    //    switch (scene)
    //    {
    //        case SceneNames.MainScene:
    //            // B 씬에 필요한 에셋 목록
    //            return new List<string> { "UI_HUD", "Model_Player", "SFX_BGM_Main" };
    //        case SceneNames.MiniGameScene:
    //            // C 씬에 필요한 에셋 목록
    //            return new List<string> { "UI_MiniGamePanel", "Texture_Map_Minigame", "Model_Enemy_Minigame" };
    //        // 다른 씬에 대한 case 추가
    //        default:
    //            return new List<string>();
    //    }
    //}
    #endregion
}