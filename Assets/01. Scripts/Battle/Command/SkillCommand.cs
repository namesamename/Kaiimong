using System.Collections.Generic;

public class SkillCommand
{
    public SkillObject skillData;
    public Character unit;
    public List<Character> targets;

    public SkillCommand(Character unit, List<Character> targets, SkillObject skillData)
    {
        this.targets = new List<Character>(targets);
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
