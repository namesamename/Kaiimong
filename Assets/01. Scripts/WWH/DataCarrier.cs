using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier
{
    List<int> CharacterID = new List<int>();

    Character character;
    CharacterSaveData characterSaveData;


    public Character GetCharacter()
    {
        return character;
    }
    public CharacterSaveData GetSave()
    {
        return characterSaveData;
    }

    public void SetCharacter(Character character)
    {
        this.character = character;
    }

    public void SetSave(CharacterSaveData characterSaveData)
    {
        this.characterSaveData = characterSaveData;
    }
    public void RemoveAll()
    {
        CharacterID.Clear();
    }
    public void AddCharacterID(int Id)
    {

        if(CharacterID.Count >= 4)
        {
            return;
        }
        CharacterID.Add(Id);
    }
    public void RemoveLast()
    {
        CharacterID.RemoveAt(CharacterID.Count - 1);
    }

    public void RemoveIndex(int index)
    {
        CharacterID.RemoveAt(index);
    }
    public List<int> GetCharacterIDList()
    {
        return CharacterID;
    }
    public int GetCharacterIDToIndex(int index)
    {
        return CharacterID[index];
    }
}
