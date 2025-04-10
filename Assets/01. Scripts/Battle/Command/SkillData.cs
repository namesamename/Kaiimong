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

    public void Execute(DummyUnit unit, List<DummyUnit> target)
    {
        foreach(DummyUnit targets in target)
        {
            Debug.Log($"{unit.Speed}À¯´ÖÀÌ {targets.AppearAnimationLength}Å¸°Ù¿¡°Ô ¹«¾ð°¡¹º°¡¹¹°¡");
        }
    }
}
