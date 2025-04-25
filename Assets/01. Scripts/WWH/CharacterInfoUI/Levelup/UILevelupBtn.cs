using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelupBtn : BaseLevelupInfo,ISetPOPUp
{
    Button[] buttons;


    public void Initialize()
    {
        buttons = GetComponentsInChildren<Button>();
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(OnMax);
        buttons[1].onClick.AddListener(OnPlus);
        buttons[2].onClick.AddListener(OnMin);
        buttons[3].onClick.AddListener(OnMinus);
        buttons[4].onClick.AddListener(OnDown);

    }
    public void OnMax()
    {
        int MaxEXP = popUP.effect.CalculateMaxCurrency(CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP), false);
        int MaxGold = popUP.effect.CalculateMaxCurrency(CurrencyManager.Instance.GetCurrency(CurrencyType.Gold), true);

        int maxPossibleLevels = Mathf.Min(MaxGold, MaxEXP);

        popUP.LevelInterval = 1;

        if (maxPossibleLevels > 0)
        {
            bool canLevelUp = popUP.effect.SetPlus(maxPossibleLevels);

            if (canLevelUp)
            {
                buttons[5].onClick.AddListener(Excute);
                buttons[5].interactable = true;
  
            }
            else
            {
                buttons[5].interactable = false;
            }
        }
        else
        {
            // 레벨업 불가능
            buttons[5].interactable = false;
        }

    }
    public void OnMin()
    {

        popUP.LevelInterval = 0;
        buttons[5].onClick.RemoveAllListeners();

        if (popUP.effect.SetPlus(1))
        {
            buttons[5].onClick.AddListener(Excute);
            buttons[5].interactable = true;
       
        }
        else
        {
            buttons[5].interactable = false;
        }
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
            buttons[5].interactable = false;
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
            buttons[5].interactable = false;
        }

    
    }
    public void OnDown()
    {
        popUP.UISet();
        popUP.Destroy();
    }
    public void Excute()
    {
        
        LevelUpSystem.LevelUp(popUP.LevelInterval, popUP.UsingGlod, popUP.UsingAmulet, GlobalDataTable.Instance.DataCarrier.GetSave());
        SaveDataBase.Instance.SaveSingleData(GlobalDataTable.Instance.DataCarrier.GetSave());
        popUP.Initialize();

    }
}
