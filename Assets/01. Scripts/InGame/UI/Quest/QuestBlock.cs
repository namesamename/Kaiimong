using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBlock : MonoBehaviour
{

    TextMeshProUGUI[] textMeshPros;
    Image[] images;


    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        images = GetComponentsInChildren<Image>();
    }


    public void SetBlock(int ID)
    {
        Quest quest = GlobalDataTable.Instance.Quest.GetQuestToID(ID);
        QuestReward questReward = GlobalDataTable.Instance.Quest.GetQuestRewardToID(ID);
        QuestSaveData Save = SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, ID);

    }


    public void TextSet(QuestSaveData questsave , Quest quest)
    {

    }


}
