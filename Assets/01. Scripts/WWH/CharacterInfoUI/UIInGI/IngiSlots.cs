using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            NeedTable[0] = table.NeedGold;
            SlotInstantiate(0, false);
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
    public void SlotInstantiate(int id, bool Isitem = true)
    {
       
     
        if (Isitem)
        {
            ItemData item = GlobalDataTable.Instance.Item.GetItemDataToID(id);
            GameObject Game = Instantiate(Slots, transform);
            Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
        }
        else
        {
            GameObject Game = Instantiate(Slots, transform);
            Game.GetComponent<IngiitemSlot>().SetGold(NeedTable[0]);
        }

    }
    public bool IsIngiBreakOK(Dictionary<int, int> Data)
    {

        foreach(int ID in Data.Keys)
        {
            if (ID != 0)
            {
                if (ItemManager.Instance.GetSaveData(ID) != null)
                {
                    if (Data.TryGetValue(ID, out int count))
                    {
                        if (count > ItemManager.Instance.GetSaveData(ID).Value)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                if (Data.TryGetValue(0, out int count))
                {
                    if (count > CurrencyManager.Instance.GetCurrency(CurrencyType.Gold))
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }




}
