using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoHUDManager : MonoBehaviour
{

    private void Awake()
    {
        
        CharacterInfoHUD[] characterInfos = GetComponentsInChildren<CharacterInfoHUD>();

        for (int i = 0; i < characterInfos.Length; i++) 
        {
            characterInfos[i].InitialIze(ImsiGameManager.Instance.GetCharacter(), ImsiGameManager.Instance.GetCharacterSaveData());
        }


    }

}
