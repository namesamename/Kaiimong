using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public bool isSingleAttack;
    public int attackCount;
    public int[] damage;
    public float[] hitTiming;
    public Sprite icon;
}
