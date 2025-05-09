using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{

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
    }



    public void QuestTypeValueUP(int value, QuestType quest)
    {
        foreach(QuestSaveData item in QuestData.Values) 
        {
            if(item.QuestType == quest)
            {
                item.CurValue += value;
            }
        }
    }

    public void QuestValueUP(int id, int value)
    {
        QuestData[id].CurValue += value;
    }

}
