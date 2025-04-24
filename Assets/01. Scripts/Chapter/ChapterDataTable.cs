using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterDataTable
{
    public Dictionary<int, Chapter> ChapterDic = new Dictionary<int, Chapter>();

    public void Initialize()
    {
        Chapter[] ChapterSO = Resources.LoadAll<Chapter>("Chapter");
        for (int i = 0; i < ChapterSO.Length; i++)
        {
            ChapterDic[ChapterSO[i].ID] = ChapterSO[i];
        }
    }
}
