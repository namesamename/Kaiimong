using System.Collections.Generic;
using UnityEngine;

public class SilHumManager : MonoBehaviour
{

    public GameObject InitialSil;
    public GameObject LoadSil;
  
    void Awake()
    {
        InitialSil.GetComponent<CharacterCarrier>().Initialize(2);
        SaveDataBase.Instance.SavingList(SaveDataBase.Instance.SaveDic[SaveType.Character],SaveType.Character);
        LoadSil.GetComponent<CharacterCarrier>().LoadData(
            SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character,
            InitialSil.GetComponent<CharacterCarrier>().CharacterSaveData.ID));

        CurrencyManager.Instance.InitialIze();
        Debug.Log("���" + CurrencyManager.Instance.GetCurrency(CurrencyType.Gold));
        Debug.Log("�ൿ��" +CurrencyManager.Instance.GetCurrency(CurrencyType.Activity));
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, 100);
        Debug.Log("���" + CurrencyManager.Instance.GetCurrency(CurrencyType.Gold));
        Debug.Log("�ൿ��" + CurrencyManager.Instance.GetCurrency(CurrencyType.Activity));
        CurrencyManager.Instance.Save();

        SaveDataBase.Instance.SaveAll();




    }
}
