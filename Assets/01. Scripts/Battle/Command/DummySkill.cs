using System.Collections.Generic;
using UnityEngine;

public class DummySkill : ICommand //DummySkillCommand
{
    public SkillData skillData;
    public DummyUnit unit;
    public List<DummyUnit> targets;

    public DummySkill(DummyUnit unit, List<DummyUnit> targets, SkillData skillData)
    {
        this.targets = targets;
        this.unit = unit;
        this.skillData = skillData;
    }
    public void Execute()
    {
        skillData.Execute(unit, targets);
    }

    public void Undo()
    {
    }
}
