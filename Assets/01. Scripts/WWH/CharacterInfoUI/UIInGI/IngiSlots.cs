using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum UpgradeType
{
    Item,
    Level,
    Gold

}

public class IngiSlots : MonoBehaviour, ISetPOPUp
{

    public GameObject Slots;

    public Dictionary<int,int> NeedTable = new Dictionary<int,int>();

    private void Awake()
    {
        Slots = Resources.Load<GameObject>("ItemSlot/ItemSlot");
    }

    public void Initialize()
    {

        int Rec = GlobalDataTable.Instance.DataCarrier.GetSave().Recognition;
        int Id = GlobalDataTable.Instance.DataCarrier.GetSave().ID;
        List<CharacterUpgradeTable> list = GlobalDataTable.Instance.Upgrade.GetRecoList(Id);

        DeleteSlot();
        if (Rec >= 3)
            return;
   
        SetItemslot(list[Rec]);
    }

    public void SetItemslot(CharacterUpgradeTable table )
    {
  
        if (table.ItemCount >= 3)
        {
            NeedTable[-1] = table.NeedLevel;
            SlotInstantiate(0, UpgradeType.Level);
            NeedTable[0] = table.NeedGold;
            SlotInstantiate(0, UpgradeType.Gold);
            NeedTable[table.FirstItemID] = table.FirstItemCount;
            SlotInstantiate(table.FirstItemID);
            NeedTable[table.SecondItemID] = table.SecondItemCount;
            SlotInstantiate(table.SecondItemID);
        }
        if (table.ItemCount >= 4)
        {
            NeedTable[table.ThridItemID] = table.ThridItemCount;
            SlotInstantiate(table.ThridItemID);
        }
        if (table.ItemCount >= 5)
        {
            NeedTable[table.FourthItemID] = table.FourthItemCount;
            SlotInstantiate(table.FourthItemID);
        }
    }

    public void DeleteSlot()
    {
        if (transform.childCount > 0)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }
    public void SlotInstantiate(int id, UpgradeType upgrade = UpgradeType.Item)
    {

        switch (upgrade)
        {
            case UpgradeType.Item:
                ItemData item = GlobalDataTable.Instance.Item.GetItemDataToID(id);
                GameObject Game = Instantiate(Slots, transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
                break;
            case UpgradeType.Gold:
                GameObject Goldslot = Instantiate(Slots, transform);
                Goldslot.GetComponent<IngiitemSlot>().SetGold(NeedTable[0]);
                break;
            case UpgradeType.Level:
               GameObject LevelSlot = Instantiate(Slots, transform);
                LevelSlot.GetComponent<IngiitemSlot>().SetLevel(NeedTable[-1]);
                break;

        }
    }
    public bool IsIngiBreakOK(Dictionary<int, int> Data)
    {

        foreach(int ID in Data.Keys)
        {
            if (ID != 0 && ID != -1)
            {
                if (ItemManager.Instance.GetSaveData(ID) != null)
                {
                    if (Data.TryGetValue(ID, out int count))
                    {
                        if (count >= ItemManager.Instance.GetSaveData(ID).Value)
                        {
                            return false;
                        }
                    }
                }
            }
            else if(ID == 0)
            {
                if (Data.TryGetValue(0, out int count))
                {
                    if (count >= CurrencyManager.Instance.GetCurrency(CurrencyType.Gold))
                    {
                        return false;
                    }
                }
            }
            else
            {
                if (Data.TryGetValue(-1, out int count))
                {
                    if (count >=  GlobalDataTable.Instance.DataCarrier.GetSave().Level)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }




}
