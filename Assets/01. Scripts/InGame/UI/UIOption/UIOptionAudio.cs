using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionAudio : MonoBehaviour
{

    Slider[] slider;


    private void Awake()
    {
        slider = GetComponentsInChildren<Slider>();
    }





    private void Start()
    {
        for (int i = 0; i < slider.Length; i++) 
        {
            slider[i].onValueChanged.RemoveAllListeners();
        }
        slider[0].onValueChanged.AddListener(BGM);
        slider[1].onValueChanged.AddListener(FX);

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
