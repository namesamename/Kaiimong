using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    public Skill skillSO;
    private float CurCooltime;

    public void SetSkill(int id)
    {
        skillSO=  GlobalDataTable.Instance.skill.GetSkillSOToID(id);

        if (skillSO.buffSkillId != null)
        {
            if(skillSO.Type == SkillType.buff)
            {
                //buffSkillSO = GlobalDatabase.Instance.skill.GetBuffToID(int.Parse(skillSO.buffSkillId));
            }
            if (skillSO.Type == SkillType.debuff)
            {
                //debuffSkillSO = GlobalDatabase.Instance.skill.GetDebuffToID(int.Parse(skillSO.buffSkillId));
            }
        }

    }


    public void UseSkill(List<CharacterCarrier> targetcharacter)
    {
        //추후 추가
        if(skillSO.Type == SkillType.debuff || skillSO.Type == SkillType.buff)
        {
            Debug.Log("Use Buff");
            foreach (CharacterCarrier c in targetcharacter) 
            {c.stat.Buff(skillSO);}
        }
        else if(skillSO.Type == SkillType.heal)
        {
            foreach (CharacterCarrier c in targetcharacter)
            { c.stat.healthStat.Heal(skillSO.damage[0]);}
        }
        else
        {
            Debug.Log("TakeDamage");
            foreach (CharacterCarrier c in targetcharacter)
            {
                CharacterStat stat = GetComponentInParent<CharacterStat>();
                float AllDamage = skillSO.Attack;
                if (stat.criticalPerStat.Value < Random.Range(0.0f, 100.0f))
                {
                    AllDamage = stat.attackStat.Value * stat.criticalAttackStat.Value;
                }

                c.stat.TakeDamage( AllDamage);
            
            
            
            
            }
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
