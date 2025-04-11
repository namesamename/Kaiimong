using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillBook : MonoBehaviour
{

    public SkillObject[] SkillList = new SkillObject[3];


 
    private void Start()
    {
        SkillList = GetComponentsInChildren<SkillObject>();
        for (int i = 0; i < SkillList.Length; i++)
        {
            //SkillList[i].SetSkill();
        }
    }

    public void SkillUsing(SkillObject skill)
    {

        //�������� ó��

        //����, ��Ÿ��, ��� �����Ѱ� �Ұ��� �Ѱ�
        if (!IsCoolTime(skill))
            return;


        //skill.UseSkill();
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
