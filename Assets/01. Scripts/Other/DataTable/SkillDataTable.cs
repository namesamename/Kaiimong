using System.Collections.Generic;
using UnityEngine;


public class SkillDataTable
{
    private Dictionary<int, ActiveSkill> ActSkillDataDic = new Dictionary<int, ActiveSkill>();
    private Dictionary<int, PassiveSkill> PasSkillDataDic = new Dictionary<int, PassiveSkill>();
    private Dictionary<int, Buff> BuffskillDic = new Dictionary<int, Buff>();
    private Dictionary<int, Debuff> DebuffskillDic = new Dictionary<int, Debuff>();
    public void Initialize()
    {
        ActiveSkill[] ActSO = Resources.LoadAll<ActiveSkill>("ActSkill");
        PassiveSkill[] PasSO = Resources.LoadAll<PassiveSkill>("PasS");
        Debuff[] DebuffSO = Resources.LoadAll<Debuff>("Debuff");
        Buff[] BuffSO = Resources.LoadAll<Buff>("Buff");

        foreach (ActiveSkill skill in ActSO)
        {
            ActSkillDataDic[skill.ID] = skill;
        }
        foreach (PassiveSkill skill in PasSO)
        {
            PasSkillDataDic[skill.ID] = skill;
        }
        foreach (Debuff skill in DebuffSO)
        {
            DebuffskillDic[skill.ID] = skill;
        }
        foreach (Buff skill in BuffSO)
        {
            BuffskillDic[skill.ID] = skill;
        }
    }

    public ActiveSkill GetActSkillSOToID(int SkillId)
    {
        if (ActSkillDataDic.ContainsKey(SkillId) && ActSkillDataDic[SkillId] != null)
        {
            return ActSkillDataDic[SkillId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }

    public PassiveSkill GetPasSkillSOToID(int SkillId)
    {
        if (PasSkillDataDic.ContainsKey(SkillId) && PasSkillDataDic[SkillId] != null)
        {
            return PasSkillDataDic[SkillId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }
    public Debuff GetDebuffToID(int id)
    {
        if( DebuffskillDic.ContainsKey(id) && DebuffskillDic[id] != null  )
        {
            return DebuffskillDic[id];
        }
        else
        {
            Debug.Log("That Id Dont Have Character");
            return null;
        }
    }
    public Buff GetBuffToID(int id)
    {
        if (BuffskillDic.ContainsKey(id) && BuffskillDic[id] != null)
        {
            return BuffskillDic[id];
        }
        else
        {
            Debug.Log("That Id Dont Have Character");
            return null;
        }
    }

 

}
