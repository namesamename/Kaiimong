using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class QuestDataTable
{
    public Dictionary<int, Quest> QuestDic = new Dictionary<int, Quest>();
    public Dictionary<int, QuestReward> RewardDic = new Dictionary<int, QuestReward>();


    public void InitialIze()
    {
        Quest[] quests = Resources.LoadAll<Quest>("Quest");
        QuestReward[] Reward = Resources.LoadAll<QuestReward>("QuestReward");


        for(int i = 0; i < quests.Length; i++)
        {
            QuestDic[quests[i].ID] = quests[i];
        }
        for(int i = 0;i < Reward.Length;i++) 
        {
            RewardDic[Reward[i].ID] = Reward[i];
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
        if (RewardDic.ContainsKey(ID) && RewardDic[ID] != null)
        {
            return RewardDic[ID];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }



}
