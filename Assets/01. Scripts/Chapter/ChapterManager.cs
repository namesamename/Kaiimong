using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : Singleton<ChapterManager>
{
    private ChapterCategoryDataTable categoryDataTable;
    public Chapter CurChapter;

    public ChapterCategoryDataTable CategoryDataTable {  get { return categoryDataTable; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        categoryDataTable = new ChapterCategoryDataTable();

        SceneLoader.Instance.RegisterSceneAction(SceneState.StageSelectScene, SetChapter);
    }

    private void SetChapter()
    {

    }
}
