using System.Collections;
using System.Collections.Generic;
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


  
}
