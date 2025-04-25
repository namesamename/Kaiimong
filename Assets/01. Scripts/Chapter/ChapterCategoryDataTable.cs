using System.Collections.Generic;
using UnityEngine;

public class ChapterCategoryDataTable
{
    public Dictionary<int, ChapterCategory> ChapterCategoryDic = new Dictionary<int, ChapterCategory>();

    public void Initialize()
    {
        ChapterCategory[] chapterCategorySO = Resources.LoadAll<ChapterCategory>("ChapterCategory");
        for (int i = 0; i < chapterCategorySO.Length; i++)
        {
            ChapterCategoryDic[chapterCategorySO[i].ID] = chapterCategorySO[i];
        }
    }
}
