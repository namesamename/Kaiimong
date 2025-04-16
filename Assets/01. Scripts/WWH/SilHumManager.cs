using System.Collections.Generic;
using UnityEngine;

public class SilHumManager : MonoBehaviour
{

    public GameObject InitialSil;
    public GameObject LoadSil;
  
    void Awake()
    {
        InitialSil.GetComponent<Character>().Initialize("002");
        SaveDataBase.Instance.SavingList(SaveDataBase.Instance.SaveDic[SaveType.Character],SaveType.Character);
        LoadSil.GetComponent<Character>().LoadData(
            SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character,
            InitialSil.GetComponent<Character>().CharacterSaveData.ID));

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
