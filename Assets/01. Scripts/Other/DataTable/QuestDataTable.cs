
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class QuestDataTable 
{


    public Dictionary<int , Quest> QuestDic = new Dictionary<int , Quest>();
    Dictionary<int, QuestReward> QuestRewardDic = new Dictionary<int , QuestReward>();


    public void Initialize()
    {
        Quest[] quests = Resources.LoadAll<Quest>("Quest");
        QuestReward[] questRe = Resources.LoadAll<QuestReward>("QuestReward");

        for (int i = 0; i < quests.Length; i++)
        {
            QuestDic[quests[i].ID] = quests[i];

        }
        for (int i = 0;i < questRe.Length;i++)
        {
            QuestRewardDic[questRe[i].ID] = questRe[i];
        }
    }

    public List<Quest> GetQuestList(TimeType time)
    {
        List<Quest> Daily = new List<Quest>();
        List<Quest> Weekly = new List<Quest>();
        foreach (Quest quest in QuestDic.Values)
        {
            if (quest.TimeType == TimeType.Daily)
            {
                Daily.Add(quest);
            }
            else if(quest.TimeType == TimeType.Weekly)
            {
                Weekly.Add(quest);
            }
        }
        switch (time) 
        {
            case TimeType.Daily:
                return Daily;
            case TimeType.Weekly:
                return Weekly;
            default:
                return null;
        }
    }

    public Quest GetQuestToID(int ID)
    {
        if (QuestDic.ContainsKey(ID) && QuestDic[ID] != null)
        {
            return QuestDic[ID];
        }
        else
        {
            return null;
        }
    }

    public QuestReward GetQuestRewardToID(int ID) 
    {
        if (QuestRewardDic.ContainsKey(ID) && QuestRewardDic[ID] != null)
        {
            return QuestRewardDic[ID];
        }
        else
        {
            return null;
        }
    }
  
}
