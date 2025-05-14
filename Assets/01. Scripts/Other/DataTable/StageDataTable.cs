using System.Collections.Generic;
using UnityEngine;

public class StageDataTable
{  
    public Dictionary<int, Stage> StageDic = new Dictionary<int, Stage>();


    int StageCount = 0;
    public void Initialize()
    {
        Stage[] stageSO = Resources.LoadAll<Stage>("Stage");
        StageCount = stageSO.Length;
        for (int i = 0; i < stageSO.Length; i++)
        {
            StageDic[stageSO[i].ID] = stageSO[i];
        }
    }


    public int GetStage()
    {
        return StageCount;
    }
}
