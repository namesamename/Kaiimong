using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScroll : MonoBehaviour
{
    QuestBlockSpawner spawner;
    QuestInsideBtn insideBtn;
    QuestSlider slider;

    public TimeType time;



    private void Awake()
    {
        spawner = GetComponentInChildren<QuestBlockSpawner>();
        insideBtn = GetComponentInChildren<QuestInsideBtn>();
        slider = GetComponentInChildren<QuestSlider>();
        spawner.SetAction(() => DailySetting());
        spawner.GetPrefabs();

    }


    private void Start()
    {
        insideBtn.SetBtn(() => DailySetting(), () => WeeklySetting());
    }


    public void SetCurrentSetting()
    {
        switch (time) 
        {
            case TimeType.Daily:
                DailySetting();
                break;
            case TimeType.Weekly:
                WeeklySetting();
                break;
        }
    }


    public void DailySetting()
    {
        spawner.SetQuestBlock(GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Daily));
        slider.SetSlider(GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Daily));
        time = TimeType.Daily;
    }

    public void WeeklySetting()
    {
        spawner.SetQuestBlock(GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Weekly));
        slider.SetSlider(GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Weekly));
        time = TimeType.Weekly;
    }



}
