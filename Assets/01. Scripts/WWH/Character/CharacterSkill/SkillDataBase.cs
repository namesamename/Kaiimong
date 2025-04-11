using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SkillDataBase
{
   public Dictionary<string, SkillSO> SkillDataDic = new Dictionary<string, SkillSO>();

    public Dictionary<int, BuffSkillSO> BuffskillDic = new Dictionary<int, BuffSkillSO>();
    public Dictionary<int, DebuffSkillSO> DebuffskillDic = new Dictionary<int, DebuffSkillSO>();
    public void Initialize()
    {
        SkillSO[] skillSO = Resources.LoadAll<SkillSO>("Skil");
        DebuffSkillSO[] DebuffSO = Resources.LoadAll<DebuffSkillSO>("Debu");
        BuffSkillSO[] BuffSO = Resources.LoadAll<BuffSkillSO>("Buff");

        foreach (SkillSO skill in skillSO)
        {
            SkillDataDic[skill.ID] = skill;
        }
        foreach (DebuffSkillSO skill in DebuffSO)
        {
            DebuffskillDic[skill.Id] = skill;
        }
        foreach (BuffSkillSO skill in BuffSO)
        {
            BuffskillDic[skill.Id] = skill;
        }
    }

    public SkillSO GetSkillSOToGUID(string SkillId)
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
    public DebuffSkillSO GetDebuffToID(int id)
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
    public BuffSkillSO GetBuffToID(int id)
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
