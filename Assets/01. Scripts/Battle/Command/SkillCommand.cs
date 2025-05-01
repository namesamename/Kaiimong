using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkillCommand
{
    public ActiveSkillObject skillData;
    public CharacterCarrier unit;
    public List<CharacterCarrier> targets;

    public SkillCommand(CharacterCarrier unit, List<CharacterCarrier> targets, ActiveSkillObject skillData)
    {
        this.targets = new List<CharacterCarrier>(targets);
        this.unit = unit;
        this.skillData = skillData;
    }
    public void Execute()
    {
        if(skillData != null && unit != null && (targets != null || targets.Count > 0))
        {
            unit.skillBook.ActiveSkillUsing(skillData, targets);
        }
        else
        {
            return;
        }
    }




    public void Undo()
    {
    }
}
