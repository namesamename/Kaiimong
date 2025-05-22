using System.Collections;
using System.Collections.Generic;
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
    public IEnumerator Execute()
    {
        Debug.Log("µü´ë");
        unit.skillBook.ActiveSkillUsing(skillData, targets);
        unit.skillBook.SetSkillGauge(skillData, targets);

        float waitTime = 0f;

        int id = skillData.SkillSO.ID % 3;
        int animIndex = id == 1 ? 3 : id == 2 ? 4 : 5;

        waitTime = unit.visual.GetAnimationLength(animIndex);

        yield return new WaitForSeconds(waitTime + 0.5f);
    }

    public void Undo()
    {
    }
}
