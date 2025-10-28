using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
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
