using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestBlockSpawner : MonoBehaviour
{

    GameObject QuestBlock;

    List<GameObject> QuestBlocks = new List<GameObject>();

    public async void GetPrefabs()
    {
        QuestBlock = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.Quest, "Quest");
    }


    public void SetQuestBlock(List<Quest> quests)
    {

        QuestBlocks.Clear();

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

    }


  

}
