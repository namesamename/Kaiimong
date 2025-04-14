using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    public SkillSO skillSO;
    DebuffSkillSO debuffSkillSO;
    BuffSkillSO buffSkillSO;


    private float CurCooltime;

    public void SetSkill(string id)
    {
        skillSO=  GlobalDatabase.Instance.skill.GetSkillSOToID(id);

        if (skillSO.buffSkillId != null)
        {
            if(skillSO.buffSkillId == "1")
            {
                buffSkillSO = GlobalDatabase.Instance.skill.GetBuffToID(int.Parse(skillSO.buffSkillId));
            }
            if (skillSO.buffSkillId == "2")
            {
                debuffSkillSO = GlobalDatabase.Instance.skill.GetDebuffToID(int.Parse(skillSO.buffSkillId));
            }
        }

    }


    public void UseSkill(Character[] targetcharacter)
    {
        //추후 추가
        if(buffSkillSO != null || debuffSkillSO != null)
        {
            foreach(Character c in targetcharacter) 
            {c.stat.Buff(skillSO);}
        }
        else if(skillSO.IsHeal)
        {
            foreach (Character c in targetcharacter)
            { c.stat.healthStat.Heal(skillSO.damage[0]);}
        }
        else
        {
            foreach (Character c in targetcharacter)
            {c.stat.TakeDamage(skillSO.damage[0]);}
        }
        
    }

    public void CoolTimeDown()
    {
        CurCooltime -= 1;
        CurCooltime = Mathf.Max(0, CurCooltime);
    }

    public float GetCoolTime()
    {
        return CurCooltime;
    }



}
