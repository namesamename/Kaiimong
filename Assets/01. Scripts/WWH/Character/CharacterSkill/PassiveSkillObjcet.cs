using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSkillObjcet : MonoBehaviour, IPassivable, ISkillable
{
    
    PassiveSkill passiveSkill;


    public void SetSkill(int id)
    {
        passiveSkill = GlobalDataTable.Instance.skill.GetPasSkillSOToID(id);
    }
    public void UseSkill(List<CharacterCarrier> targetcharacter)
    {
        if(IsOk())
        {
            PassiveOn();
        }
    }
    public bool IsOk()
    {
        return true;
    }
    public void PassiveOn()
    {

        switch(passiveSkill.PassiveType) 
        {
            case PassiveType.Stat:
                break;
            case PassiveType.Heal:
                break;
            case PassiveType.Other:
                break;
        }

    }


}
