using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpPopUP : UIPopup
{

    int UsingGlod;
    int UsingAmulet;
    private void Awake()
    {
        GetComponentsInChildren<LevelupHUD>().Initialize();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
