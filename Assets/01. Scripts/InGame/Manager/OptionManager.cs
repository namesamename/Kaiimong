using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("슬라이더")]
    public Slider bgmSlider;    //배경음 슬라이더
    public Slider sfxSlider;    //효과음 슬라이더

    [Header("오디오 믹서")]
    public AudioMixer audioMixer;   // AudioMixer 에셋 연결

   
    private const string BGM = "BGM"; // PlayerPrefs에 저장할 키
    private const string SFX = "SFX";

    private const string MIXER_BGM = "BGMVolume"; // AudioMixer 파라미터 이름
    private const string MIXER_SFX = "SFXVolume";
    private void Start()
    {
        // 슬라이더 초기값 불러오기
        float savedBGM = PlayerPrefs.GetFloat(BGM, 50f);
        float savedSFX = PlayerPrefs.GetFloat(SFX, 50f);



        // 슬라이더에 저장된 값을 반영

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        // AudioMixer에 값 반영

        SetBGMVolume(savedBGM);
        SetSFXVolume(savedSFX);

        // 슬라이더 값이 바뀔 때마다 저장

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float value)
    {

        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 100f) / 100f) * 20f; // 슬라이더 값(0~100)을 데시벨 단위로 변환 (-80 ~ 0dB)
        audioMixer.SetFloat(MIXER_BGM, volume);                                     // AudioMixer의 BGM 볼륨 파라미터에 반영
    }

    private void SetSFXVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 100f) / 100f) * 20f;
        audioMixer.SetFloat(MIXER_SFX, volume);
    }

    // 씬이 바뀌거나 오브젝트가 비활성화될 때 호출됨
    private void OnDisable()
    {
        // 현재 슬라이더 값을 저장
        PlayerPrefs.SetFloat(BGM, bgmSlider.value);
        PlayerPrefs.SetFloat(SFX, sfxSlider.value);
        PlayerPrefs.Save();                                  // 저장 강제 반영
    }
}
