using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager :Singleton<CharacterManager>
{

    public List<CharacterSO> characterList = new List<CharacterSO>();

    public void AddCharacter(CharacterSO character)
    {
        characterList.Add(character);
    }
    public void RemoveCharacter(CharacterSO character)
    {
        characterList.Remove(character);
    }
    public CharacterSO GetCharacter(int index)
    {
        return characterList[index];
    }




}
