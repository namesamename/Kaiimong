using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier
{
    List<int> CharacterID = new List<int>();
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
