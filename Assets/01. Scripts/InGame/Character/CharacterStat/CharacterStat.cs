using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;


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

public class BuffInstance
{
    public int buffID;
    public BaseStat targetStat;
    public float value;
    public int remainingTurns;
    public int originalTurns;
    public SkillType skillType;
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
    private Animator animator;


    private Dictionary<int, BuffInstance> activeBuffs = new Dictionary<int, BuffInstance>();

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

    private void Start()
    {
        animator = transform.parent.GetComponentInChildren<Animator>();
    }

    public void SetCharacter(Character character, int Level)
    {
    
        attackStat.Value = character.Attack + Level;
        defenseStat.Value = character.Defence + Level;
        healthStat.Value = character.Health + Level;
        //agilityStat.Value = character.Speed + level;
        criticalAttackStat.Value = character.CriticalAttack + (float)(Level * 0.01);
        criticalPerStat.Value = character.CriticalPer +  (float)(Level * 0.01);
        healthStat.CurHealth = healthStat.Value;
    }
    public void SetEnemy(Enemy enemy, int Level)
    {
        //List<StatGrade> statList = new List<StatGrade>
        //{
        //    enemy.Att,
        //    enemy.Def,
        //    enemy.Cri,
        //    enemy.Speed
        //};
  
        //foreach (StatGrade stat in statList) 
        //{
        //    switch (stat)
        //    {
        //        case StatGrade.A:
        //            break;
        //        case StatGrade.B:
        //            break;
        //        case StatGrade.C:
        //            break;
        //        case StatGrade.D:
        //            break;
        //    }
        //}


        attackStat.Value = enemy.Att + Level;
        defenseStat.Value = enemy.Def + Level;
        healthStat.Value = enemy.HP + Level;
        //agilityStat.Value = enemy.Speed + level;
        criticalAttackStat.Value = enemy.Cri + (float)(Level * 0.01);
        criticalPerStat.Value = enemy.Cri +(float)(Level * 0.01);
        healthStat.CurHealth = healthStat.Value;



    }


    public void TakeDamage(float Amount)
    {
        if(healthStat.CurHealth <= 0)
        {
            return;
        }
        animator.SetTrigger("Hit");
        float Damage = Amount - defenseStat.GetStat();

        if (Damage <= 0)
        {
            Debug.Log("Damage");
            healthStat.CurHealth -= 1f;
        }
        else
        {
            Debug.Log(Damage);
            healthStat.CurHealth -= Damage;
        }
        
        //if(healthStat.CurHealth<=0)
        //{
        //    OnDie();
        //}
     
    }


    public void OnDie()
    {
        animator.SetTrigger("Death");
        OnDeath?.Invoke();
    }




    public BaseStat EnumChanger(StatType statType)
    {
        switch (statType)
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

    public void Buff(ActiveSkill skill)
    {
        if (skill.BuffID == 0) return;

        float value;
        int turns;
        StatType statType;


        if (skill.Type == SkillType.buff)
        {
            var buffData = GlobalDataTable.Instance.skill.GetBuffToID(skill.BuffID);
            value = buffData.Value;
            turns = buffData.Turn;
            statType = buffData.Type;
        }
        else if (skill.Type == SkillType.debuff)
        {
            var debuffData = GlobalDataTable.Instance.skill.GetDebuffToID(skill.BuffID);
            value = -debuffData.Value; 
            turns = debuffData.Turn;
            statType = debuffData.Type;
        }
        else
        {
            return;
        }

        BaseStat targetStat = EnumChanger(statType);
        if (targetStat == null) return;

        ApplyBuff(skill.BuffID, targetStat, value, turns, skill.Type);
    }

    private void ApplyBuff(int buffID, BaseStat targetStat, float value, int turns, SkillType skillType)
    {

        if (activeBuffs.ContainsKey(buffID))
        {
   
            BuffInstance existingBuff = activeBuffs[buffID];
            existingBuff.remainingTurns = turns;
            existingBuff.originalTurns = turns;
        }
        else
        {

            BuffInstance newBuff = new BuffInstance
            {
                buffID = buffID,
                targetStat = targetStat,
                value = value,
                remainingTurns = turns,
                originalTurns = turns,
                skillType = skillType
            };

            // 스탯에 효과 적용
            targetStat.AddMultiples(value);
            activeBuffs[buffID] = newBuff;


        }
    }


    public void OnTurnEnd()
    {
        List<int> buffsToRemove = new List<int>();

        foreach (var kvp in activeBuffs)
        {
            BuffInstance buff = kvp.Value;
            buff.remainingTurns--;

            Debug.Log($"버프 {buff.buffID}: {buff.remainingTurns}턴 남음");

     
            if (buff.remainingTurns <= 0)
            {
                buffsToRemove.Add(kvp.Key);
            }
        }


        foreach (int buffID in buffsToRemove)
        {
            RemoveBuff(buffID);
        }
    }

    private void RemoveBuff(int buffID)
    {
        if (activeBuffs.ContainsKey(buffID))
        {
            BuffInstance buff = activeBuffs[buffID];


            buff.targetStat.AddMultiples(-buff.value);

 
            activeBuffs.Remove(buffID);

         
        }
    }
}
