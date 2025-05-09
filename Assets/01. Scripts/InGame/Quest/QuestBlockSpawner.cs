using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestBlockSpawner : MonoBehaviour
{

    GameObject QuestBlock;

    List<GameObject> QuestBlocks = new List<GameObject>();


    Action Action;

    public async void GetPrefabs()
    {
        QuestBlock = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.Quest, "Quest");
        Action?.Invoke();
    }


    public void SetAction(Action action)
    {
        Action += action;
    }

    public void SetQuestBlock(List<Quest> quests)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            QuestBlocks.Clear();
            Destroy(transform.GetChild(i).gameObject);

        }
        foreach (Quest quest in quests) 
        {
            GameObject game = Instantiate(QuestBlock, transform);
            game.GetComponent<QuestBlock>().SetBlock(quest.ID);
            QuestBlocks.Add(game);
        }
        QuestBlocks =QuestBlocks.
            OrderBy(Item => Item.GetComponent<QuestBlock>().GetSave().IsComplete).
            ThenByDescending(Item => Item.GetComponent<QuestBlock>().GetSave().IsCan).
            ToList();

        for (int i = 0; i < QuestBlocks.Count; i++)
        {
            QuestBlocks[i].transform.SetSiblingIndex(i);
        }

    }


  

}
