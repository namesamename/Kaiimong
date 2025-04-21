using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterContent : MonoBehaviour
{
    public ChapterSaveData ChapterSaveData;
    public Chapter Chapter;
    public bool ChapterOpen = false;
    [SerializeField] private StageSlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<StageSlot>();
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
        }
    }

    public void Initialize(int Id)
    {
        //ÃÊ±âÈ­
        ChapterSaveData.ID = Id;
        HaveData();
    }

    public ChapterSaveData CreatNewData()
    {
        SaveDataBase.Instance.SaveSingleData(ChapterSaveData);
        return ChapterSaveData;
    }

    public void LoadData(ChapterSaveData saveData)
    {
        ChapterSaveData.ChapterOpen = saveData.ChapterOpen;
    }

    public void HaveData()
    {
        var foundData = SaveDataBase.Instance.GetSaveInstanceList<ChapterSaveData>(SaveType.Chapter);
        if (foundData != null)
        {
            if (foundData.Find(x => x.ID == ChapterSaveData.ID) != null)
            {
                LoadData(foundData.Find(x => x.ID == ChapterSaveData.ID));
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
        SaveDataBase.Instance.SaveSingleData(ChapterSaveData);
    }
}
