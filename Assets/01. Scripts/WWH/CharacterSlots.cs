using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlots : MonoBehaviour
{

    Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(SetGet);
    }


    public void SetGet()
    {
        ImsiGameManager.Instance.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(1));
        ImsiGameManager.Instance.SetCharacterSaveData(SaveDataBase.Instance.
            GetSaveDataToID<CharacterSaveData>(SaveType.Character,1));
       

    }




}
