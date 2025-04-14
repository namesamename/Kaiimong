using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public bool isSingleAttack;
    public bool isBuff;
    public int attackCount;
    public int[] damage;
    public float[] hitTiming;
    public Sprite icon;

    public void Execute(DummyUnit unit, List<DummyUnit> targets)
    {
        foreach (DummyUnit target in targets)
        {
            Debug.Log($"{unit.Speed}À¯´ÖÀÌ {target.Speed}Å¸°Ù¿¡°Ô ¹«¾ð°¡¹º°¡¹¹°¡");
        }
    }
}
