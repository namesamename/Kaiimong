using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChapterContent : MonoBehaviour
{
    public Chapter Chapter;
    [SerializeField] private StageSlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<StageSlot>();
        Chapter = GlobalDataTable.Instance.Chapter.ChapterDic[1];
        ChapterManager.Instance.InitializeChapter(Chapter.ID);
    }

    void Start()
    {
        InitializeStages();
    }

    void Update()
    {

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
