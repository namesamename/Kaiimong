using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;



public class ActiveSkillObject : MonoBehaviour
{
    public ActiveSkill skillSO;
    private float CurCooltime;
    private Animator animator;

    private void Awake()
    {
        animator = transform.parent.transform.parent.GetComponentInChildren<Animator>();
    }
    public void SetSkill(int id)
    {
        skillSO = GlobalDataTable.Instance.skill.GetActSkillSOToID(id);
    }
    public void UseSkill(List<CharacterCarrier> targetcharacter)
    {
        if(skillSO.Id % 3 == 1)
        {
            animator.SetTrigger("First");
        }
        else if(skillSO.Id % 3 == 2)
        {
            animator.SetTrigger("Second");
        }
        else
        {
            animator.SetTrigger("Thrid");
        }

        CharacterStat stat = transform.parent.transform.parent.GetComponentInChildren<CharacterStat>();
        //추후 추가
        if (skillSO.Type == SkillType.debuff || skillSO.Type == SkillType.buff)
        {
            Debug.Log("Use Buff");
            foreach (CharacterCarrier c in targetcharacter)
            { c.stat.Buff(skillSO); }
        }
        else if (skillSO.Type == SkillType.heal)
        {
            foreach (CharacterCarrier c in targetcharacter)
            { 
                
                c.stat.healthStat.Heal(skillSO.Attack * stat.attackStat.GetStat());

                UIPopup DamagePOP = UIManager.Instance.ShowPopup("DamagePOPUP");
                

       

                DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(skillSO.Attack * stat.attackStat.GetStat(), false, c);
            }
        }
        else
        {
            Debug.Log("TakeDamage");

            foreach (CharacterCarrier character in targetcharacter.ToList())
            {
                UIPopup DamagePOP = UIManager.Instance.ShowPopup("DamagePOPUP");

                Vector3 pos = Camera.main.WorldToScreenPoint(character.transform.parent.position);
                DamagePOP.GetComponent<RectTransform>().anchoredPosition = pos;

                if (skillSO == null)
                {
                    Debug.Log("SKill null");
                }

                float AllDamage = skillSO.Attack * stat.attackStat.GetStat();
                if (stat.criticalPerStat.Value > Random.Range(0.000f, 1f))
                {
                    AllDamage *= stat.criticalAttackStat.Value;
                    Debug.Log("크리티컬 뜸ㄷㄷ");
                    character.stat.TakeDamage(AllDamage);
                    DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(AllDamage, true,character);
                }
                else
                {
                    character.stat.TakeDamage(AllDamage);
                    DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(AllDamage, false, character);
                }
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
