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

    public string buffSkillId;
    public bool IsMuti;
    public bool IsHeal;
    public bool IsBuff;
    public int Id;

}



