using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestBlock : MonoBehaviour
{

    TextMeshProUGUI[] textMeshPros;
    Image[] images;
    Button button;
    GameObject ItemSlot;
    QuestReward questReward;
    QuestReward SecondReward;
    Quest questSO;
    QuestSaveData QuestSave;

    QuestScroll QuestScroll;
    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        images = GetComponentsInChildren<Image>();
        button = GetComponentInChildren<Button>();
        ItemSlot = Resources.Load<GameObject>("ItemSlot/Slot");
        QuestScroll =GetComponentInParent<QuestScroll>();
    }


    public QuestSaveData GetSave()
    {
        return QuestSave;
    }

    public Quest GetQuset()
    {
        return questSO;
    }

    public void SetBlock(int ID)
    {
        QuestSave = SaveDataBase.Instance.GetSaveDataToID<QuestSaveData>(SaveType.Quest, ID);
        questSO = GlobalDataTable.Instance.Quest.GetQuestToID(ID);
        questReward = GlobalDataTable.Instance.Quest.GetQuestRewardToID(questSO.RewardTableID);
        ItemSlotItemSet(questReward);
        if(questSO.SecondRewardTableID != 0 ) 
        {
            SecondReward = GlobalDataTable.Instance.Quest.GetQuestRewardToID(questSO.SecondRewardTableID);
            ItemSlotItemSet(SecondReward);
        }
       
        if (QuestSave.CurValue >= questSO.RequiredCount)
        {
            QuestSave.IsCan = true;
        }
        Setting();
    }


    public void Setting()
    {
        transform.DOLocalRotate(Vector3.zero, 1f);
        ButtonSet();
        TextSet();
    }



    public void ItemSlotItemSet(QuestReward reward)
    {

        GameObject game = Instantiate(ItemSlot, transform.GetChild(3));

        switch (reward.Type)
        {
            case RewardType.Item:
                game.GetComponent<InventorySlot>().SetSlot(GlobalDataTable.Instance.Item.GetItemDataToID(reward.ItemID), reward.ItemCount);
                break;
            case RewardType.Gold:
                game.GetComponent<InventorySlot>().SetSlotCurrency(CurrencyType.Gold, reward.CurrencyValue);
                break;
            case RewardType.Ticket:
                game.GetComponent<InventorySlot>().SetSlotCurrency(CurrencyType.Gacha, reward.CurrencyValue);
                break;
            case RewardType.Cystal:
                game.GetComponent<InventorySlot>().SetSlotCurrency(CurrencyType.Dia, reward.CurrencyValue);
                break;
            case RewardType.Stamina:
                game.GetComponent<InventorySlot>().SetSlotCurrency(CurrencyType.Activity, reward.CurrencyValue);
                break;
            case RewardType.UserEXP:
                game.GetComponent<InventorySlot>().SetSlotCurrency(CurrencyType.UserEXP, reward.CurrencyValue);
                break;
            case RewardType.CharacterEXP:
                game.GetComponent<InventorySlot>().SetSlotCurrency(CurrencyType.CharacterEXP, reward.CurrencyValue);
                break;
        }

    }

    public void Getitem(QuestReward quest , QuestReward Second)
    {
        if(quest != null && Second != null)
        {
            GetClear(quest);
            GetClear(Second);
        }
        else
        {
            GetClear(quest);
        }

        
        QuestSave.IsComplete = true;
        Setting();
        QuestScroll.SetCurrentSetting();
        SaveDataBase.Instance.SaveSingleData(QuestSave);

    }

    public void GetClear(QuestReward quest)
    {
        switch (quest.Type) 
        {
            case RewardType.Item:
                ItemManager.Instance.SetitemCount(quest.ItemID, quest.ItemCount); 
            break;
            case RewardType.UserEXP:
                LevelUpSystem.GainPlayerEXP(quest.CurrencyValue);
                break;
            case RewardType.Cystal:
                CurrencyManager.Instance.SetCurrency(CurrencyType.Dia, quest.CurrencyValue);
                break;
            case RewardType.Gold:
                CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, quest.CurrencyValue);
                break;
            case RewardType.Ticket:
                CurrencyManager.Instance.SetCurrency(CurrencyType.Gacha, quest.CurrencyValue);
                break;
            case RewardType.CharacterEXP:
                CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, quest.CurrencyValue);
                break;
            case RewardType.Stamina:
                CurrencyManager.Instance.SetCurrency(CurrencyType.Activity, quest.CurrencyValue);
                break;
        }
    }






    public void ButtonSet()
    {
        button.onClick.RemoveAllListeners();
        if(QuestSave.IsComplete) 
        {
            images[0].color = Color.gray;
            textMeshPros[0].text = "Complete";
            button.interactable = false;
        }
        else if(QuestSave.IsCan && !QuestSave.IsComplete)
        {
            textMeshPros[0].text = "Can";
            button.interactable = true;
            button.onClick.AddListener(() => Getitem(questReward, SecondReward));

        }
        else if(!QuestSave.IsCan && !QuestSave.IsComplete)
        {
            textMeshPros[0].text = "Find";
            button.interactable = true;
            button.onClick.AddListener(() => FindQuest());
        }
    }

    public void FindQuest()
    {
 
        switch(questSO.QuestType) 
        {
            case QuestType.StageClear:
            case QuestType.KillMonster:
            case QuestType.UseSkill:
            case QuestType.UseStamina:
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ChapterScene));
                break;
            case QuestType.UesItem:
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.InventoryScene));
                break;
            case QuestType.CharacterInteraction:
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
                break;
            case QuestType.Gacha:
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.PickupScene));
                break;
            case QuestType.Recognition:
            case QuestType.LevelUp:
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.CharacterSelectScene));
                break;
            case QuestType.UseGold:
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ShopScene));
                break;

     
        }

    }



    public void TextSet()
    {
        if(QuestSave.CurValue >= questSO.RequiredCount)
        {
            textMeshPros[1].text = $"{questSO.RequiredCount}/{questSO.RequiredCount}";
   
        }
        else
        {
            textMeshPros[1].text = $"{QuestSave.CurValue}/{questSO.RequiredCount}";
      
        }
        textMeshPros[2].text = questSO.QuestDescription;
    }


}
