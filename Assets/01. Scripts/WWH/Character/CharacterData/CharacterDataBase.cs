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
   
    public CharacterSO GetCharSOToGUID(string characterId)
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

    public GameObject CharacterInstanceSummon(CharacterSO character, Vector3 pos, Transform parent = null)
    {
        GameObject CharacterObject = Object.Instantiate(CharacterPrefabs, pos, Quaternion.identity , parent);

        if(CharacterObject.GetComponent<Character>() == null) 
        {   CharacterObject.AddComponent<Character>();}
        CharacterObject.GetComponent<Character>().Initialize(character.ID);
        return CharacterObject;

    }

    public GameObject CharacterInstanceSummonFromSaveData(CharacterSaveData saveData , Vector3 pos, Transform parent = null)
    {
        GameObject Character = CharacterInstanceSummon(GetCharSOToGUID(saveData.characterId), pos, parent);
        return Character;
    }




}
