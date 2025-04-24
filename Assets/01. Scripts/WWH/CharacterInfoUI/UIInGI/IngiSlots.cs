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
        //    NeedTabel[updata.fristItemId] = updata.firstItemCount;
        //    SlotInstantiate(updata.fristItemId)
        //    NeedTabel[updata.SecondItemId] = updata.SecondItemCount;
        //    SlotInstantiate(updata.SecondItemId)
        //    NeedTabel[updata.ThridItemId] = updata.ThridItemCount;
        //    SlotInstantiate(updata.ThridItemId)
        //}
        //if(updata.ItemCount >= 4)
        //{
        //    NeedTabel[updata.FourthItemId] = updata.FourthItemCount;
        //    SlotInstantiate(updata.FourthItemId)
        //}
        //if(updata.ItemCount >= 5)
        //{
        //    NeedTabel[updata.FifththItemId] = updata.FixthItemCount;
        //    SlotInstantiate(updata.FifththItemId)
        //}
    }


    public void SlotInstantiate(int id)
    {
        ItemData item = GlobalDataTable.Instance.Item.GetItemDataToID(id);

        if(item.Grade == ERarity.S)
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
    public bool IsIngiBreakOK(Dictionary<int, int> Data)
    {

        foreach(int ID in Data.Keys)
        {
            if(ItemManager.Instance.GetSaveData(ID) != null)
            {
                if(Data.TryGetValue(ID, out int count))
                {
                    if(count > ItemManager.Instance.GetSaveData(ID).Value) 
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }




}
