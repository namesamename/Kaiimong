using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySkill : ICommand
{
    [SerializeField] private SkillData skillData;
    public DummySkill(SkillData skillData)
    {

    }
    public void Execute()
    {
    }

    public void Undo()
    {
    }
}
