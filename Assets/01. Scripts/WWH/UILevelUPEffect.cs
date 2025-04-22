using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevelUPEffect : MonoBehaviour
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
    public void SetLevel(int Level , bool IsOk, int needGold, int needAmulet)
    {
        textMeshPros[0].text = ImsiGameManager.Instance.GetCharacterSaveData().Level.ToString();

        if(Level != 0)
        {
            textMeshPros[1].text = (ImsiGameManager.Instance.GetCharacterSaveData().Level + Level).ToString();
            textMeshPros[2].text = needGold.ToString();
            textMeshPros[3].text = needAmulet.ToString();
            textMeshPros[4].text = (ImsiGameManager.Instance.GetCharacterSaveData().Level + Level).ToString();


            if(!IsOk)
            {
                textMeshPros[1].color = Color.red;
                textMeshPros[2].color = Color.red;
                textMeshPros[3].color = Color.red;
            }
        }





    }





}
