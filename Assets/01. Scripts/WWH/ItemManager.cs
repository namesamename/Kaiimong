using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>, ISavable
{
    public List<ItemSavaData>  itemDatas = new List<ItemSavaData>();


    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {   
        foreach(ItemData item in GlobalDataTable.Instance.Item.ItemDic.Values)
        {
            HaveData(item, itemDatas);
        }
     
    }
    public void HaveData(ItemData item, List<ItemSavaData> CurSaveList)
    {
        var itemDatas = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);

        var FindData = itemDatas.FindIndex(x => x.ID == item.ID);
        if(FindData != -1) 
        {
            CurSaveList.Add(itemDatas[FindData]);
        }
        else
        {
            ItemSavaData itemSava = new ItemSavaData()
            { 
                ID = item.ID,
                Value = 0,
            };
            CurSaveList.Add(itemSava);
        }
    }

    public ItemSavaData GetSaveData(int ID)
    {
        return itemDatas.Find(x => x.ID == ID);
    }

    public List<ItemSavaData> GetSaveList()
    {
        List<ItemSavaData> list = new List<ItemSavaData>();
        foreach(ItemSavaData item in itemDatas)
        {
            if(item.Value > 0)
            {
                list.Add(item);
            }
        }
        
        return list;

    }

    public void SetitemCount(int ID, int Amount)
    {
        var item = itemDatas.Find(x => x.ID == ID);
        item.Value += Amount;
    }

    public void Save()
    {
        foreach(var item in itemDatas) 
        {
            SaveDataBase.Instance.SaveSingleData(item);
        }
    }
}
