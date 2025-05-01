using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class LevelUpSystem
{
    public static List<int> PlayerNeedExp = new List<int>();
    public static List<int> needGold = new List<int>();
    public static List<int> needamulet = new List<int>();
    public static List<int> needLove = new List<int>();
    public static int[] MaxLevel = new int[4] { 20, 40, 60, 90 };
    public static void Init()
    {
        for (int i = 1; i <= 91; i++)
        {
            PlayerNeedExp.Add(i);
            needGold.Add(i);
            needamulet.Add(i);
            needLove.Add(i);
        }
    }
    public static void GainPlayerEXP(int EXP)
    {
        if (EXP <= 0)
        {
            return;
        }

        CurrencyManager.Instance.SetCurrency(CurrencyType.UserEXP, EXP);

        if(CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP) >= PlayerNeedExp[CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)])
        {
            CurrencyManager.Instance.SetCurrency(CurrencyType.UserEXP, -PlayerNeedExp[CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)]);
            CurrencyManager.Instance.UserLevelup();
        }

    }
    public static void LevelUp( int Level,int UseGold, int UseAmulet, CharacterSaveData saveData)
    {
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -UseGold);
        CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -UseAmulet);
        saveData.Level += Level;
    }
   
}
