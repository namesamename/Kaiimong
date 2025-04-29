using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class ItemBattleSlots : MonoBehaviour
{
    private GameObject Prefabs;
    private List<ItemSaveData> saveDatas = new List<ItemSaveData>();
    private List<GameObject> Slots = new List<GameObject>();
    public List<ItemBattleSlot> ItemSlotList = new List<ItemBattleSlot>();
    private int index = 0;

    private void Awake()
    {
        Prefabs = Resources.Load<GameObject>("Battle/Stage/ItemSlot");
    }

    public List<GameObject> GetSlots()
    {
        return Slots;
    }
    public void CreateSlot()
    {
        List<ItemSaveData> list = SaveDataBase.Instance.GetSaveInstanceList<ItemSaveData>(SaveType.Item);
        if (Slots.Count >= list.Count)
        {
            int index = 0;
            foreach (GameObject gameObject in Slots)
            {
                gameObject.SetActive(true);
                gameObject.GetComponent<ItemBattleSlot>().SetComponent();
                gameObject.GetComponent<ItemBattleSlot>().SetSlot(saveDatas[index].ID);
                index++;
            }
        }
        else
        {
            foreach (ItemSaveData data in list)
            {
                saveDatas.Add(data);
            }
            for (int i = 0; i < saveDatas.Count; i++)
            {
                GameObject slot = Instantiate(Prefabs, transform);
                Slots.Add(slot);
                slot.GetComponent<ItemBattleSlot>().SetComponent();
                slot.GetComponent<ItemBattleSlot>().SetSlot(saveDatas[i].ID);
            }
        }
    }

    public void SelectSlot(ItemBattleSlot slot)
    {
        if (!slot.IsSeted)
        {
            slot.IsSeted = true;
            ItemSlotList.Add(slot);
        }
        else
        {
            slot.IsSeted = false;
            ItemSlotList.Remove(slot);
        }
    }
}
