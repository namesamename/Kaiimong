using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillBook : MonoBehaviour
{

    public ActiveSkillObject[] ActiveSkillList = new ActiveSkillObject[3];
    public PassiveSkillObjcet[] PassiveSkillList = new PassiveSkillObjcet[3];
    // public PassiveSkillObject[] PassiveSkillList = new PassiveSkillObject[3];
    private void Awake()
    {
        PassiveSkillList = GetComponentsInChildren<PassiveSkillObjcet>();
        ActiveSkillList = GetComponentsInChildren<ActiveSkillObject>();

    }
   

    public void SkillSet(int ID)
    {
        int Count = ID * 3;

        for (int i = 0; i < 3; i++) 
        {
            int Skillid = Count - i;
            ActiveSkillList[i].SetSkill(Skillid);

        }
     
    }

    public void PassiveSkillOn(List<CharacterCarrier> characters)
    {
        for (int i = 0; i < PassiveSkillList.Length; i++)
        {
            PassiveSkillList[i].UseSkill(characters);
        }
    }
    public void ActiveSkillUsing(ActiveSkillObject skill, List<CharacterCarrier> characters)
    {

        if(skill == null) return;
        if (characters == null) return;
        
        //여러가지 처리
        //마나, 쿨타임, 사용 가능한가 불가능 한가
        if (!IsCoolTime(skill))
            return;
        skill.UseSkill(characters);
    }

    public bool IsCoolTime(ActiveSkillObject skill)
    {
        if (skill.GetCoolTime() == 0)
        { 
            return true;
        }
        else
        {
            return false;
        }
        
    }


    public void SetCoolTimeDown()
    {
        for (int i = 0; i < ActiveSkillList.Length; i++)
        {
            ActiveSkillList[i].CoolTimeDown();
        }
    }

}
