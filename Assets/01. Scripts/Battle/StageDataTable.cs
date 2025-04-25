using System.Collections.Generic;
using UnityEngine;

public class StageDataTable
{  
    public Dictionary<int, Stage> StageDic = new Dictionary<int, Stage>();

    public void Initialize()
    {
        Stage[] stageSO = Resources.LoadAll<Stage>("Stage");
        for (int i = 0; i < stageSO.Length; i++)
        {
            StageDic[stageSO[i].ID] = stageSO[i];
        }
    }
}
