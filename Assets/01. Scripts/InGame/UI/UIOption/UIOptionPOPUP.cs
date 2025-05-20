using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIOptionPOPUP : UIPOPUP
{

    Button[] buttons;
    Slider[] sliders;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        sliders = GetComponentsInChildren<Slider>();
    }

    private void Start()
    {
        for (int i = 0; i < sliders.Length; i++)
        {
            sliders[i].onValueChanged.RemoveAllListeners();
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener(Destroy);
        }
        sliders[0].onValueChanged.AddListener(BGM);
        sliders[1].onValueChanged.AddListener(FX);
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
