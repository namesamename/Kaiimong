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
        SkillEffectPrefab = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.SkillEffect, $"{id}");
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
        if (SkillSO.IsBuff == false)
        {
            if (targetcharacter[0].GetCharacterType() == CharacterType.Enemy)
            {
                QuestManager.Instance.QuestTypeValueUP(1, QuestType.UseSkill);
            }
        }
        else
        {
            if (targetcharacter[0].GetCharacterType()  == CharacterType.Friend)
            {
                QuestManager.Instance.QuestTypeValueUP(1, QuestType.UseSkill);
            }
           
        }
        
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
                SkillDelay(GetAnimationLength(), SkillSO.Attack * stat.attackStat.GetStat(), c, DamageType.Cri);
                EffectOn(SkillSO.EffectType, targetcharacter);
            }
        }
        else
        {
            Debug.Log("TakeDamage");

            foreach (CharacterCarrier character in targetcharacter.ToList())
            {
                
                if (!CurrencyManager.Instance.GetIsTutorial())
                {
                    
                    if(SkillSO.ID != 3)
                    {
                        character.stat.TakeDamage(1f);
                        StartCoroutine(SkillDelay(GetAnimationLength(),  1f, character, DamageType.Week));
                        EffectOn(SkillSO.EffectType, targetcharacter);
                    }
                    else
                    {
                        character.stat.TakeDamage(999f);
                        StartCoroutine(SkillDelay(GetAnimationLength(), 999f, character, DamageType.Week));
                        EffectOn(SkillSO.EffectType, targetcharacter);
        
                    }
             
                }
                else
                {
                    float AllDamage = SkillSO.Attack * stat.attackStat.GetStat();

                    if (stat.criticalPerStat.Value > Random.Range(0.000f, 1f))
                    {
                        AllDamage *= stat.criticalAttackStat.Value;
                        Debug.Log("Å©¸®Æ¼ÄÃ ¶ä¤§¤§");

                        if (character.GetCharacterType() != transform.GetComponentInParent<CharacterCarrier>().GetCharacterType())
                        {
                            character.stat.TakeDamage(AllDamage * (float)1.2);
                            StartCoroutine(SkillDelay(GetAnimationLength(), AllDamage * (float)1.2 - character.stat.defenseStat.GetStat(), character, DamageType.CriAndWeek));
                            EffectOn(SkillSO.EffectType, targetcharacter);
                        }
                        else
                        {
                            character.stat.TakeDamage(AllDamage);
                            StartCoroutine(SkillDelay(GetAnimationLength(), AllDamage - character.stat.defenseStat.GetStat(), character, DamageType.Cri));
                            EffectOn(SkillSO.EffectType, targetcharacter);
                        }

                    }
                    else
                    {

                        if (character.GetCharacterType() != transform.GetComponentInParent<CharacterCarrier>().GetCharacterType())
                        {
                            character.stat.TakeDamage(AllDamage * (float)1.2);
                            StartCoroutine(SkillDelay(GetAnimationLength(), AllDamage * (float)1.2 - character.stat.defenseStat.GetStat(), character, DamageType.Week));
                            EffectOn(SkillSO.EffectType, targetcharacter);
                        }
                        else
                        {
                            character.stat.TakeDamage(AllDamage);
                            StartCoroutine(SkillDelay(GetAnimationLength(), AllDamage - character.stat.defenseStat.GetStat(), character, DamageType.Basic));
                            EffectOn(SkillSO.EffectType, targetcharacter);
                        }
                    }
                }
            }

            if (!CurrencyManager.Instance.GetIsTutorial() && SkillSO.ID == 3)
            {
                CurrencyManager.Instance.ClearTutorial();
            }


        }





    }


    public async void SetDamageAsync(CharacterCarrier character, float AllDamage, DamageType damage)
    {
        GameObject DamagePOP = await UIManager.Instance.CreatTransformPOPUPAsync("DamagePOPUP", character.transform);
        DamagePOP.GetComponent<DamagePOPUP>().SetPOPUP(AllDamage, damage, character);
    }

    public IEnumerator SkillDelay(float SkillAnimation, float AllDamage, CharacterCarrier character, DamageType damage)
    {
       
         yield return new WaitForSeconds(SkillAnimation);
        SetDamageAsync(character, AllDamage, damage);
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
            case SkillEffectType.transformToTarget:
                GameObject Tagetskill = Instantiate(SkillEffectPrefab, transform.position, Quaternion.identity);
                EffectDirection(Tagetskill);
                Tagetskill.GetComponent<ISkillEffectable>().Play(characters);
                break;
            case SkillEffectType.Self:
                GameObject skill  = Instantiate(SkillEffectPrefab, transform.position, Quaternion.identity);
                EffectDirection(skill);
                skill.GetComponent<ISkillEffectable>().Play(characters);
                break;
            case SkillEffectType.AllEnemy:
            case SkillEffectType.AllFriend:
            case SkillEffectType.SingleEnemy:
            case SkillEffectType.SingleFriend:
                foreach (CharacterCarrier character in characters)
                {
                    GameObject skiils = Instantiate(SkillEffectPrefab, character.transform.position, Quaternion.identity);
                    EffectDirection(skiils);
                    skiils.GetComponent<ISkillEffectable>().Play(characters);
                }
                break;
            case SkillEffectType.BattleField:
                GameObject FieldSkill = Instantiate(SkillEffectPrefab);
                EffectDirection(FieldSkill);
                FieldSkill.transform.position = new Vector3(0f, -5f, 0f);
      
                FieldSkill.GetComponent<ISkillEffectable>().Play(characters);
                break;
            case SkillEffectType.EnemyField:
                GameObject Enemyskiils = Instantiate(SkillEffectPrefab, new Vector3(5,-1,0), Quaternion.identity);
                EffectDirection(Enemyskiils);
                Enemyskiils.GetComponent<ISkillEffectable>().Play(characters);
                break;
            case SkillEffectType.FriendField:
                GameObject Friendskiils = Instantiate(SkillEffectPrefab, new Vector3(-5, -1, 0), Quaternion.identity);
                EffectDirection(Friendskiils);
                Friendskiils.GetComponent<ISkillEffectable>().Play(characters);
                break;


        }

    }


    public void EffectDirection(GameObject Effect)
    {
        switch(SkillSO.EffectUpDownType) 
        {
            case SkillEffectUpDownType.Middle:
                break;
            case SkillEffectUpDownType.Right:
                Effect.transform.position = new Vector3(Effect.transform.position.x + 1f, Effect.transform.position.y, Effect.transform.position.z);
                break;
            case SkillEffectUpDownType.Left:
                Effect.transform.position = new Vector3(Effect.transform.position.x -1f, Effect.transform.position.y, Effect.transform.position.z);
                break;
            case SkillEffectUpDownType.Down:
                Effect.transform.position = new Vector3(Effect.transform.position.x, Effect.transform.position.y -1f, Effect.transform.position.z);
                break;
            case SkillEffectUpDownType.Up:
                Effect.transform.position = new Vector3(Effect.transform.position.x, Effect.transform.position.y +1f, Effect.transform.position.z);
                break;
        }
    }
}
