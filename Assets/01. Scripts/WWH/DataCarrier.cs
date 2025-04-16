using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataCarrier
{
    List<int> CharacterID = new List<int>();
    public void AddCharacterID(int Id)
    {
        CharacterID.Add(Id);
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
