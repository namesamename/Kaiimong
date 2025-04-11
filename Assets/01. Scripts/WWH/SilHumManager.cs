using System.Collections.Generic;
using UnityEngine;

public class SilHumManager : MonoBehaviour
{

    public GameObject InitialSil;
    public GameObject LoadSil;
  
    void Start()
    {
        InitialSil.GetComponent<Character>().Initialize("002");
        GameSaveSystem.SaveDatas(SaveDataBase.Instance.SaveDatas[SaveType.Character]);
        LoadSil.GetComponent<Character>().LoadData(SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character,
            InitialSil.GetComponent<Character>().characterId));



    }
}
