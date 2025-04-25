using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarrier : MonoBehaviour
{
    Enemy enemy;

    CharacterVisual characterVisual;
    CharacterSkillBook characterSkillBook;
    CharacterStat stat;

    private void Awake()
    {
        characterSkillBook = GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        characterVisual = GetComponentInChildren<CharacterVisual>();
    }


    public void Initialize(int i, int Level)
    {
        enemy = GlobalDataTable.Instance.character.GetEnemyToID(i);
        characterVisual.Initialize();
        characterSkillBook.SkillSet(i);
        stat.SetEnemy(enemy, Level);


    }



   



}
