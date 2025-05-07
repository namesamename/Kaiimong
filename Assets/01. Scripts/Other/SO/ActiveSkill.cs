using UnityEngine;
public class ActiveSkill : SO
{
    public SkillType Type;
    public SkillTargetType Target;
    public int TargetCount;
    public float Attack;
    public int BuffID;


    public string Name;
    public string Description;
    public string IconPath;

    public bool isSingleAttack;
    public int attackCount;
    public int[] damage;
    public float[] hitTiming;
    public Sprite icon;


    public int UltimateGauge;

    public bool IsMuti;
    public bool IsHeal;
    public bool IsBuff;
    public int Id;

}



