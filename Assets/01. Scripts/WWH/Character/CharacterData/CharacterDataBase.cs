using System.Collections.Generic;
using UnityEngine;

public class CharacterDataBase 
{
    public Dictionary<string , CharacterSO> characterDic = new Dictionary<string , CharacterSO>();  
    public GameObject CharacterPrefabs;


    public void Initialize()
    {
        CharacterSO[] characters = Resources.LoadAll<CharacterSO>("Char");

        foreach (CharacterSO character in characters)
        {
            characterDic[character.ID] = character;
        }
    }
   
    public CharacterSO GetCharSOToID(string characterId)
    {
        if (characterDic[characterId] != null && characterDic.ContainsKey(characterId))
        {
            return characterDic[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }


    //ĳ���� ���̵�� ����
    public GameObject CharacterInstanceSummon(CharacterSO character, Vector3 pos, Transform parent = null)
    {
        GameObject CharacterObject = Object.Instantiate(CharacterPrefabs, pos, Quaternion.identity , parent);

        if(CharacterObject.GetComponent<Character>() == null) 
        {   CharacterObject.AddComponent<Character>();}
        CharacterObject.GetComponent<Character>().Initialize(character.ID);
        return CharacterObject;

    }
    //ĳ���� ���̺� ������ ����
    public GameObject CharacterInstanceSummonFromSaveData(CharacterSaveData saveData , Vector3 pos, Transform parent = null)
    {
        GameObject Character = CharacterInstanceSummon(GetCharSOToID(saveData.ID), pos, parent);
        return Character;
    }




}
