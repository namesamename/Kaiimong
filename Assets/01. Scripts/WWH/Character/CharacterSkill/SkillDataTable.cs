using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillDataTable
{
   public Dictionary<int, ActiveSkill> SkillDataDic = new Dictionary<int, ActiveSkill>();

    public Dictionary<int, Buff> BuffskillDic = new Dictionary<int, Buff>();
    public Dictionary<int, Debuff> DebuffskillDic = new Dictionary<int, Debuff>();
    public void Initialize()
    {
        ActiveSkill[] skillSO = Resources.LoadAll<ActiveSkill>("Skil");
        Debuff[] DebuffSO = Resources.LoadAll<Debuff>("Debu");
        Buff[] BuffSO = Resources.LoadAll<Buff>("Buff");

        foreach (ActiveSkill skill in skillSO)
        {
            SkillDataDic[skill.ID] = skill;
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

    public ActiveSkill GetSkillSOToID(int SkillId)
    {
        if (SkillDataDic[SkillId] != null && SkillDataDic.ContainsKey(SkillId))
        {
            return SkillDataDic[SkillId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }
    public Debuff GetDebuffToID(int id)
    {
        if (DebuffskillDic[id] != null && DebuffskillDic.ContainsKey(id))
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
        if (BuffskillDic[id] != null && BuffskillDic.ContainsKey(id))
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
