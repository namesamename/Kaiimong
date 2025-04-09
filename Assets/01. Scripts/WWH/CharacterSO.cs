using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class CharacterSO : SO
{
    public string Name;
    public string Grade;
    public int Health;
    public int Attack;
    public int Defense;
    public int Speed;
    public string CriticalPer;
    public string CriticalAttack;

    public string CharacterId;


#if UNITY_EDITOR
    public void OnValidate()
    {
        if(string.IsNullOrEmpty(CharacterId) && !string.IsNullOrEmpty(Name))
        {
           string Chaguid = Guid.NewGuid().ToString().Substring(0,8);
            CharacterId = Chaguid;
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}
