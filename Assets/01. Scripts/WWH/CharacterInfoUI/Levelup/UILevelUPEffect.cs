using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUPEffect : BaseLevelupInfo, ISetPOPUp
{
    TextMeshProUGUI[] textMeshPros;
    Image[] Images;
    public void Initialize()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        Images = GetComponentsInChildren<Image>();
        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = popUP.NextLevel.ToString();
        textMeshPros[2].text = 0.ToString();
        textMeshPros[3].text = 0.ToString();
        textMeshPros[4].text = (popUP.CurLevel).ToString();
        CurCurrencyTextSet();
        SetImages();
    }



    public void CurCurrencyTextSet()
    { 
        textMeshPros[4].text = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold).ToString();
        textMeshPros[5].text = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP).ToString();

    }
    public void SetImages()
    {
        Images[1].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.Gold).IconPath);
        Images[3].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.Gold).IconPath);
        
        Images[2].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.CharacterEXP).IconPath);
        Images[4].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.CharacterEXP).IconPath); 
    }
    public bool SetPlus(int levelToAdd)
    {

        if (popUP.CurLevel + popUP.LevelInterval + 1 > LevelUpSystem.MaxLevel[GlobalDataTable.Instance.DataCarrier.GetSave().Necessity])
        {
            return false;
        }

        Debug.Log(popUP.LevelInterval);

        popUP.LevelInterval += levelToAdd;
        popUP.NextLevel = popUP.CurLevel + popUP.LevelInterval;
      
        popUP.stat.Statset(popUP.LevelInterval);

        int curGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int curExp = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = popUP.NextLevel.ToString();

        int requiredGold = LevelUpSystem.needGold[popUP.NextLevel];
        textMeshPros[2].text = requiredGold.ToString();
        popUP.UsingGlod = requiredGold;

        int requiredExp = LevelUpSystem.needamulet[popUP.NextLevel];
        textMeshPros[3].text = requiredExp.ToString();
        popUP.UsingAmulet = requiredExp;

        textMeshPros[6].text = $"Lv.{popUP.NextLevel}";

        bool canLevelUp = true;
        if (curGold < requiredGold)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[2].color = Color.red;
            canLevelUp = false;
        }
        else
        {

            textMeshPros[1].color = Color.black;
            textMeshPros[2].color = Color.black;
        }   


        if (curExp < requiredExp)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[3].color = Color.red;
            canLevelUp = false;
        }
        else
        {

            if (canLevelUp)
            {
                textMeshPros[1].color = Color.black;
            }
            textMeshPros[3].color = Color.black;
        }

        return canLevelUp;
    }

    public bool SetMinus(int levelToSubtract)
    {
        // 레벨 간격이 1보다 작으면 더 이상 감소 불가
        if (popUP.LevelInterval < 1 )
        {
            return false;
        }
        popUP.LevelInterval -= levelToSubtract;
        popUP.LevelInterval = Mathf.Max(0, popUP.LevelInterval);
        popUP.NextLevel = popUP.CurLevel + popUP.LevelInterval;
        popUP.stat.Statset(popUP.LevelInterval);
        int curGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int curExp = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = popUP.NextLevel.ToString();


        int requiredGold = popUP.NextLevel > popUP.CurLevel ? LevelUpSystem.needGold[popUP.NextLevel] : 0;
        textMeshPros[2].text = requiredGold.ToString();
        popUP.UsingGlod = requiredGold;

        int requiredExp = popUP.NextLevel > popUP.CurLevel ? LevelUpSystem.needamulet[popUP.NextLevel] : 0;
        textMeshPros[3].text = requiredExp.ToString();
        popUP.UsingAmulet = requiredExp;


        textMeshPros[6].text = $"Lv.{popUP.NextLevel}";


        if (popUP.LevelInterval == 0)
        {

            textMeshPros[1].color = Color.black;
            textMeshPros[2].color = Color.black;
            textMeshPros[3].color = Color.black;
            return false;
        }

        bool canLevelUp = true;


        if (curGold < requiredGold)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[2].color = Color.red;
            canLevelUp = false;
        }
        else
        {
            textMeshPros[1].color = Color.black;
            textMeshPros[2].color = Color.black;
        }
        if (curExp < requiredExp)
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[3].color = Color.red;
            canLevelUp = false;
        }
        else
        {
            if (canLevelUp)
            {
                textMeshPros[1].color = Color.black;
            }
            textMeshPros[3].color = Color.black;
        }

        return canLevelUp;
    }


    public int CalculateMaxCurrency(int curent, bool IsGold)
    {
        int MaxLevel = popUP.CurLevel;
        int total = 0;
       for(int i = MaxLevel + 1; i < LevelUpSystem.MaxLevel[GlobalDataTable.Instance.DataCarrier.GetSave().Necessity]; i++) 
        {
            total += IsGold ? LevelUpSystem.needGold[i] : LevelUpSystem.needamulet[i];
            if(total > curent)
            {
                break;
            }
            MaxLevel = i;
        }


       return MaxLevel - popUP.CurLevel;

    }


}
