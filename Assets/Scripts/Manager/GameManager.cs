using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadMainScene()
    {
        SceneLoader.LoadScene(SceneNames.MainScene);
        AudioManager.Instance.PlayBgm(Bgm.MainBgm);
    }

    public void LoadMiniGameScene()
    {
        SceneLoader.LoadScene(SceneNames.MiniGameScene);
        AudioManager.Instance.PlayBgm(Bgm.GameBgm);
    }
}
