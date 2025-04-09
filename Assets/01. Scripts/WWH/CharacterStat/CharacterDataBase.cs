using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterDataBase : Singleton<CharacterDataBase>
{
    public Dictionary<string , CharacterSO> characterDic = new Dictionary<string , CharacterSO>();  

    public List<CharacterSaveData> SaveDatas = new List<CharacterSaveData>();

    public GameObject CharacterPrefabs;

    private void Awake()
    {
        CharacterSO[] characters = Resources.LoadAll<CharacterSO>("Char");
        foreach (CharacterSO character in characters) 
        {
            characterDic[character.CharacterId] = character;
        }

    }


    public CharacterSO GetCharSOToGUID(string characterId)
    {
        if (characterDic[characterId] != null)
        {
            return characterDic[characterId];
        }
        else
        {
            Debug.Log("That Id Dont Have Character");
            return null;
        }
    }

    public GameObject CharacterInstanceSummon(CharacterSO character, Vector3 pos, Transform parent = null)
    {
        GameObject CharacterObject = Instantiate(CharacterPrefabs, pos, Quaternion.identity , parent);

        if(CharacterObject.GetComponent<Character>() == null) 
        {
            CharacterObject.AddComponent<Character>();
        }

        CharacterObject.GetComponent<Character>().Initialize(character.CharacterId);
        //세이브 데이터 가져오기
        return CharacterObject;

    }

    public GameObject CharacterInstanceSummonFromSaveData(CharacterSaveData saveData , Vector3 pos, Transform parent = null)
    {
        GameObject Character = CharacterInstanceSummon(GetCharSOToGUID(saveData.characterId), pos, parent);
        return Character;
    }



    [System.Serializable]
    public class CharacterSaveData :SaveInstance
    {
        public string characterId;
        public int Level;
        public int Recognition;
        public int Necessity;

    }
}
