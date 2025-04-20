using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;


public enum StatType
{
    Attack,
    Defense,
    Agility,
    Health,
    CriticalPer,
    CriticalAttack,
    invincible,
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

    public Action OnDeath;

    private int BuffTurn = 0;


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
            {StatType.Attack, attackStat},
            {StatType.Defense, defenseStat},
            {StatType.Agility, agilityStat},
            {StatType.Health, healthStat},
            {StatType.CriticalPer, criticalPerStat},
            {StatType.CriticalAttack, criticalAttackStat},
        };



    }

    public void SetCharacter(Character character, int Level)
    {
    
        attackStat.Value = character.Attack + Level;
        defenseStat.Value = character.Defense + Level;
        healthStat.Value = character.Health + Level;
        //agilityStat.Value = character.Speed + level;
        criticalAttackStat.Value = character.CriticalAttack + (float)(Level * 0.01);
        criticalPerStat.Value = character.CriticalPer +  (float)(Level * 0.01);
    }

    public void TakeDamage(float Amount)
    {
        if(defenseStat.GetStat() - Amount <=0)
        {
            healthStat.CurHealth -= 1f;
        }
        else
        {
            healthStat.CurHealth -= defenseStat.GetStat() - Amount;
        }
        
        if(healthStat.CurHealth<=0)
        {
            OnDie();
        }
     
    }
    public void OnDie()
    {
        gameObject.SetActive(false);
        OnDeath?.Invoke();
    }
    

    public void SetBufftimeDown()
    {
        if(BuffTurn != 0)
        {
            BuffTurn--;
        }
    }

    public BaseStat EnumChanger(StatType statType)
    {
        switch(statType) 
        {
            case StatType.Agility:
                return agilityStat;
            case StatType.Health:
                return healthStat;
            case StatType.CriticalPer:
                return criticalPerStat;
            case StatType.CriticalAttack:
                return criticalAttackStat;
            case StatType.Defense:
                return defenseStat;
            case StatType.Attack:
                return attackStat;
            default:
                return null;
        }
    }


    public void Buff(ActiveSkill Skill)
    {
        float Attack;
        if (Skill.Type == SkillType.buff && Skill.BuffID != 0)
        {
            BuffTurn = GlobalDataTable.Instance.skill.GetBuffToID(Skill.BuffID).Turn;
            Attack = GlobalDataTable.Instance.skill.GetBuffToID(Skill.BuffID).Value;
            BaseStat stat = EnumChanger(GlobalDataTable.Instance.skill.GetBuffToID(Skill.BuffID).Type);
            buffStart(stat,Attack,BuffTurn,SkillType.buff);
        }
        else if(Skill.Type == SkillType.debuff && Skill.BuffID != 0)
        {
            BuffTurn = GlobalDataTable.Instance.skill.GetDebuffToID(Skill.BuffID).Turn;
            Attack = GlobalDataTable.Instance.skill.GetDebuffToID(Skill.BuffID).Value;
            BaseStat stat = EnumChanger(GlobalDataTable.Instance.skill.GetDebuffToID(Skill.BuffID).Type);
            buffStart(stat, Attack, BuffTurn, SkillType.debuff);
        }
    

    }
    public IEnumerator buffStart(BaseStat stat, float Attack, int duration, SkillType skill)
    {
        if(skill == SkillType.debuff)
        {
            Attack = -Attack;
        }
        stat.AddMultiples(Attack);
        yield return duration <= 0;
        stat.AddMultiples(-Attack);
    }

   

   


}
