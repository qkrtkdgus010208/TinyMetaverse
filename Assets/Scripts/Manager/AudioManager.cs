using System.Collections;
using UnityEngine;

public enum Bgm { StartBgm, MainBgm, GameBgm }

public class AudioManager : Singleton<AudioManager>
{
    [Header("# BGM")]
    [SerializeField] private AudioClip[] bgmClips;
    [SerializeField][Range(0f, 1f)] private float bgmVolume = 0.1f;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        PlayBgm(Bgm.StartBgm);
    }

    public void PlayBgm(Bgm bgm)
    {
        audioSource.clip = bgmClips[(int)bgm];
        audioSource.loop = true;
        audioSource.Play();

        StartCoroutine(FadeInBgm());
    }

    IEnumerator FadeInBgm()
    {
        float timer = 0f;
        float fadeTime = 5f; // BGM이 커지는 데 걸리는 시간
        float startVolume = 0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, bgmVolume, timer / fadeTime);
            yield return null;
        }

        // 최종 볼륨으로 설정
        audioSource.volume = bgmVolume;
    }

    public void StopBgm()
    {
        StopAllCoroutines();
        audioSource.Stop();
    }
}
