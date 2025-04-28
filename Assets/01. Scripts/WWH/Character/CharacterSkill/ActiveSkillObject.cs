using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ActiveSkillObject : MonoBehaviour
{
    public ActiveSkill skillSO;
    private float CurCooltime;
    public void SetSkill(int id)
    {
        skillSO=  GlobalDataTable.Instance.skill.GetActSkillSOToID(id);
    }
    public void UseSkill(List<CharacterCarrier> targetcharacter)
    {
        CharacterStat stat = transform.parent.transform.parent.GetComponentInChildren<CharacterStat>();
        //추후 추가
        if (skillSO.Type == SkillType.debuff || skillSO.Type == SkillType.buff)
        {
            Debug.Log("Use Buff");
            foreach (CharacterCarrier c in targetcharacter) 
            {c.stat.Buff(skillSO);}
        }
        else if(skillSO.Type == SkillType.heal)
        {
            foreach (CharacterCarrier c in targetcharacter)
            { c.stat.healthStat.Heal(skillSO.Attack * stat.attackStat.GetStat());}
        }
        else
        {
            Debug.Log("TakeDamage");

            for (int i = 0; i < targetcharacter.Count; i++)
            {
                CharacterCarrier character = targetcharacter[i];
                if (skillSO == null)
                {
                    Debug.Log("SKill null");
                }
                float AllDamage = skillSO.Attack * stat.attackStat.GetStat();
                if (stat.criticalPerStat.Value > Random.Range(0.000f, 1f))
                {
                    AllDamage *= stat.criticalAttackStat.Value;
                    Debug.Log("크리티컬 뜸ㄷㄷ");
                }
                character.stat.TakeDamage(AllDamage);
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
