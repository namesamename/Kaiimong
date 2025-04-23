using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : Singleton<ChapterManager>
{
    public ChapterSaveData ChapterSaveData;
    public StageSaveData StageSaveData;

    public Chapter CurChapter;

    public Action OnChapterOpen;

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
        ChapterSaveData = new ChapterSaveData();
        StageSaveData = new StageSaveData();

        SceneLoader.Instance.RegisterSceneAction(SceneState.StageSelectScene, SetChapter);
    }

    private void SetChapter()
    {
        GameObject obj = Instantiate(Resources.Load(CurChapter.ContentPrefabPath) as GameObject);
        
        OnChapterOpen?.Invoke();
    }
    public void InitializeChapter(int Id)
    {
        //초기화
        ChapterSaveData.ID = Id;
        HaveDataChapter();
    }

    public ChapterSaveData CreatNewDataChapter()
    {
        SaveDataBase.Instance.SaveSingleData(ChapterSaveData);
        return ChapterSaveData;
    }

    public void LoadDataChapter(ChapterSaveData saveData)
    {
        ChapterSaveData.ChapterOpen = saveData.ChapterOpen;
    }

    public void HaveDataChapter()
    {
        var foundData = SaveDataBase.Instance.GetSaveInstanceList<ChapterSaveData>(SaveType.Chapter);
        if (foundData != null)
        {
            if (foundData.Find(x => x.ID == ChapterSaveData.ID) != null)
            {
                LoadDataChapter(foundData.Find(x => x.ID == ChapterSaveData.ID));
            }
            else
            {
                CreatNewDataChapter();
            }
        }
        else
        {
            CreatNewDataChapter();
        }
    }

    public void SaveChapter()
    {
        SaveDataBase.Instance.SaveSingleData(ChapterSaveData);
    }

    //Stage SaveData
    public void InitializeStage(int Id)
    {
        //초기화
        StageSaveData.ID = Id;
        HaveDataStage();
    }

    public StageSaveData CreatNewDataStage()
    {
        SaveDataBase.Instance.SaveSingleData(StageSaveData);
        return StageSaveData;
    }

    public void LoadDataStage(StageSaveData saveData)
    {
        StageSaveData.ClearedStage = saveData.ClearedStage;
    }

    public void HaveDataStage()
    {
        var foundData = SaveDataBase.Instance.GetSaveInstanceList<StageSaveData>(SaveType.Stage);
        if (foundData != null)
        {
            if (foundData.Find(x => x.ID == StageSaveData.ID) != null)
            {
                LoadDataStage(foundData.Find(x => x.ID == StageSaveData.ID));
            }
            else
            {
                CreatNewDataStage();
            }
        }
        else
        {
            CreatNewDataStage();
        }
    }

    public void SaveStage()
    {
        SaveDataBase.Instance.SaveSingleData(StageSaveData);
    }
}
