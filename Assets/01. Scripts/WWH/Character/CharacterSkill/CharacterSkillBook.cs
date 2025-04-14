using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillBook : MonoBehaviour
{

    public SkillObject[] SkillList = new SkillObject[3];

    public string[] SkillId = new string[3];

    private void Awake()
    {
        SkillList = GetComponentsInChildren<SkillObject>();
    }
   

    public void SkillSet(string ID)
    {
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillList[i].SetSkill($"{ID}_{i+1}");
        }
    }
    public void SkillUsing(SkillObject skill, Character[] characters)
    {

        //여러가지 처리

        //마나, 쿨타임, 사용 가능한가 불가능 한가
        if (!IsCoolTime(skill))
            return;


        skill.UseSkill(characters);
    }

    public bool IsCoolTime(SkillObject skill)
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
        for (int i = 0; i < SkillList.Length; i++)
        {
            SkillList[i].CoolTimeDown();
        }
    }

}
