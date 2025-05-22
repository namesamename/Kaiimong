using System;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : Singleton<ChapterManager>
{
    public ChapterSaveData ChapterSaveData;
    public StageSaveData StageSaveData;

    public Dictionary<int, ChapterSaveData> ChaptersSaveList = new Dictionary<int, ChapterSaveData>();
    public Dictionary<int, StageSaveData> StageSaveList = new Dictionary<int, StageSaveData>();
    public Dictionary<int, List<ItemData>> StageItemList = new Dictionary<int, List<ItemData>>();

    public GameObject SpawnedChapter;

    public Chapter CurChapter;

    public Action OnChapterOpen;

    private void Start()
    {
        foreach (var stages in GlobalDataTable.Instance.Stage.StageDic)
        {
            int stageID = stages.Key;
            Stage stage = stages.Value;
            List<ItemData> itemList = new List<ItemData>();

            foreach(int itemID in stage.ItemID)
            {
                if (GlobalDataTable.Instance.Item.ItemDic.TryGetValue(itemID, out ItemData item))
                {
                    itemList.Add(item);
                }
            }

            StageItemList[stageID] = itemList;
        }
    }

    public void Initailize()
    {
        ChapterSaveData = new ChapterSaveData();
        StageSaveData = new StageSaveData();

        // ��� é�� �ʱ�ȭ
        for (int i = 1; i < GlobalDataTable.Instance.Chapter.ChapterDic.Count + 1; i++)
        {
            int chapterId = GlobalDataTable.Instance.Chapter.ChapterDic[i].ID;
            ChapterSaveData newChapterData = new ChapterSaveData();
            newChapterData.ID = chapterId;

            // ����� �����Ͱ� �ִ��� Ȯ��
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<ChapterSaveData>(SaveType.Chapter);
            if (foundData != null && foundData.Find(x => x.ID == chapterId) != null)
            {
                // ����� ������ �ε�
                var savedData = foundData.Find(x => x.ID == chapterId);
                newChapterData.ChapterOpen = savedData.ChapterOpen;
                newChapterData.Savetype = SaveType.Chapter;
            }
            else
            {
                // �� ������ ����
                newChapterData.Savetype = SaveType.Chapter;
                SaveDataBase.Instance.SaveSingleData(newChapterData);
            }

            // ������ ����
            ChaptersSaveList[chapterId] = newChapterData;
        }

        ChaptersSaveList[1].ChapterOpen = true;
        SaveDataBase.Instance.SaveSingleData(ChaptersSaveList[1]);


        // ��� �������� �ʱ�ȭ
        for (int i = 1; i < GlobalDataTable.Instance.Stage.StageDic.Count + 1; i++)
        {
            int stageId = GlobalDataTable.Instance.Stage.StageDic[i].ID;
            StageSaveData newStageData = new StageSaveData();
            newStageData.ID = stageId;

            // ����� �����Ͱ� �ִ��� Ȯ��
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<StageSaveData>(SaveType.Stage);
            if (foundData != null && foundData.Find(x => x.ID == stageId) != null)
            {
                // ����� ������ �ε�
                var savedData = foundData.Find(x => x.ID == stageId);
                newStageData.ClearedStage = savedData.ClearedStage;
                newStageData.StageOpen = savedData.StageOpen;
                newStageData.TargetOne = savedData.TargetOne;
                newStageData.TargetTwo = savedData.TargetTwo;
                newStageData.Savetype = SaveType.Stage;
            }
            else
            {
                // �� ������ ����
                newStageData.Savetype = SaveType.Stage;
                SaveDataBase.Instance.SaveSingleData(newStageData);
            }

            // ������ ����
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
        if(SpawnedChapter != null)
        {
            Destroy(SpawnedChapter);
        }

        SpawnedChapter = Instantiate(Resources.Load(CurChapter.ContentPrefabPath) as GameObject);
        
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
}
