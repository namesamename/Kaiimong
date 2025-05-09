using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlider : MonoBehaviour
{

    Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }


    public void SetSlider(List<Quest> quests)
    {
        int CompleteCount = 0;

        foreach (Quest quest in quests) 
        {
            if(SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest,quest.ID).IsComplete)
            {
                CompleteCount++;
            }

        }
        slider.value = (float)(CompleteCount /quests.Count);
    }

}
