using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static CharacterManager;

public class CharacterManager :Singleton<CharacterManager>
{

    CharacterListWrapper characterListWrapper;

    public List<CharacterSO> characterSO = new List<CharacterSO>();

    string path = Path.Combine(Application.dataPath, "database.json");

    private void Awake()
    {
        Initalize();
    }
    private void Start()
    {

        Save();
    }
    public void Initalize()
    {
        characterListWrapper = new CharacterListWrapper();
       
        CharacterSO[] characters = Resources.LoadAll<CharacterSO>("Char");
        Debug.Log(characters.Length);
        characterSO = characters.ToList();
        characterListWrapper.characterList = characterSO;
    }


    public void Save()
    {
        string Json = JsonUtility.ToJson(characterListWrapper);
        try
        {
            File.WriteAllText(path, Json);
            Debug.Log("파일 저장 완료!");
        }
        catch (Exception e)
        {
            Debug.LogError("파일 저장 실패: " + e.Message);
        }
    }


    [System.Serializable]
    public class CharacterListWrapper
    {
        public List<CharacterSO> characterList;
    }






}
