using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("슬라이더")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const string BGM = "배경음";
    private const string SFX = "효과음";

    private void Start()
    {
        // 슬라이더 초기값 불러오기
        float savedBGM = PlayerPrefs.GetFloat(BGM, 50f);
        float savedSFX = PlayerPrefs.GetFloat(SFX, 50f);


        Debug.Log("저장된 BGM: " + savedBGM);
        Debug.Log("저장된 SFX: " + savedSFX);

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        // 슬라이더 값이 바뀔 때마다 저장
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float value)
    {
        Debug.Log("SetBGMVolume 호출됨: " + value);
        PlayerPrefs.SetFloat(BGM, value);
    }

    private void SetSFXVolume(float value)
    {
        Debug.Log("SetSFXVolume 호출됨: " + value);
        PlayerPrefs.SetFloat(SFX, value);
    }
}
