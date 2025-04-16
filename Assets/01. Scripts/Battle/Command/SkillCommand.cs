using System.Collections.Generic;

public class SkillCommand
{
    public SkillObject skillData;
    public CharacterCarrier unit;
    public List<CharacterCarrier> targets;

    public SkillCommand(CharacterCarrier unit, List<CharacterCarrier> targets, SkillObject skillData)
    {
        this.targets = new List<CharacterCarrier>(targets);
        this.unit = unit;
        this.skillData = skillData;
    }
    public void Execute()
    {
        unit.skillBook.SkillUsing(skillData, targets);
    }

    public void Undo()
    {
    }
}
