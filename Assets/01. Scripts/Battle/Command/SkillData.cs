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

    public void Execute(Character unit, List<Character> targets)
    {
        foreach (Character target in targets)
        {
            Debug.Log($"{unit.stat.agilityStat}������ {target.stat.agilityStat}Ÿ�ٿ��� ���𰡹�������");
        }
    }
}
