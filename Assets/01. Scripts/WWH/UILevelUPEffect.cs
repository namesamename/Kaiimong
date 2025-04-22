using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUPEffect : BaseLevelupInfo
{
    TextMeshProUGUI[] textMeshPros;
    Image[] Images;
    private void Start()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        Images = GetComponentsInChildren<Image>();

    }
    public void SetImages()
    {
        Images[0].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.Gold).IconPath);
        Images[1].sprite = Resources.Load<Sprite>(GlobalDataTable.Instance.currency.GetCurrencySOToEnum<CurrencySO>(CurrencyType.CharacterEXP).IconPath); ;
    }
    public bool SetPlus(int Level)
    {
        popUP.LevelInterval += Level;
        popUP.NextLevel = popUP.LevelInterval + popUP.CurLevel;
        
        int CurGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int CurAmulet = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        textMeshPros[0].text = popUP.CurLevel.ToString();

        if (popUP.NextLevel > popUP.CurLevel)
        {
            textMeshPros[1].text = (popUP.NextLevel).ToString();
            
            textMeshPros[2].text = LevelUpSystem.needGold[popUP.NextLevel].ToString();
            popUP.UsingGlod = LevelUpSystem.needGold[popUP.NextLevel];


            textMeshPros[3].text = LevelUpSystem.needamulet[popUP.NextLevel].ToString();
            popUP.UsingAmulet = LevelUpSystem.needamulet[popUP.NextLevel];

            textMeshPros[4].text = (popUP.NextLevel).ToString();


            if(CurGold < LevelUpSystem.needGold[popUP.CurLevel])
            {
                textMeshPros[1].color = Color.red;
                textMeshPros[2].color = Color.red;
                return false;
            }
            if (CurAmulet < LevelUpSystem.needamulet[popUP.CurLevel])
            {
                textMeshPros[1].color = Color.red;
                textMeshPros[3].color = Color.red;
                return false;
            }

            return true;
        }

        return true;
    }

    public bool SetMinus(int Level)
    {
        if(popUP.LevelInterval < 1)
        {
            return false;
        }
        int CurGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int CurAmulet = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        popUP.LevelInterval -= Level;
        popUP.NextLevel -= popUP.LevelInterval;
        textMeshPros[0].text = popUP.CurLevel.ToString();
        textMeshPros[1].text = (popUP.NextLevel).ToString();
        textMeshPros[2].text = LevelUpSystem.needGold[popUP.NextLevel].ToString();
        popUP.UsingGlod = LevelUpSystem.needGold[popUP.NextLevel];


        textMeshPros[3].text = LevelUpSystem.needamulet[popUP.NextLevel].ToString();
        popUP.UsingAmulet = LevelUpSystem.needamulet[popUP.NextLevel];
        textMeshPros[4].text = (popUP.NextLevel).ToString();
        if (CurGold < LevelUpSystem.needGold[popUP.NextLevel])
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[2].color = Color.red;
            return false;
        }
        if (CurAmulet < LevelUpSystem.needamulet[popUP.NextLevel])
        {
            textMeshPros[1].color = Color.red;
            textMeshPros[3].color = Color.red;
            return false;
        }
        return true;
    }





}
