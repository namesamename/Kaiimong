using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillSO : SO
{

    public string Name;
    public bool isSingleAttack;
    public int attackCount;
    public int[] damage;
    public float[] hitTiming;
    public Sprite icon;

    public bool IsDebuff;
    public bool IsBuff;


    public int Id;

    public string SkillId;

#if UNITY_EDITOR
    public void OnValidate()
    {
        if (string.IsNullOrEmpty(SkillId) && !string.IsNullOrEmpty(Name))
        {
            string Chaguid = Guid.NewGuid().ToString().Substring(0, 8);
            SkillId = Chaguid;
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
#endif
}



