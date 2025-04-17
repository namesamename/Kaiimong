using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PassiveSkillObjcet : MonoBehaviour
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
            PassiveOn(targetcharacter);
        }
    }
 
    public  void PassiveOn(List<CharacterCarrier> targetcharacter)
    {

        switch(passiveSkill.PassiveType) 
        {
            case PassiveType.Stat:
                StatPassive(targetcharacter, passiveSkill);
                break;
            case PassiveType.Heal:
                HealPassive(targetcharacter, passiveSkill);
                break;
            case PassiveType.Other:
                OtherPassive(targetcharacter, passiveSkill);
                break;
        }

    }
    public virtual bool IsOk()
    {
        return true;
    }

    public virtual void StatPassive(List<CharacterCarrier> targetcharacter , PassiveSkill passive)
    {
        
    }
    public virtual void HealPassive(List<CharacterCarrier> targetcharacter, PassiveSkill passive)
    {

    }
    public virtual void OtherPassive(List<CharacterCarrier> targetcharacter, PassiveSkill passive)
    {

    }


}
