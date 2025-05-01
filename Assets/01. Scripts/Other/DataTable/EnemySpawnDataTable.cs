using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDataTable
{
    public Dictionary<int, List<EnemySpawn>> EnemySpawnDic = new Dictionary<int, List<EnemySpawn>>();

    public void Initialize()
    {
        List<EnemySpawn> enemySpawnSO = Resources.LoadAll<EnemySpawn>("EnemySpawn").ToList();
        foreach(var data in enemySpawnSO)
        {
            if (!EnemySpawnDic.ContainsKey(data.Stage))
            {
                EnemySpawnDic[data.Stage] = new List<EnemySpawn>();
            }
            EnemySpawnDic[data.Stage].Add(data);
        }
    }
}
