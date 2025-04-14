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
        if(skillSO.IsBuff)
        {buffSkillSO = GlobalDatabase.Instance.skill.GetBuffToID(skillSO.Id);}
        else if(skillSO.IsDebuff)
        {debuffSkillSO = GlobalDatabase.Instance.skill.GetDebuffToID(skillSO.Id);}
    }


    public void UseSkill(Character[] character)
    {
        //추후 추가

        if(skillSO.IsBuff && buffSkillSO != null)
        {
            foreach(Character c in character) 
            {
                c.stat.Buff(buffSkillSO, skillSO);
            }
        }
        else if(skillSO.IsDebuff && debuffSkillSO != null)
        {
            foreach (Character c in character)
            {
                c.stat.DeBuff(debuffSkillSO, skillSO);
            }
        }
        else
        {
            foreach (Character c in character)
            {
                c.stat.TakeDamage(skillSO.damage[0]);
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
