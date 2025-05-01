using System.Collections.Generic;
using UnityEngine;

public class ItemBattleSlots : MonoBehaviour
{
    private GameObject Prefabs;
    private List<ItemData> curStageItemList;
    private Queue<GameObject> slotPool = new Queue<GameObject>();
    private List<GameObject> activeSlots = new List<GameObject>();
    public List<ItemBattleSlot> ItemSlotList = new List<ItemBattleSlot>();
    private int index = 0;

    private void Awake()
    {
        Prefabs = Resources.Load<GameObject>("UI/Stage/ItemSlot");
    }

    public void SetItemList(int stageID)
    {
        // 기존 슬롯 정리
        foreach (var slot in activeSlots)
        {
            slot.GetComponent<ItemBattleSlot>().SlotClear();
            slot.SetActive(false);
            slotPool.Enqueue(slot);
        }
        activeSlots.Clear();

        if (curStageItemList != null)
        {
            curStageItemList.Clear();
        }
        else
        {
            curStageItemList = new List<ItemData>();
        }

        // ChapterManager에서 아이템 리스트 확인
        if (ChapterManager.Instance.StageItemList.ContainsKey(stageID))
        {
            foreach (var item in ChapterManager.Instance.StageItemList[stageID])
            {
                curStageItemList.Add(item);
            }
            CreateSlot();
        }
    }

    public void CreateSlot()
    {
        foreach (ItemData item in curStageItemList)
        {
            GameObject slot;
            if (slotPool.Count > 0)
            {
                slot = slotPool.Dequeue();
                slot.SetActive(true);
            }
            else
            {
                slot = Instantiate(Prefabs, transform);
            }

            slot.GetComponent<ItemBattleSlot>().SetComponent();
            slot.GetComponent<ItemBattleSlot>().SetSlot(item.ID);
            activeSlots.Add(slot);
        }
    }

    public void ClearSlots()
    {
        foreach (GameObject slot in activeSlots)
        {
            slot.GetComponent<ItemBattleSlot>().SlotClear();
            slot.SetActive(false);
            slotPool.Enqueue(slot);
        }
        activeSlots.Clear();
    }
}
