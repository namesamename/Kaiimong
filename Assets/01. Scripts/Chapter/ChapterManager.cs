using System;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : Singleton<ChapterManager>
{
    public ChapterSaveData ChapterSaveData;
    public StageSaveData StageSaveData;

    public Dictionary<int, ChapterSaveData> ChaptersSaveList = new Dictionary<int, ChapterSaveData>();
    public Dictionary<int, StageSaveData> StageSaveList = new Dictionary<int, StageSaveData>();

    public Chapter CurChapter;

    public Action OnChapterOpen;


    public void Initailize()
    {
        ChapterSaveData = new ChapterSaveData();
        StageSaveData = new StageSaveData();

        // 모든 챕터 초기화
        for (int i = 1; i < GlobalDataTable.Instance.Chapter.ChapterDic.Count + 1; i++)
        {
            int chapterId = GlobalDataTable.Instance.Chapter.ChapterDic[i].ID;
            ChapterSaveData newChapterData = new ChapterSaveData();
            newChapterData.ID = chapterId;

            // 저장된 데이터가 있는지 확인
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<ChapterSaveData>(SaveType.Chapter);
            if (foundData != null && foundData.Find(x => x.ID == chapterId) != null)
            {
                // 저장된 데이터 로드
                var savedData = foundData.Find(x => x.ID == chapterId);
                newChapterData.ChapterOpen = savedData.ChapterOpen;
            }
            else
            {
                // 새 데이터 생성
                newChapterData.Savetype = SaveType.Chapter;
                SaveDataBase.Instance.SaveSingleData(newChapterData);
            }

            // 사전에 저장
            ChaptersSaveList[chapterId] = newChapterData;
        }

        ChaptersSaveList[1].ChapterOpen = true;
        SaveDataBase.Instance.SaveSingleData(ChaptersSaveList[1]);


        // 모든 스테이지 초기화
        for (int i = 1; i < GlobalDataTable.Instance.Stage.StageDic.Count + 1; i++)
        {
            int stageId = GlobalDataTable.Instance.Stage.StageDic[i].ID;
            StageSaveData newStageData = new StageSaveData();
            newStageData.ID = stageId;

            // 저장된 데이터가 있는지 확인
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<StageSaveData>(SaveType.Stage);
            if (foundData != null && foundData.Find(x => x.ID == stageId) != null)
            {
                // 저장된 데이터 로드
                var savedData = foundData.Find(x => x.ID == stageId);
                newStageData.ClearedStage = savedData.ClearedStage;
                newStageData.StageOpen = savedData.StageOpen;
            }
            else
            {
                // 새 데이터 생성
                newStageData.Savetype = SaveType.Stage;
                SaveDataBase.Instance.SaveSingleData(newStageData);
            }

            // 사전에 저장
            StageSaveList[stageId] = newStageData;
        }

        StageSaveList[1].StageOpen = true;
        SaveDataBase.Instance.SaveSingleData(StageSaveList[1]);        
    }

    public void RegisterChapter(Chapter chapter)
    {
        CurChapter = chapter;
        SceneLoader.Instance.RegisterSceneAction(SceneState.StageSelectScene, SetChapter);
    }
  
    private void SetChapter()
    {
        GameObject obj = Instantiate(Resources.Load(CurChapter.ContentPrefabPath) as GameObject);
        
        OnChapterOpen?.Invoke();
    }

    public ChapterSaveData GetChapterSaveData(int chapterId)
    {
        if (ChaptersSaveList.ContainsKey(chapterId))
        {
            return ChaptersSaveList[chapterId];
        }
        return null;
    }

    public StageSaveData GetStageSaveData(int stageId)
    {
        if (StageSaveList.ContainsKey(stageId))
        {
            return StageSaveList[stageId];
        }
        return null;
    }

    public void SaveAllData()
    {
        foreach (var chapterData in ChaptersSaveList.Values)
        {
            SaveDataBase.Instance.SaveSingleData(chapterData);
        }

        foreach (var stageData in StageSaveList.Values)
        {
            SaveDataBase.Instance.SaveSingleData(stageData);
        }
    }

    public void SaveChapterSingleData(int chpaterID)
    {
        SaveDataBase.Instance.SaveSingleData(ChaptersSaveList[chpaterID]);
    }

    public void SaveStageSingleData(int stageID)
    {
        SaveDataBase.Instance.SaveSingleData(StageSaveList[stageID]);
    }

    //public void InitializeChapter(int Id)
    //{
    //    //초기화
    //    ChapterSaveData.ID = Id;
    //    HaveDataChapter();
    //}

    //public ChapterSaveData CreatNewDataChapter()
    //{
    //    ChapterSaveData.Savetype = SaveType.Chapter;
    //    SaveDataBase.Instance.SaveSingleData(ChapterSaveData);
    //    return ChapterSaveData;
    //}

    //public void LoadDataChapter(ChapterSaveData saveData)
    //{
    //    ChapterSaveData.ChapterOpen = saveData.ChapterOpen;
    //}

    //public void HaveDataChapter()
    //{
    //    var foundData = SaveDataBase.Instance.GetSaveInstanceList<ChapterSaveData>(SaveType.Chapter);
    //    if (foundData != null)
    //    {
    //        if (foundData.Find(x => x.ID == ChapterSaveData.ID) != null)
    //        {
    //            LoadDataChapter(foundData.Find(x => x.ID == ChapterSaveData.ID));
    //        }
    //        else
    //        {
    //            ChapterSaveData = CreatNewDataChapter();
    //        }
    //    }
    //    else
    //    {
    //        ChapterSaveData = CreatNewDataChapter();
    //    }
    //}

    //public void SaveChapter()
    //{
    //    SaveDataBase.Instance.SaveSingleData(ChapterSaveData);
    //}

    ////Stage SaveData
    //public void InitializeStage(int Id)
    //{
    //    //초기화
    //    StageSaveData.ID = Id;
    //    HaveDataStage();
    //}

    //public StageSaveData CreatNewDataStage()
    //{
    //    StageSaveData.Savetype = SaveType.Stage;
    //    SaveDataBase.Instance.SaveSingleData(StageSaveData);
    //    return StageSaveData;
    //}

    //public void LoadDataStage(StageSaveData saveData)
    //{
    //    StageSaveData.ClearedStage = saveData.ClearedStage;
    //    StageSaveData.StageOpen = saveData.StageOpen;
    //}

    //public void HaveDataStage()
    //{
    //    var foundData = SaveDataBase.Instance.GetSaveInstanceList<StageSaveData>(SaveType.Stage);
    //    if (foundData != null)
    //    {
    //        if (foundData.Find(x => x.ID == StageSaveData.ID) != null)
    //        {
    //            LoadDataStage(foundData.Find(x => x.ID == StageSaveData.ID));
    //        }
    //        else
    //        {
    //            StageSaveData = CreatNewDataStage();
    //        }
    //    }
    //    else
    //    {
    //        StageSaveData = CreatNewDataStage();
    //    }
    //}

    //public void SaveStage()
    //{
    //    SaveDataBase.Instance.SaveSingleData(StageSaveData);
    //}
}
