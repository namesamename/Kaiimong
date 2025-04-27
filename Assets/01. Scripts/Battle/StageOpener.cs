using UnityEngine;

public class StageOpener : MonoBehaviour
{
    public StageSaveData StageSaveData;

    void LoadStageData()
    {
        StageSaveData = SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, 1);
    }

}
