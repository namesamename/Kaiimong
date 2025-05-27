using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    TimeSpan resetTime = new TimeSpan(5, 0, 0); // 오전 5시 초기화
    Dictionary<int, QuestSaveData> QuestData = new Dictionary<int, QuestSaveData>();

    public void Initialize()
    {
        for (int i = 1; i < GlobalDataTable.Instance.Quest.QuestDic.Count + 1; i++)
        {
            int QuestID = GlobalDataTable.Instance.Quest.QuestDic[i].ID;
            QuestSaveData newQuestData = new QuestSaveData();
            newQuestData.ID = QuestID;
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<QuestSaveData>(SaveType.Quest);
            if (foundData != null && foundData.Find(x => x.ID == QuestID) != null)
            {
                var savedData = foundData.Find(x => x.ID == QuestID);
                newQuestData.CurValue = savedData.CurValue;
                newQuestData.IsComplete = savedData.IsComplete;
                newQuestData.Savetype = SaveType.Quest;
                newQuestData.QuestType = savedData.QuestType;
                newQuestData.IsCan = savedData.IsCan;
            }
            else
            {
                newQuestData.CurValue = 0;
                newQuestData.Savetype = SaveType.Quest;
                newQuestData.QuestType = GlobalDataTable.Instance.Quest.GetQuestToID(QuestID).QuestType;
                newQuestData.IsCan = false;
                newQuestData.IsComplete = false;

                SaveDataBase.Instance.SaveSingleData(newQuestData);
                Debug.Log("Quest data NO");
            }

            QuestData[QuestID] = newQuestData;
        }

 
        CheckDaily();
        CheckWeekly();
    }

    void CheckDaily()
    {
        TimeSaveData timeSave = GetOrCreateTimeSaveData();
        DateTime now = DateTime.Now;
        DateTime todayResetTime = DateTime.Today + resetTime;

        if (now.TimeOfDay < resetTime)
        {
            todayResetTime = todayResetTime.AddDays(-1);
        }

        if (timeSave.lastDailyReset < todayResetTime)
        {
            Debug.Log($"Daily Quest Reset: {now}");
            ResetQuest(TimeType.Daily);
            timeSave.lastDailyReset = todayResetTime; // now 대신 todayResetTime 저장
            SaveDataBase.Instance.SaveSingleData(timeSave);
            CurrencyManager.Instance.ResetPurchase();
        }
    }

    void CheckWeekly()
    {
        TimeSaveData timeSave = GetOrCreateTimeSaveData();
        DateTime now = DateTime.Now; 
        DateTime thisWeekReset = GetMondayOfThisWeek() + resetTime;


        if (now < thisWeekReset)
        {
            thisWeekReset = thisWeekReset.AddDays(-7);
        }


        if (timeSave.lastWeeklyReset < thisWeekReset)
        {
            Debug.Log($"Weekly Quest Reset: {now}");
            ResetQuest(TimeType.Weekly);
            timeSave.lastWeeklyReset = now;
            SaveDataBase.Instance.SaveSingleData(timeSave);
        }
    }


    TimeSaveData GetOrCreateTimeSaveData()
    {
        TimeSaveData timeSave = SaveDataBase.Instance.GetSaveDataToID<TimeSaveData>(SaveType.Time, 0);

        if (timeSave == null)
        {
            timeSave = new TimeSaveData()
            {
                Savetype = SaveType.Time,
                ID = 0,
                lastDailyReset = DateTime.MinValue, // 처음이므로 무조건 초기화되도록
                lastWeeklyReset = DateTime.MinValue,
            };
            SaveDataBase.Instance.SaveSingleData(timeSave);
        }

        return timeSave;
    }


    DateTime GetMondayOfThisWeek()
    {
        DateTime today = DateTime.Today;
        int diff = (7 + (int)today.DayOfWeek - (int)DayOfWeek.Monday) % 7;
        return today.AddDays(-diff);
    }

    public void ResetQuest(TimeType timeType)
    {
        if (timeType == TimeType.Daily)
        {
            foreach (Quest quest in GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Daily))
            {
                QuestSaveData questSave = SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, quest.ID);
                if (questSave != null)
                {
                    questSave.IsCan = false;
                    questSave.CurValue = 0;
                    questSave.IsComplete = false;
                    SaveDataBase.Instance.SaveSingleData(questSave);

                    if (QuestData.ContainsKey(quest.ID))
                    {
                        QuestData[quest.ID] = questSave;
                    }
                }
            }
        }
        else if (timeType == TimeType.Weekly)
        {
            foreach (Quest quest in GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Weekly))
            {
                QuestSaveData questSave = SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, quest.ID);
                if (questSave != null)
                {
                    questSave.IsCan = false;
                    questSave.CurValue = 0;
                    questSave.IsComplete = false;
                    SaveDataBase.Instance.SaveSingleData(questSave);
                    if (QuestData.ContainsKey(quest.ID))
                    {
                        QuestData[quest.ID] = questSave;
                    }
                }
            }
        }
    }
    public void TimeCheck()
    {
        TimeSaveData timeSave = GetOrCreateTimeSaveData();
        timeSave.lastWeeklyReset = DateTime.Now;
        timeSave.lastDailyReset = DateTime.Now;
        SaveDataBase.Instance.SaveSingleData(timeSave);
    }
    public void QuestTypeValueUP(int value, QuestType quest)
    {
        foreach (QuestSaveData item in QuestData.Values)
        {
            if (item.QuestType == quest)
            {
                item.CurValue += value;
                SaveDataBase.Instance.SaveSingleData(item);
            }
        }
    }

    public void QuestValueUP(int id, int value)
    {
        if (QuestData.ContainsKey(id))
        {
            QuestData[id].CurValue += value;
            SaveDataBase.Instance.SaveSingleData(QuestData[id]);
        }
    }
}