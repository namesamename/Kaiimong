using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarrier : MonoBehaviour
{
    Enemy enemy;
    [HideInInspector]
    public CharacterVisual characterVisual;
    [HideInInspector]
    public CharacterSkillBook characterSkillBook;
    [HideInInspector]
    public CharacterStat stat;
    public GameObject SelectEffect;

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
        characterSkillBook.SkillSet(enemy.SkillID);
        stat.SetEnemy(enemy, Level);
    }



   



}
