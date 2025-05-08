
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



    public Quest GetQuestToID(int ID)
    {
        if (QuestDic.ContainsKey(ID) && QuestDic[ID] != null)
        {
            return QuestDic[ID];
        }
        else
        {
            Debug.Log("This ID is incorrect");
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
            Debug.Log("This ID is incorrect");
            return null;
        }
    }
  
}
