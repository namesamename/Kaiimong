using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileText : MonoBehaviour
{

    TextMeshProUGUI[] textMeshPros;
    Image image;

    int Clearcount = 0;
    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        image = GetComponentInChildren<Image>();
    }


    public void Initialize()
    {
        TextSet();
    }


    public void TextSet()
    {
        textMeshPros[0].text = $"{SaveDataBase.Instance.GetSaveDataToID<CurrencySaveData>(SaveType.Currency, 0).UserName}/{SaveDataBase.Instance.GetSaveDataToID<CurrencySaveData>(SaveType.Currency, 0).UserLevel}";
        textMeshPros[1].text = $"{SaveDataBase.Instance.GetSaveDataToID<CurrencySaveData>(SaveType.Currency, 0).date}";

        List<StageSaveData> stageSaveDatas = SaveDataBase.Instance.GetSaveInstanceList<StageSaveData>(SaveType.Stage);

        foreach (StageSaveData data in stageSaveDatas)
        {
            if (data.ClearedStage)
            {
                Clearcount++;
            }

        }


        textMeshPros[2].text = $"{Clearcount}/{GlobalDataTable.Instance.Stage.GetStage()}";


        



    }


}
