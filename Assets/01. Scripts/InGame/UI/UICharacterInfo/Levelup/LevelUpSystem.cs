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
        for(int i = 1; i <= 55; i++) 
        {
            if(i  <= 10)
            {
                PlayerNeedExp.Add(100 + 100 * i);
            }
            else if (i > 10 && i <=  20)
            {
                PlayerNeedExp.Add(1100 + 50 * (i - 10));
            }
            else if (i > 20 && i <= 30)
            {
                PlayerNeedExp.Add(1600 + 30 * (i - 20));
            }
            else if (i > 30 && i <= 40)
            {
                PlayerNeedExp.Add(1900 + 200 * (i - 30));
            }
            else if (i > 40 && i <= 45)
            {
                PlayerNeedExp.Add(3900 + 500 * (i - 40) );
            }
            else if (i > 45 && i <= 50)
            {
                PlayerNeedExp.Add(6400 + 700 * (i - 45));
            }
            else if(i > 50 && i <= 55)
            {
                PlayerNeedExp.Add(9900 + 1000 * (i - 50));
            }
        }


        for (int i = 1; i <= 90; i++)
        {
            needGold.Add(
                (int)((i * 0.6 + (2.5 * i * i) + 25 * i + 100) 
                ));
            needamulet.Add(
                (int)((0.52 * (i*i*i)) + ( 6.1*(i*i)) + 40 * i + 100)
                );
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
