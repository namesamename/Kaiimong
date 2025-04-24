using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoHUDManager : MonoBehaviour
{
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        CharacterInfoHUD[] characterInfos = GetComponentsInChildren<CharacterInfoHUD>();

        Debug.Log(ImsiGameManager.Instance.GetCharacter());
        Debug.Log(ImsiGameManager.Instance.GetCharacterSaveData());
        for (int i = 0; i < characterInfos.Length; i++)
        {
            characterInfos[i].InitialIze(ImsiGameManager.Instance.GetCharacter(), ImsiGameManager.Instance.GetCharacterSaveData());
        }
    }

}
