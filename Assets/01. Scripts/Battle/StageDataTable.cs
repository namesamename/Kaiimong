using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageDataTable
{
    public Dictionary<int, Stage> CurrencyDic = new Dictionary<int, Stage>();


    public void Initialize()
    {
        Stage[] stageSO = Resources.LoadAll<Stage>("Stage");
        for (int i = 0; i < stageSO.Length; i++)
        {
            CurrencyDic[stageSO[i].ID] = stageSO[i];
        }
    }

}
