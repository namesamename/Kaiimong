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


    public void initialize()
    {
        buttons[0].onClick.AddListener(OnMax);
        buttons[1].onClick.AddListener(OnPlus);
        buttons[2].onClick.AddListener(OnMin);
        buttons[3].onClick.AddListener(OnMinus);
        buttons[4].onClick.AddListener(OnDown);

    }
    public void OnMax()
    {
        
    }
    public void OnMin()
    {

    }
    public void OnPlus()
    {
        buttons[5].onClick.RemoveAllListeners();

        if(popUP.effect.SetPlus(1))
        {
            buttons[5].onClick.AddListener(Excute);
            buttons[5].interactable = true;
        }
        else
        {
            buttons[5].interactable = true;
        }

    }
    public void OnMinus()
    {
        buttons[5].onClick.RemoveAllListeners();
        if (popUP.effect.SetMinus(1))
        {
            buttons[5].onClick.AddListener(Excute);
            buttons[5].interactable = true;
        }
        else
        {
            buttons[5].interactable = true;
        }

    
    }
    public void OnDown()
    {
        popUP.UISet();
        popUP.Hide();
    }
    public void Excute()
    {
        LevelUpSystem.LevelUp(popUP.LevelInterval, popUP.UsingGlod, popUP.UsingAmulet, ImsiGameManager.Instance.GetCharacterSaveData());
        popUP.Initialize();
    }
}
