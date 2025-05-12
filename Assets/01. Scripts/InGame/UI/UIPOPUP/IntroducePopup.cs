using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroducePopup : UIPOPUP
{
    InputField input;

    Button[] buttons;


    CharacterProfileStatusMessage statusMessage;

    private void Awake()
    {
        input = GetComponentInChildren<InputField>();

        buttons = GetComponentsInChildren<Button>();
        statusMessage = FindAnyObjectByType<CharacterProfileStatusMessage>();
    }


    public void SetBtn()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(Destroy);
        buttons[1].onClick.AddListener(Confirm);
        buttons[2].onClick.AddListener(Destroy);


    }


    public void Confirm()
    {
        statusMessage.SetIntroduce(input.text);
        Destroy();
       
    }




}
