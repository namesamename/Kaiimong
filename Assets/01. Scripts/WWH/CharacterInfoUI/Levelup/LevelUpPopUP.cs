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


    public ISetPOPUp[] setPOPUps;



    private void Awake()
    {
       
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();
        btn = GetComponentInChildren<UILevelupBtn>();
        effect = GetComponentInChildren<UILevelUPEffect>();
        stat = GetComponentInChildren<UILevelupStatboard>();
 
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetDefault();
        for (int i = 0; i < setPOPUps.Length; i++)
        {
            setPOPUps[i].Initialize();
        }
    }

    public void SetDefault()
    {
        UsingGlod = 0;
        UsingAmulet = 0;
        LevelInterval = 0;
        CurLevel = GlobalDataTable.Instance.DataCarrier.GetSave().Level;
        NextLevel = CurLevel;
    }

    public void UISet()
    {
      CharacterInfoHUDManager.Instance.Initialize();
    }



}
