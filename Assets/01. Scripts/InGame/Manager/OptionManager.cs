using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    [Header("�����̴�")]
    public Slider bgmSlider;    //����� �����̴�
    public Slider sfxSlider;    //ȿ���� �����̴�

    [Header("����� �ͼ�")]
    public AudioMixer audioMixer;   // AudioMixer ���� ����

   
    private const string BGM = "BGM"; // PlayerPrefs�� ������ Ű
    private const string SFX = "SFX";

    private const string MIXER_BGM = "BGMVolume"; // AudioMixer �Ķ���� �̸�
    private const string MIXER_SFX = "SFXVolume";
    private void Start()
    {
        // �����̴� �ʱⰪ �ҷ�����
        float savedBGM = PlayerPrefs.GetFloat(BGM, 50f);
        float savedSFX = PlayerPrefs.GetFloat(SFX, 50f);



        // �����̴��� ����� ���� �ݿ�

        bgmSlider.value = savedBGM;
        sfxSlider.value = savedSFX;

        // AudioMixer�� �� �ݿ�

        SetBGMVolume(savedBGM);
        SetSFXVolume(savedSFX);

        // �����̴� ���� �ٲ� ������ ����

        bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void SetBGMVolume(float value)
    {

        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 100f) / 100f) * 20f; // �����̴� ��(0~100)�� ���ú� ������ ��ȯ (-80 ~ 0dB)
        audioMixer.SetFloat(MIXER_BGM, volume);                                     // AudioMixer�� BGM ���� �Ķ���Ϳ� �ݿ�
    }

    private void SetSFXVolume(float value)
    {
        float volume = Mathf.Log10(Mathf.Clamp(value, 0.0001f, 100f) / 100f) * 20f;
        audioMixer.SetFloat(MIXER_SFX, volume);
    }

    // ���� �ٲ�ų� ������Ʈ�� ��Ȱ��ȭ�� �� ȣ���
    private void OnDisable()
    {
        // ���� �����̴� ���� ����
        PlayerPrefs.SetFloat(BGM, bgmSlider.value);
        PlayerPrefs.SetFloat(SFX, sfxSlider.value);
        PlayerPrefs.Save();                                  // ���� ���� �ݿ�
    }
}
