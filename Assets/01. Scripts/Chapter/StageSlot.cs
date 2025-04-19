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
        //√ ±‚»≠
        StageSaveData.ID = Id;
        HaveData();
    }

    public StageSaveData CreatNewData()
    {
        SaveDataBase.Instance.SetSingleSaveInstance(StageSaveData, SaveType.Stage);
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
        SaveDataBase.Instance.SetSingleSaveInstance(StageSaveData, SaveType.Stage);
    }
}
