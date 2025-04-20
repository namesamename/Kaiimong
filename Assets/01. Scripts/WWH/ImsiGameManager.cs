using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImsiGameManager : Singleton<ImsiGameManager>
{
    Character character;
    CharacterSaveData characterSaveData;


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public Character GetCharacter() 
    {
        return character;
    }

    public CharacterSaveData GetCharacterSaveData() 
    {
        return characterSaveData;
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
    }
    public void SetCharacterSaveData(CharacterSaveData character)
    {
        characterSaveData = character;
    }


}
