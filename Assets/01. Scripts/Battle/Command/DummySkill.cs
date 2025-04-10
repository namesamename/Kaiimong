using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySkill : ICommand //DummySkillCommand
{
    private SkillData skillData;
    private DummyUnit unit;
    private List<DummyUnit> targets;

    public DummySkill(DummyUnit unit, List<DummyUnit> targets, SkillData skillData)
    {
        this.targets = targets;
        this.unit = unit;
        this.skillData = skillData;
    }
    public void Execute()
    {

    }

    public void Undo()
    {
    }
}
