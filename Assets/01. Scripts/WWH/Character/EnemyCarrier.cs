using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class EnemyCarrier : CharacterCarrier
{
    public override void Initialize(int id, int level = -1)
    {
        visual.Initialize(id , CharacterType.Enemy);
        skillBook.SkillSet(GlobalDataTable.Instance.character.GetEnemyToID(id).SkillID);
        stat.SetEnemy(GlobalDataTable.Instance.character.GetEnemyToID(id), level);
    }
}
