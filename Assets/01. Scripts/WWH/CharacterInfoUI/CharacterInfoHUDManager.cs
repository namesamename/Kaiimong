using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfoHUDManager : Singleton<CharacterInfoHUDManager>
{
    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        CharacterInfoHUD[] characterInfos = GetComponentsInChildren<CharacterInfoHUD>();

        for (int i = 0; i < characterInfos.Length; i++)
        {
            characterInfos[i].InitialIze(GlobalDataTable.Instance.DataCarrier.GetCharacter(), GlobalDataTable.Instance.DataCarrier.GetSave());
        }
    }

}
