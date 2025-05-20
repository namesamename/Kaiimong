using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    AudioSaveData saveData = new AudioSaveData();

    [Header("BGM Settings")]
    [SerializeField]
    private UnityEngine.AudioSource bgmSource; // ��� ���ǿ� ����� �ҽ�

    [Range(0f, 1f)] public float bgmVolume = 1f; // �������(BGM)�� ����

    [SerializeField] private List<AudioClip> bgmClips; // ��� ���� Ŭ�� ����Ʈ

    [Header("SFX Settings")]
    [SerializeField]
    [Range(0f, 1f)]
    private float sfxVolume = 1f; // ȿ���� ����

    [SerializeField][Range(0f, 1f)] private float sfxPitchVariance; // ȿ���� ��ġ ���� ����

    public AudioSource audioSourcePrefab; // ȿ���� ����� ���� ���� �ҽ� ������

    [SerializeField] private bool isMuted; // ���Ұ� ���� ����

    private Coroutine fadeCoroutine; // ���� Ȱ��ȭ�� ���̵� �ڷ�ƾ

    protected void Start()
    {
        bgmSource = gameObject.GetOrAddComponent<UnityEngine.AudioSource>();
        ApplyVolumeSettings(); // �ʱ� ���� ����
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

        // �̸����� BGM ã��
        AudioClip clip = bgmClips.Find(c => c.name == clipName);

        if (clip != null && bgmSource.clip != clip)
        {
            bgmSource.Stop(); // ���� ��� ���� ����
            bgmSource.clip = clip; // ���ο� ��� ���� ����
            bgmSource.Play(); // ��� ���� ���
        }
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public static void PlaySFX(AudioClip clip)
    {
        if (Instance.isMuted || !clip) return;

        AudioSource obj = Instantiate(Instance.audioSourcePrefab); // ���ο� ���� �ҽ� ������Ʈ ����
        AudioSource soundSource = obj.GetComponent<AudioSource>(); // ���� �ҽ� ��������
        soundSource.Play(clip, Instance.sfxVolume, Instance.sfxPitchVariance); // ȿ���� ���
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
        // ���� ���� ���� ���̵� �ڷ�ƾ �ߴ� (�ߺ� ���� ����)
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
        bgmSource.volume = 0; // ������ 0���� ����
    }

    // ������� ���̵���
    private IEnumerator FadeInBGM(string clipName, float fadeDuration)
    {
        // ���ο� Ŭ�� ��������
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

        bgmSource.volume = bgmVolume; // ���� ���� ����
    }

    //BGM ���� �� ���̵� ó�� (�ܺ� �޼��� ȣ��)
    public void ChangeBGMWithFade(string clipName, float fadeDuration)
    {
        // ���� �ڷ�ƾ �ߴ� �� ���ο� �ڷ�ƾ ����
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(ChangeBGMCoroutine(clipName, fadeDuration));
    }

    // ���̵� �ƿ� �� ���̵� ��
    private IEnumerator ChangeBGMCoroutine(string clipName, float fadeDuration)
    {
        float initialVolume = bgmSource.volume;

        // Ŭ���� ���� �̹� ��� ���̶�� ���̵�ƿ� ����
        if (bgmSource.isPlaying && bgmSource.clip && (bgmSource.clip.name == clipName))
        {
            bgmSource.volume = initialVolume; // ���� ���� ����
        }
        else
        {
            yield return FadeOutBGM(fadeDuration); // ���� ��� ���� ���� ���̵�ƿ�
        }

        yield return FadeInBGM(clipName, fadeDuration); // ���ο� ���� ���̵���
    }

    // ���������� ���̵� �ƿ� ����
    public void StartFadeOut(float fadeDuration)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(FadeOutBGM(fadeDuration));
    }

    // ���������� ���̵� �� ����
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
