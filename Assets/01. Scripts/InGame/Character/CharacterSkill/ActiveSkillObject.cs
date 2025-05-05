using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;




public class ActiveSkillObject : MonoBehaviour
{
    public ActiveSkill SkillSO;
    private float curCooltime;
    private Animator animator;

    private void Awake()
    {
        animator = transform.parent.transform.parent.GetComponentInChildren<Animator>();
    }
    public void SetSkill(int id)
    {
        SkillSO = GlobalDataTable.Instance.skill.GetActSkillSOToID(id);
    }
    public async Task UseSkillAsync(List<CharacterCarrier> targetcharacter)
    {
        if (animator != null)
        {
            if (SkillSO.Id % 3 == 1)
            {
                animator.SetTrigger("First");
            }
            else if (SkillSO.Id % 3 == 2)
            {
                animator.SetTrigger("Second");
            }
            else
            {
                animator.SetTrigger("Thrid");
            }
        }

        CharacterStat stat = transform.parent.transform.parent.GetComponentInChildren<CharacterStat>();
        //추후 추가
        if (SkillSO.Type == SkillType.debuff || SkillSO.Type == SkillType.buff)
        {
            Debug.Log("Use Buff");
            foreach (CharacterCarrier c in targetcharacter)
            { c.stat.Buff(SkillSO); }
        }
        else if (SkillSO.Type == SkillType.heal)
        {
            foreach (CharacterCarrier c in targetcharacter)
            { 
                c.stat.healthStat.Heal(SkillSO.Attack * stat.attackStat.GetStat());
                GameObject DamagePOP = await UIManager.Instance.CreatTransformPOPUP("DamagePOPUP", c.transform);
                DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(SkillSO.Attack * stat.attackStat.GetStat(), false, c);
            }
        }
        else
        {
            Debug.Log("TakeDamage");

            foreach (CharacterCarrier character in targetcharacter.ToList())
            {
                GameObject DamagePOP = await UIManager.Instance.CreatTransformPOPUP("DamagePOPUP", character.transform);
                if (SkillSO == null)
                {
                    Debug.Log("SKill null");
                }

                float AllDamage = SkillSO.Attack * stat.attackStat.GetStat();
                if (stat.criticalPerStat.Value > Random.Range(0.000f, 1f))
                {
                    AllDamage *= stat.criticalAttackStat.Value;
                    Debug.Log("크리티컬 뜸ㄷㄷ");
                    DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(AllDamage, true, character);
                    character.stat.TakeDamage(AllDamage);
                    
                }
                else
                {
                    DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(AllDamage, false, character);
                    character.stat.TakeDamage(AllDamage);
                    
                }
            }

        }



    }

    public void CoolTimeDown()
    {
        curCooltime -= 1;
        curCooltime = Mathf.Max(0, curCooltime);
    }

    public float GetCoolTime()
    {
        return curCooltime;
    }



}
