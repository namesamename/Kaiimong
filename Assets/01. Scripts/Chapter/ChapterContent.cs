using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterContent : MonoBehaviour
{
    public Chapter Chapter;
    public bool ChapterOpen = false;
    [SerializeField] private StageSlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<StageSlot>();
        ChapterManager.Instance.InitializeChapter(Chapter.ID);
    }

    void Start()
    {
        InitializeStages();
    }

    private void InitializeStages()
    {
        for (int i = 0; i < Chapter.StagesID.Length; i++)
        {
            slots[i].Initialize(Chapter.StagesID[i]);
            if (SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, Chapter.StagesID[i]).StageOpen)
            {
                slots[i].gameObject.SetActive(true);
            }
            else { slots[i].gameObject.SetActive(false); }
        }
    }
}
