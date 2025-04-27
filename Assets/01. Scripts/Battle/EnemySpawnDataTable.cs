using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDataTable
{
    public Dictionary<int, EnemySpawn> EnemySpawnDic = new Dictionary<int, EnemySpawn>();

    public void Initialize()
    {
        EnemySpawn[] enemySpawnSO = Resources.LoadAll<EnemySpawn>("EnemySpawn");
        for (int i = 0; i < enemySpawnSO.Length; i++)
        {
            EnemySpawnDic[enemySpawnSO[i].ID] = enemySpawnSO[i];
        }
    }
}
