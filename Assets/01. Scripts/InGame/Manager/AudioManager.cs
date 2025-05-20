using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    AudioSaveData saveData = new AudioSaveData();

    [Header("BGM Settings")]
    [SerializeField]
    private UnityEngine.AudioSource bgmSource; // 배경 음악용 오디오 소스

    [Range(0f, 1f)] public float bgmVolume = 1f; // 배경음악(BGM)의 볼륨

    [SerializeField] private List<AudioClip> bgmClips; // 배경 음악 클립 리스트

    [Header("SFX Settings")]
    [SerializeField]
    [Range(0f, 1f)]
    private float sfxVolume = 1f; // 효과음 볼륨

    [SerializeField][Range(0f, 1f)] private float sfxPitchVariance; // 효과음 피치 변동 범위

    public AudioSource audioSourcePrefab; // 효과음 재생을 위한 사운드 소스 프리팹

    [SerializeField] private bool isMuted; // 음소거 설정 여부

    private Coroutine fadeCoroutine; // 현재 활성화된 페이드 코루틴

    protected void Start()
    {
        bgmSource = gameObject.GetOrAddComponent<UnityEngine.AudioSource>();
        ApplyVolumeSettings(); // 초기 볼륨 설정
    }


    public void Initialize()
    {
        if(SaveDataBase.Instance.GetSaveDataToID<AudioSaveData>(SaveType.Audio, 0) != null)
        {
            var data = SaveDataBase.Instance.GetSaveDataToID<AudioSaveData>(SaveType.Audio, 0);
            saveData.ID = data.ID;
            saveData.Savetype = data.Savetype;
            saveData.BGMVolume = data.BGMVolume;
            saveData.SFXVolume = data.SFXVolume;
            saveData.IsMute = data.IsMute;


            SetMute(saveData.IsMute);

            bgmVolume = saveData.BGMVolume;
            sfxVolume = saveData.SFXVolume;

            SetBGMVolume(bgmVolume);
            SetSFXVolume(sfxVolume);

            PlayBGM("Piano Instrumental 5");
        }
        else
        {

            AudioSaveData audio = new AudioSaveData()
            { 
                SFXVolume = 0.3f,
                BGMVolume = 0.3f,
                Savetype = SaveType.Audio,
                IsMute = false,
                ID = 0,
            };

            saveData.ID = audio.ID;
            saveData.Savetype = audio.Savetype;
            saveData.BGMVolume = audio.BGMVolume;
            saveData.SFXVolume = audio.SFXVolume;
            saveData.IsMute = audio.IsMute;

            bgmVolume = saveData.BGMVolume;
            sfxVolume = saveData.SFXVolume;

            SetMute(false);
            SetBGMVolume(bgmVolume);
            SetSFXVolume(sfxVolume);

            PlayBGM("Piano Instrumental 5");

        }
    }


    private void ApplyVolumeSettings()
    {
        bgmSource.volume = isMuted ? 0f : bgmVolume;
        bgmSource.loop = true;
        //isMuted | isSfxMuted? 0f : sfxVolume;
    }

    public void PlayBGM(string clipName)
    {
        if (isMuted) return;

        // 이름으로 BGM 찾기
        AudioClip clip = bgmClips.Find(c => c.name == clipName);

        if (clip != null && bgmSource.clip != clip)
        {
            bgmSource.Stop(); // 현재 배경 음악 정지
            bgmSource.clip = clip; // 새로운 배경 음악 설정
            bgmSource.Play(); // 배경 음악 재생
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public static void PlaySFX(AudioClip clip)
    {
        if (Instance.isMuted || !clip) return;

        AudioSource obj = Instantiate(Instance.audioSourcePrefab); // 새로운 사운드 소스 오브젝트 생성
        AudioSource soundSource = obj.GetComponent<AudioSource>(); // 사운드 소스 가져오기
        soundSource.Play(clip, Instance.sfxVolume, Instance.sfxPitchVariance); // 효과음 재생
    }

    public void SetMute(bool mute)
    {
        isMuted = mute;
        saveData.IsMute = mute;
        SaveDataBase.Instance.SaveSingleData(saveData);
        ApplyVolumeSettings();
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = Mathf.Clamp(volume, 0f, 1f);
        saveData.BGMVolume = bgmVolume;
        SaveDataBase.Instance.SaveSingleData(saveData);

        ApplyVolumeSettings();
    }

    public void SetSFXVolume(float volume)
    {

        sfxVolume = Mathf.Clamp(volume, 0f, 1f);
        saveData.SFXVolume = sfxVolume;
        SaveDataBase.Instance.SaveSingleData(saveData);

        ApplyVolumeSettings();
    }

    private IEnumerator FadeOutBGM(float fadeDuration)
    {
        // 현재 실행 중인 페이드 코루틴 중단 (중복 실행 방지)
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        float startVolume = bgmSource.volume;

        while (bgmSource.volume > 0)
        {
            bgmSource.volume =
                Mathf.MoveTowards(bgmSource.volume, 0f, (startVolume / fadeDuration) * Time.deltaTime);
            yield return null;
        }

        bgmSource.Stop();
        bgmSource.volume = 0; // 완전히 0으로 설정
    }

    // 배경음악 페이드인
    private IEnumerator FadeInBGM(string clipName, float fadeDuration)
    {
        // 새로운 클립 가져오기
        AudioClip newClip = bgmClips.Find(c => c.name == clipName);

        if (!newClip)
        {
            Debug.LogError($"SoundManager: BGM '{clipName}' not found.");
            yield break;
        }

        if (bgmSource.clip != newClip | !bgmSource.isPlaying)
        {
            bgmSource.clip = newClip;
            bgmSource.volume = 0;
            bgmSource.Play();
        }


        while (bgmSource.volume < bgmVolume)
        {
            bgmSource.volume = Mathf.MoveTowards(bgmSource.volume, bgmVolume,
                (bgmVolume / fadeDuration) * Time.deltaTime);
            yield return null;
        }

        bgmSource.volume = bgmVolume; // 최종 볼륨 고정
    }

    //BGM 변경 및 페이드 처리 (외부 메서드 호출)
    public void ChangeBGMWithFade(string clipName, float fadeDuration)
    {
        // 기존 코루틴 중단 후 새로운 코루틴 시작
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(ChangeBGMCoroutine(clipName, fadeDuration));
    }

    // 페이드 아웃 후 페이드 인
    private IEnumerator ChangeBGMCoroutine(string clipName, float fadeDuration)
    {
        float initialVolume = bgmSource.volume;

        // 클립이 같고 이미 재생 중이라면 페이드아웃 생략
        if (bgmSource.isPlaying && bgmSource.clip && (bgmSource.clip.name == clipName))
        {
            bgmSource.volume = initialVolume; // 현재 볼륨 유지
        }
        else
        {
            yield return FadeOutBGM(fadeDuration); // 현재 재생 중인 음악 페이드아웃
        }

        yield return FadeInBGM(clipName, fadeDuration); // 새로운 음악 페이드인
    }

    // 독립적으로 페이드 아웃 시작
    public void StartFadeOut(float fadeDuration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutBGM(fadeDuration));
    }

    // 독립적으로 페이드 인 시작
    public void StartFadeIn(string clipName, float fadeDuration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeInBGM(clipName, fadeDuration));
    }

    public bool GetMute()
    {
        return isMuted;
    }
}
