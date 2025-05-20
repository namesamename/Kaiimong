using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionAudio : MonoBehaviour
{

    Slider[] slider;
    Toggle toggle;

    private void Awake()
    {
        slider = GetComponentsInChildren<Slider>();
        toggle = GetComponentInChildren<Toggle>();
    }





    private void Start()
    {
        for (int i = 0; i < slider.Length; i++) 
        {
            slider[i].onValueChanged.RemoveAllListeners();
        }
        slider[0].onValueChanged.AddListener(BGM);
        slider[1].onValueChanged.AddListener(FX);


        
        
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(Mute);

    }


    public void Mute(bool Ismute)
    {
        AudioManager.Instance.SetMute(Ismute);
        toggle.isOn = AudioManager.Instance.GetMute();



    }

    public void FX(float Volume)
    {
        float Vol = Volume / 100;

        AudioManager.Instance.SetSFXVolume(Vol);
    }


    public void BGM(float Volume)
    {
        float Vol = Volume / 100;
        AudioManager.Instance.SetBGMVolume(Vol);
    }






}
