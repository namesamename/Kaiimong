using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngiSlots : MonoBehaviour, ISetPOPUp
{

    GameObject[] Slots;

    public Dictionary<int,int> NeedTable = new Dictionary<int,int>();

    private void Awake()
    {
        Slots = Resources.LoadAll<GameObject>("ItemSlot");
    }

    public void Initialize()
    {
        int Rec = ImsiGameManager.Instance.GetCharacterSaveData().Recognition;
        int Id = ImsiGameManager.Instance.GetCharacterSaveData().ID;
        List<CharacterUpgradeTable> list = GlobalDataTable.Instance.Upgrade.GetRecoList(ImsiGameManager.Instance.GetCharacterSaveData().ID);
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


    public void SlotInstantiate(int id, bool Isitem = true)
    {
        if (Isitem)
        {
            ItemData item = GlobalDataTable.Instance.Item.GetItemDataToID(id);
            if (item.Grade == ERarity.S)
            {
                GameObject Game = Instantiate(Slots[4], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
            }
            else if (item.Grade == ERarity.A)
            {
                GameObject Game = Instantiate(Slots[0], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
            }
            else if (item.Grade == ERarity.B)
            {
                GameObject Game = Instantiate(Slots[1], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
            }
            else if (item.Grade == ERarity.C)
            {
                GameObject Game = Instantiate(Slots[2], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
            }
            else if (item.Grade == ERarity.D)
            {
                GameObject Game = Instantiate(Slots[3], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTable[id]);
            }
        }
        else
        {
            GameObject Game = Instantiate(Slots[4], transform);
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
