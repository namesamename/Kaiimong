using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelupBtn : BaseLevelupInfo
{
    Button[] buttons;



    private void Start()
    {
        buttons = GetComponentsInChildren<Button>();
    }


    public void SetBtn()
    {
        //buttons[0].onClick.AddListener();
        //buttons[1].onClick.AddListener();
        //buttons[2].onClick.AddListener();
        //buttons[3].onClick.AddListener();
        //buttons[4].onClick.AddListener();
        //buttons[5].onClick.AddListener();
    }
    public void OnMax()
    {
        
    }
    public void OnPlus()
    {
        if(popUP.effect.SetPlus(1))
        {
            buttons[5].onClick.AddListener(() =>LevelUpSystem.LevelUp(popUP.LevelInterval, popUP.UsingGlod, popUP.UsingAmulet, ImsiGameManager.Instance.GetCharacterSaveData()));
        }
        popUP.UISet();
    }


    public void OnMinus()
    {
        if (popUP.effect.SetMinus(1))
        {
            buttons[5].onClick.AddListener(() => LevelUpSystem.LevelUp(popUP.LevelInterval, popUP.UsingGlod, popUP.UsingAmulet, ImsiGameManager.Instance.GetCharacterSaveData()));
        }
        popUP.UISet();
    }

}
