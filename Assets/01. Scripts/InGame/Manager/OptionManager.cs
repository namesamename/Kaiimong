using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("�����̴�")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const string BGM = "�����";
    private const string SFX = "ȿ����";

    private void Start()
    {
        // �����̴� �ʱⰪ �ҷ�����
        float savedBGM = PlayerPrefs.GetFloat(BGM, 50f);
        float savedSFX = PlayerPrefs.GetFloat(SFX, 50f);


        Debug.Log("����� BGM: " + savedBGM);
        Debug.Log("����� SFX: " + savedSFX);

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        // �����̴� ���� �ٲ� ������ ����
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float value)
    {
        Debug.Log("SetBGMVolume ȣ���: " + value);
        PlayerPrefs.SetFloat(BGM, value);
    }

    private void SetSFXVolume(float value)
    {
        Debug.Log("SetSFXVolume ȣ���: " + value);
        PlayerPrefs.SetFloat(SFX, value);
    }
}
