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
    CriticalAttack
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
            {StatType.Attack, attackStat},
            {StatType.Defense, defenseStat},
            {StatType.Agility, agilityStat},
            {StatType.Health, healthStat},
            {StatType.CriticalPer, criticalPerStat},
            {StatType.CriticalAttack, criticalAttackStat},
        };



    }

    public void SetCharacter(CharacterSO character)
    {
        attackStat.Value = character.Attack;
        defenseStat.Value = character.Defense;
        healthStat.Value = character.Health;
        //agilityStat.Value = character.Speed;
        criticalPerStat.Value = float.Parse(character.CriticalPer.Substring(0, character.CriticalPer.Length - 1));
        criticalAttackStat.Value = float.Parse(character.CriticalAttack.Substring(0, character.CriticalAttack.Length - 1));
    }

    public void TakeDamage(float Amount)
    {
        Debug.Log("TakeDamage");
        //healthStat.AddStat(Amount);
        //if(healthStat.Value == 0)
        //{
        //    OnDie();
        //}
    }
    public void OnDie()
    {
        gameObject.SetActive(false);
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


    public void Buff(SkillSO Skill)
    {
        string effectName;
        int duration;

        if (Skill.IsBuff)
        {
            //effectName = Utility.KoreanValueChanger(GlobalDatabase.Instance.skill.GetBuffToID(int.Parse(Skill.buffSkillId)).Name);
            //duration = GlobalDatabase.Instance.skill.GetBuffToID(int.Parse(Skill.buffSkillId)).Duration;
            Debug.Log("Use Buff");
        }
        else
        {
            //effectName = Utility.KoreanValueChanger(GlobalDatabase.Instance.skill.GetDebuffToID(int.Parse(Skill.buffSkillId)).Name);
            //duration = GlobalDatabase.Instance.skill.GetDebuffToID(int.Parse(Skill.buffSkillId)).Duration;
        }

        //if (Enum.TryParse(effectName, out StatType statType))
        //{
        //    BaseStat stat = EnumChanger(statType);

        //    StartCoroutine(buffStart(stat, Skill, duration));
        //}
        //else
        //{
        //    Debug.Log("Can't Exist StatType");
        //}
    }
   

    public IEnumerator buffStart(BaseStat stat, SkillSO skill, int Duration)
    {
        int[] Damage = skill.damage;

        if (skill.buffSkillId == "2")//디버프 아이디면
        {
            for(int i = 0; skill.damage.Length < i;  i++)
            {
                Damage[i] = -skill.damage[i];
            }
        }
        if (skill.IsMuti)
        {stat.AddMultiples(Damage[0]);}
        else
        {stat.AddStat(Damage[0]);}
        yield return Duration <= 0; //나중에 턴수로 판단
        if (skill.IsMuti)
        {stat.AddMultiples(-Damage[0]);}
        else
        {stat.AddStat(-Damage[0]);}
    }

   

   


}
