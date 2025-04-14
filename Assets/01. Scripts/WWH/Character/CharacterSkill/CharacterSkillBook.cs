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

        //�������� ó��

        //����, ��Ÿ��, ��� �����Ѱ� �Ұ��� �Ѱ�
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
