using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngiSlots : MonoBehaviour, ISetPOPUp
{

    GameObject[] Slots;

    public Dictionary<int,int> NeedTabel = new Dictionary<int,int>();

    private void Awake()
    {
        Slots = Resources.LoadAll<GameObject>("ItemSlot");
    }

    public void Initialize()
    {
        int Rec = ImsiGameManager.Instance.GetCharacterSaveData().Recognition;
        int Id = ImsiGameManager.Instance.GetCharacterSaveData().ID;



        //UpgradeData updata=  GlobalDatabase.instance.Upgrade.GetChracterIDAndRecognition(Id, Rec);
        //if(updata.ItemCount >= 3)
        //{
        //    NeedTable[0] = updata.NeedGold;
        //    SlotInstantiate(0, false)
        //    NeedTabel[updata.fristItemId] = updata.firstItemCount;
        //    SlotInstantiate(updata.fristItemId)
        //    NeedTabel[updata.SecondItemId] = updata.SecondItemCount;
        //    SlotInstantiate(updata.SecondItemId)
        //}
        //if(updata.ItemCount >= 4)
        //{
        //    NeedTabel[updata.ThridItemId] = updata.ThridItemCount;
        //    SlotInstantiate(updata.ThridItemId)
        //}
        //if(updata.ItemCount >= 5)
        //{
        //    NeedTabel[updata.FourthItemId] = updata.FourthItemCount;
        //    SlotInstantiate(updata.FourthItemId)
        //}
    }


    public void SlotInstantiate(int id, bool Isitem = true)
    {
        if (Isitem)
        {
            ItemData item = GlobalDataTable.Instance.Item.GetItemDataToID(id);
            if (item.Grade == ERarity.S)
            {
                GameObject Game = Instantiate(Slots[4], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTabel[id]);
            }
            else if (item.Grade == ERarity.A)
            {
                GameObject Game = Instantiate(Slots[0], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTabel[id]);
            }
            else if (item.Grade == ERarity.B)
            {
                GameObject Game = Instantiate(Slots[1], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTabel[id]);
            }
            else if (item.Grade == ERarity.C)
            {
                GameObject Game = Instantiate(Slots[2], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTabel[id]);
            }
            else if (item.Grade == ERarity.D)
            {
                GameObject Game = Instantiate(Slots[3], transform);
                Game.GetComponent<IngiitemSlot>().SetSlot(item, NeedTabel[id]);
            }
        }
        else
        {
            GameObject Game = Instantiate(Slots[4], transform);
            Game.GetComponent<IngiitemSlot>().SetGold(NeedTabel[0]);
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
