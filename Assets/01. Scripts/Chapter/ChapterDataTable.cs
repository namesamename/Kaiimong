using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterDataTable
{
    public Dictionary<int, Chapter> ChapterDic = new Dictionary<int, Chapter>();

    public void Initialize()
    {
        Chapter[] stageSO = Resources.LoadAll<Chapter>("Chapter");
        for (int i = 0; i < stageSO.Length; i++)
        {
            ChapterDic[stageSO[i].ID] = stageSO[i];
        }
    }
}
