using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    public SkillSO skillSO;
    DebuffSkillSO debuffSkillSO;
    public BuffSkillSO buffSkillSO;


    private float CurCooltime;

    public void SetSkill(int id)
    {
        skillSO=  GlobalDataTable.Instance.skill.GetSkillSOToID(id);

        if (skillSO.buffSkillId != null)
        {
            if(skillSO.IsBuff)
            {
                //buffSkillSO = GlobalDatabase.Instance.skill.GetBuffToID(int.Parse(skillSO.buffSkillId));
            }
            if (skillSO.IsBuff)
            {
                //debuffSkillSO = GlobalDatabase.Instance.skill.GetDebuffToID(int.Parse(skillSO.buffSkillId));
            }
        }

    }


    public void UseSkill(List<CharacterCarrier> targetcharacter)
    {
        //추후 추가
        if(skillSO.IsBuff)
        {
            Debug.Log("Use Buff");
            foreach (CharacterCarrier c in targetcharacter) 
            {c.stat.Buff(skillSO);}
        }
        else if(skillSO.IsHeal)
        {
            foreach (CharacterCarrier c in targetcharacter)
            { c.stat.healthStat.Heal(skillSO.damage[0]);}
        }
        else
        {
            Debug.Log("TakeDamage");
            foreach (CharacterCarrier c in targetcharacter)
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
