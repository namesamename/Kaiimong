using UnityEngine;

public class ChapterContent : MonoBehaviour
{
    public Chapter Chapter;
    [SerializeField] private StageSlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<StageSlot>();
        Chapter = ChapterManager.Instance.CurChapter;
    }

    void Start()
    {
        InitializeStages();
    }

    private void InitializeStages()
    {
        for (int i = 0; i < Chapter.StagesID.Length; i++)
        {
            StageSaveData stageData = ChapterManager.Instance.GetStageSaveData(Chapter.StagesID[i]);
            if (stageData.StageOpen)
            {
                slots[i].gameObject.SetActive(true);
                slots[i].Stage = GlobalDataTable.Instance.Stage.StageDic[Chapter.StagesID[i]];
                slots[i].StageName.text = GlobalDataTable.Instance.Stage.StageDic[Chapter.StagesID[i]].Name;
            }
            else { slots[i].gameObject.SetActive(false); }
        }
    }
}
