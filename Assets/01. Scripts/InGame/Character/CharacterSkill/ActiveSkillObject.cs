using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;





public class ActiveSkillObject : MonoBehaviour
{
    public ActiveSkill SkillSO;
    private float curCooltime;
    private Animator animator;
    private CharacterVisual characterVisual;

   public GameObject SkillEffectPrefab;




    private void Awake()
    {
        animator = transform.parent.transform.parent.GetComponentInChildren<Animator>();
        characterVisual =transform.parent.transform.parent.GetComponentInChildren<CharacterVisual>();
    }
    public async void SetSkill(int id)
    {
        SkillSO = GlobalDataTable.Instance.skill.GetActSkillSOToID(id);
        SkillEffectPrefab = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.SkillEffect, "1");
    }


    public float GetAnimationLength()
    {
        if (SkillSO.ID % 3 == 1)
        {
            return characterVisual.GetAnimationLength(2);
        }
        else if (SkillSO.ID % 3 == 2)
        {
            return characterVisual.GetAnimationLength(3);
        }
        else
        {
            return characterVisual.GetAnimationLength(4);
        }
    }
    public void UseSkill(List<CharacterCarrier> targetcharacter)
    {
        QuestManager.Instance.QuestTypeValueUP(1, QuestType.UseSkill);
        if (animator != null)
        {
            if (SkillSO.ID % 3 == 1)
            {
                animator.SetTrigger("First");
            }
            else if (SkillSO.ID % 3 == 2)
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
            { c.stat.Buff(SkillSO);
                EffectOn(SkillSO.EffectType, targetcharacter);
            }
        }
        else if (SkillSO.Type == SkillType.heal)
        {
            foreach (CharacterCarrier c in targetcharacter)
            {
                c.stat.healthStat.Heal(SkillSO.Attack * stat.attackStat.GetStat());
                SkillDelay(GetAnimationLength(), SkillSO.Attack * stat.attackStat.GetStat(), c, false);
                EffectOn(SkillSO.EffectType, targetcharacter);
            }
        }
        else
        {
            Debug.Log("TakeDamage");

            foreach (CharacterCarrier character in targetcharacter.ToList())
            {

                float AllDamage = SkillSO.Attack * stat.attackStat.GetStat();

                if (stat.criticalPerStat.Value > Random.Range(0.000f, 1f))
                {
                    AllDamage *= stat.criticalAttackStat.Value;
                    Debug.Log("크리티컬 뜸ㄷㄷ");
                    character.stat.TakeDamage(AllDamage);
                    StartCoroutine(SkillDelay(GetAnimationLength(), AllDamage, character, true));
                    EffectOn(SkillSO.EffectType, targetcharacter);
                }
                else
                {
                    character.stat.TakeDamage(AllDamage);
                    StartCoroutine(SkillDelay(GetAnimationLength(), AllDamage, character, false));
                    EffectOn(SkillSO.EffectType, targetcharacter);

                }
            }


         

        }





    }


    public async void SetDamageAsync(CharacterCarrier character, float AllDamage, bool IsCri)
    {
        GameObject DamagePOP = await UIManager.Instance.CreatTransformPOPUPAsync("DamagePOPUP", character.transform);
        DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(AllDamage, IsCri, character);
    }

    public IEnumerator SkillDelay(float SkillAnimation, float AllDamage, CharacterCarrier character, bool IsCri)
    {
       
         yield return new WaitForSeconds(SkillAnimation);
        SetDamageAsync(character, AllDamage, IsCri);
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


    public void EffectOn(SkillEffectType skillEffect, List<CharacterCarrier> characters)
    {
        

        switch (skillEffect)
        {
            case SkillEffectType.Self:
                Debug.Log("왜 안나오냐");
                GameObject skill  = Instantiate(SkillEffectPrefab, transform.position, Quaternion.identity);
                skill.GetComponent<SkillEffect>().Play();
                break;
            case SkillEffectType.AllEnemy:
            case SkillEffectType.AllFriend:
            case SkillEffectType.SingleEnemy:
            case SkillEffectType.SingleFriend:
                foreach (CharacterCarrier character in characters)
                {
                    GameObject skiils = Instantiate(SkillEffectPrefab, character.transform.position, Quaternion.identity);
                    skiils.GetComponent<SkillEffect>().Play();
                }
                break;
            case SkillEffectType.BattleField:
                GameObject FieldSkill = Instantiate(SkillEffectPrefab);
                FieldSkill.transform.position = Vector3.zero;
                FieldSkill.GetComponent<SkillEffect>().Play();
                break;
            case SkillEffectType.EnemyField:
                GameObject Enemyskiils = Instantiate(SkillEffectPrefab, new Vector3(5,0,0), Quaternion.identity);
                Enemyskiils.GetComponent<SkillEffect>().Play();
                break;
            case SkillEffectType.FriendField:
                GameObject Friendskiils = Instantiate(SkillEffectPrefab, new Vector3(-5, 0, 0), Quaternion.identity);
                Friendskiils.GetComponent<SkillEffect>().Play();
                break;

        }

    }
}
