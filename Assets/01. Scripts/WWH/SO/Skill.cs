using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public enum SkillType
{
    attk,
    debuff,
    buff,
    heal
}

public enum SkillTargetType
{ 
    enemy,
    our
}



public class Skill : SO
{
    public SkillType Type;
    public SkillTargetType Target;
    public int TargetCount;
    public float Attack;

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



