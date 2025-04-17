using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PassiveType
{
    Stat,
    Heal,
    Other
}

public class PassiveSkill : SO
{
    public PassiveType PassiveType;
    public float Value;
    public SkillTargetType Target;
    public StatType StatType;

}
