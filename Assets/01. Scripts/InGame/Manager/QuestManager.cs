using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    TimeSpan resetTime = new TimeSpan(5, 0, 0);
    Dictionary<int, QuestSaveData> QuestData = new Dictionary<int, QuestSaveData>();


    private void Start()
    {
        CheckDaily();
        CheckWeekly();
    }

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
    }

    void CheckWeekly()
    {
        TimeSaveData timeSave = SaveDataBase.Instance.GetSaveDataToID<TimeSaveData>(SaveType.Time, 0);

        if (timeSave != null)
        {
            DateTime now = DateTime.Now;
            DateTime thisTime = GetMonday() + resetTime;
            DateTime lastTime = thisTime.AddDays(-7);
            DateTime lastWeekly = timeSave.lastWeeklyReset;
            if (now >= thisTime && lastWeekly < thisTime || (now < thisTime && lastWeekly < lastTime))
            {
                ResetQuest(TimeType.Weekly);
            }
        }
        else
        {
            TimeSaveData newtimeSave = new TimeSaveData()
            {
                Savetype = SaveType.Time,
                ID = 0,
                lastDailyReset = DateTime.Now,
                lastWeeklyReset = DateTime.Now,
            };


            SaveDataBase.Instance.SaveSingleData(newtimeSave);
        }
    }

    void CheckDaily()
    {
        TimeSaveData timeSave = SaveDataBase.Instance.GetSaveDataToID<TimeSaveData>(SaveType.Time, 0);

        if(timeSave != null)
        {
            DateTime now = DateTime.Now;  
            DateTime lastDaily = timeSave.lastDailyReset; 
            DateTime todayResetTime = DateTime.Today + resetTime;  
            DateTime yesterdayResetTime = todayResetTime.AddDays(-1);
            if ((now >= todayResetTime && lastDaily < todayResetTime) ||(now < todayResetTime && lastDaily < yesterdayResetTime))
            {
                ResetQuest(TimeType.Daily);
                timeSave.lastDailyReset = now;
                SaveDataBase.Instance.SaveSingleData(timeSave);
            }
        }
        else
        {
            TimeSaveData newtimeSave = new TimeSaveData()
            {
                Savetype = SaveType.Time,
                ID = 0,
                lastDailyReset = DateTime.Now,
                lastWeeklyReset = DateTime.Now,
            };
            SaveDataBase.Instance.SaveSingleData(newtimeSave);
        }

    
    }

    DateTime GetMonday()
    {
        DateTime today = DateTime.Today;
        int WhenIsMonday = (int)DayOfWeek.Monday - (int)today.DayOfWeek;
        if (WhenIsMonday == 0)
            return today;
        return today.AddDays(WhenIsMonday);
    }



    public void ResetQuest(TimeType timeType)
    {
        if (timeType == TimeType.Daily)
        {
            foreach (Quest quest in GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Daily))
            {
                if (SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, quest.ID) != null)
                {
                    QuestSaveData questSave = SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, quest.ID);
                    questSave.IsCan = false;
                    questSave.CurValue = 0;
                    questSave.IsComplete = false;
                }
            }
        }
        else
        {
            foreach (Quest quest in GlobalDataTable.Instance.Quest.GetQuestList(TimeType.Weekly))
            {
                if (SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, quest.ID) != null)
                {
                    QuestSaveData questSave = SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, quest.ID);
                    questSave.IsCan = false;
                    questSave.CurValue = 0;
                    questSave.IsComplete = false;
                }
            }
        }
    }



    public void TimeCheck()
    {
        TimeSaveData timeSave = SaveDataBase.Instance.GetSaveDataToID<TimeSaveData>(SaveType.Time, 0);
        timeSave.lastWeeklyReset = DateTime.Now;
        timeSave.lastDailyReset = DateTime.Now;
        SaveDataBase.Instance.SaveSingleData(timeSave);
    }



    public void QuestTypeValueUP(int value, QuestType quest)
    {
        foreach(QuestSaveData item in QuestData.Values) 
        {
            if(item.QuestType == quest)
            {
                item.CurValue += value;
                SaveDataBase.Instance.SaveSingleData(item);
               //
            }
            else
            {
                //Debug.Log("NoOk");
            }
        }
    }

    public void QuestValueUP(int id, int value)
    {
        QuestData[id].CurValue += value;
    }

}
