using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class LevelUpSystem
{
    public static List<int> PlayerNeedExp = new List<int>();
    public static List<float> needGold = new List<float>();
    public static List<float> needamulet = new List<float>();
    public static List<float> needLove = new List<float>();
    public static int[] MaxLevel = new int[4] { 20, 40, 60, 90 };
    public static void Init()
    {
  
        for(int i = 1; i < 51; i++) 
        {
            if(i >= 1 && i <11)
            {
                PlayerNeedExp.Add(100 + 100 * i);
            }
            else if( 11 >= i && 21< i)
            {
                PlayerNeedExp.Add(1100 + 50 * (i - 10));
            }
            else if (21 >= i && 31 < i)
            {
                PlayerNeedExp.Add(1600 + 30 * (i-20));
            }
            else if (31 >= i && 41 < i)
            {
                PlayerNeedExp.Add(1900 + 200 * (i-30));
            }
            else if (41 >= i && 46 < i)
            {
                PlayerNeedExp.Add(3900 + 500 * (i - 40));
            }
            else if (46 >= i && 51 < i)
            {
                PlayerNeedExp.Add(6400 + 700 * (i - 45));
            }

        }



        for (int i = 1; i <= 91; i++)
        {

            needGold.Add(


               (float)( 0.6 * (i * i * i) + 2.5 * (i * i) + 25 * i) + 100


                ) ;
            needamulet.Add(
                
                (float)(0.52*(i * i * i) + 6.1*(i * i) + 100 + 40 * i)
                
                
                );
            needLove.Add(
                (float)(0.075 * (i * i * i) + 3.75 * (i * i) + 6.75 * i)
                ) ;
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
