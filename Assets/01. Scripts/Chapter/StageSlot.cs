using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public StageSaveData StageSaveData;
    public Stage Stage;
    public bool ClearedStage = false;

    private void Awake()
    {
        StageSaveData = new StageSaveData();
    }

    public void Initialize(int Id)
    {
        ChapterManager.Instance.InitializeStage(Id);
    }

    public StageSaveData CreatNewData()
    {
        SaveDataBase.Instance.SaveSingleData(StageSaveData);
        return StageSaveData;
    }

    public void LoadData(StageSaveData saveData)
    {
        StageSaveData.ClearedStage = saveData.ClearedStage;
    }

    public void HaveData()
    {
        var foundData = SaveDataBase.Instance.GetSaveInstanceList<StageSaveData>(SaveType.Stage);
        if (foundData != null)
        {
            if (foundData.Find(x => x.ID == StageSaveData.ID) != null)
            {
                LoadData(foundData.Find(x => x.ID == StageSaveData.ID));
            }
            else
            {
                CreatNewData();
            }
        }
        else
        {
            CreatNewData();
        }
    }

    public void Save()
    {
        SaveDataBase.Instance.SaveSingleData(StageSaveData);
    }
}
