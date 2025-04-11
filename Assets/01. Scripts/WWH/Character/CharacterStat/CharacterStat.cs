using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public enum StatType
{
    attack,
    defense,
    agility,
    health,
    criticalPer,
    criticalAttack
}


[System.Serializable]
public class CharacterStat : MonoBehaviour
{
    public AttackStat attackStat;
    public DefenseStat defenseStat;
    public AgilityStat agilityStat;
    public HealthStat healthStat;
    public CriticalPerStat criticalPerStat;
    public CriticalAttackStat criticalAttackStat;
    public Dictionary<StatType, BaseStat> statDict;


    private void Awake()
    {
        attackStat = GetComponent<AttackStat>();
        defenseStat = GetComponent<DefenseStat>();
        agilityStat = GetComponent<AgilityStat>();
        healthStat = GetComponent<HealthStat>();
        criticalAttackStat = GetComponent<CriticalAttackStat>();
        criticalPerStat = GetComponent<CriticalPerStat>();

        statDict = new Dictionary<StatType, BaseStat>()
        {
            {StatType.attack, attackStat},
            {StatType.defense, defenseStat},
            {StatType.agility, agilityStat},
            {StatType.health, healthStat},
            {StatType.criticalPer, criticalPerStat},
            {StatType.criticalAttack, criticalAttackStat},
        };



    }

    public void SetCharacter(CharacterSO character)
    {
        attackStat.Value = character.Attack;
        defenseStat.Value = character.Defense;
        healthStat.Value = character.Health;
        agilityStat.Value = character.Speed;
        criticalPerStat.Value = float.Parse(character.CriticalPer.Substring(0, character.CriticalPer.Length - 1));
        criticalAttackStat.Value = float.Parse(character.CriticalAttack.Substring(0, character.CriticalAttack.Length - 1));
    }

    public void TakeDamage(float Amount)
    {
        healthStat.AddStat(Amount);
        if(healthStat.Value == 0)
        {
            OnDie();
        }
    }
    public void OnDie()
    {
        gameObject.SetActive(false);
    }
    


    public void Buff(BuffSkillSO buffSkill, SkillSO skill)
    {
       
        string s = Utility.KoreanValueChanger(buffSkill.Name);
        //아군 적군 가리는 것이 아직 불가능
        //리팩토링도 해야 함

        if(s == "Health")
        {
            StartCoroutine(BuffStart(healthStat, buffSkill, skill));
        }
        else if(s == "Attack")
        {
            StartCoroutine(BuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "Defense")
        {
            StartCoroutine(BuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "Speed")
        {
            StartCoroutine(BuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "CriticalPer")
        {
            StartCoroutine(BuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "CriticalAttack")
        {
            StartCoroutine(BuffStart(healthStat, buffSkill, skill));
        }


    }
    public void DeBuff(DebuffSkillSO buffSkill, SkillSO skill)
    {

        string s = Utility.KoreanValueChanger(buffSkill.Name);
        //아군 적군 가리는 것이 아직 불가능

        if (s == "Health")
        {
            StartCoroutine(DebuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "Attack")
        {
            StartCoroutine(DebuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "Defense")
        {
            StartCoroutine(DebuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "Speed")
        {
            StartCoroutine(DebuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "CriticalPer")
        {
            StartCoroutine(DebuffStart(healthStat, buffSkill, skill));
        }
        else if (s == "CriticalAttack")
        {
            StartCoroutine(DebuffStart(healthStat, buffSkill, skill));
        }


    }

    public IEnumerator DebuffStart(BaseStat stat, DebuffSkillSO buffSkill, SkillSO skill)
    {
        if (skill.damage[0].GetType() == typeof(float))
        {
            stat.AddMultiples(skill.damage[0]);
        }
        else
        {
            stat.AddStat(skill.damage[0]);
        }
        yield return buffSkill.Duration <= 0;
        if (skill.damage[0].GetType() == typeof(float))
        {
            stat.AddMultiples(-skill.damage[0]);
        }
        else
        {
            stat.AddStat(-skill.damage[0]);
        }
    }

    public IEnumerator BuffStart(BaseStat stat, BuffSkillSO buffSkill , SkillSO skill)
    {
        if (skill.damage[0].GetType() ==  typeof(float)) 
        {
            stat.AddMultiples(skill.damage[0]);
        }
        else
        {
            stat.AddStat(skill.damage[0]);
        }
        yield return buffSkill.Duration <= 0;
        if (skill.damage[0].GetType() == typeof(float))
        {
            stat.AddMultiples(-skill.damage[0]);
        }
        else
        {
            stat.AddStat(-skill.damage[0]);
        }
    }

   


}
