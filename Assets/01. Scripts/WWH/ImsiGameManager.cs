using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ImsiGameManager : Singleton<ImsiGameManager>
{
    Character character;
    CharacterSaveData Scharacter;


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

        Scharacter = new CharacterSaveData()
        {
            ID = 1,
            Level = 1,
            Love = 0,
            Necessity = 0,
            Recognition = 0,
            Savetype = SaveType.Character,
            IsEquiped = false,
        };

        SaveDataBase.Instance.SaveSingleData(Scharacter);

    }

    public Character GetCharacter() 
    {
        return character;
    }

    public CharacterSaveData GetCharacterSaveData() 
    {
        return Scharacter;
    }
    public void SetCharacter(Character character)
    {
        this.character = character;
        Debug.Log(character);
    }
    public void SetCharacterSaveData(CharacterSaveData character)
    {
        Scharacter = character;
    }


}
