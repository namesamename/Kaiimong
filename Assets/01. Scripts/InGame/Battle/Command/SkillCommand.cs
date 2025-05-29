using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCommand
{
    public ActiveSkillObject skillData;
    public CharacterCarrier unit;
    public List<CharacterCarrier> targets;

    public SkillCommand(CharacterCarrier unit, List<CharacterCarrier> targets, ActiveSkillObject skillData)
    {
        this.targets = new List<CharacterCarrier>(targets);
        this.unit = unit;
        this.skillData = skillData;
    }
    public IEnumerator Execute()
    {
        List<CharacterCarrier> validTargets = FilterValidTargets();
        unit.skillBook.ActiveSkillUsing(skillData, validTargets);
        unit.skillBook.SetSkillGauge(skillData, validTargets);

        float waitTime = 0f;

        int id = skillData.SkillSO.ID % 3;
        int animIndex = id == 1 ? 3 : id == 2 ? 4 : 5;

        waitTime = unit.visual.GetAnimationLength(animIndex);

        yield return new WaitForSeconds(waitTime + 0.5f);
    }

    private List<CharacterCarrier> FilterValidTargets()
    {
        List<CharacterCarrier> validTargets = new List<CharacterCarrier>();
        BattleSystem battleSystem = StageManager.Instance.BattleSystem;

        bool casterIsPlayer = battleSystem.GetActivePlayers().Contains(unit);

        foreach (CharacterCarrier target in targets)
        {
       
            if (target == null || target.stat.healthStat.CurHealth <= 0)
                continue;

            bool targetIsPlayer = battleSystem.GetActivePlayers().Contains(target);

            if (skillData.SkillSO.IsBuff)
            {

                if (casterIsPlayer == targetIsPlayer)
                {
                    validTargets.Add(target);
          
                }
     
            }
            else
            {
        
                if (casterIsPlayer != targetIsPlayer)
                {
                    validTargets.Add(target);
             
                }
            }
        }

        return validTargets;
    }
}
