using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestInsideBtn : MonoBehaviour
{

    Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    public void SetBtn(Action action, Action action1)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.RemoveAllListeners();
        }


        buttons[0].onClick.AddListener(() => action());
        buttons[1].onClick.AddListener(() => action1());



    }


}
