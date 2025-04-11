using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObject : MonoBehaviour
{
    SkillSO skillSO;
    DebuffSkillSO debuffSkillSO;
    BuffSkillSO buffSkillSO;


    private float CurCooltime;

    public void SetSkill(string id)
    {
        skillSO=  GlobalDatabase.Instance.skill.GetSkillSOToGUID(id);
        if(skillSO.IsBuff)
        {buffSkillSO = GlobalDatabase.Instance.skill.GetBuffToID(skillSO.Id);}
        else if(skillSO.IsDebuff)
        {debuffSkillSO = GlobalDatabase.Instance.skill.GetDebuffToID(skillSO.Id);}
    }


    public void UseSkill(Character[] character)
    {
        //추후 추가
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
