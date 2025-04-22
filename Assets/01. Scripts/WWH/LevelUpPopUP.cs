using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopUP : UIPopup
{

    public int UsingGlod;
    public int UsingAmulet;
    public int CurLevel;
    public int NextLevel;
    public int LevelInterval;

    public UILevelupBtn btn;
    public UILevelUPEffect effect;
    public UILevelupStatboard stat;

    public CharacterInfoHUDManager characterInfoHUDManager;

    private void Awake()
    {
        characterInfoHUDManager = FindAnyObjectByType<CharacterInfoHUDManager>();
        btn = GetComponentInChildren<UILevelupBtn>();
        effect = GetComponentInChildren<UILevelUPEffect>();
        stat = GetComponentInChildren<UILevelupStatboard>();
        CurLevel = ImsiGameManager.Instance.GetCharacterSaveData().Level;
        NextLevel = CurLevel;
    }

    public void SetDefault()
    {
        UsingGlod = 0;
        UsingAmulet = 0;
        LevelInterval = 0;
        CurLevel = ImsiGameManager.Instance.GetCharacterSaveData().Level;
        NextLevel = CurLevel;
    }

    public void UISet()
    {
        characterInfoHUDManager.Initialize();
    }



}
