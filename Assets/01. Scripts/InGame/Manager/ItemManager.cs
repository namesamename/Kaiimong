using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public Dictionary<int, ItemSavaData> ItemDatasSaveDic = new Dictionary<int, ItemSavaData>();
    public ItemSavaData GetItemSaveData(int itemID)
    {
        if (ItemDatasSaveDic.ContainsKey(itemID))
        {
            return ItemDatasSaveDic[itemID];
        }
        return null;
    }

    public void SaveAllData()
    {
        foreach (var itemData in ItemDatasSaveDic.Values)
        {
            SaveDataBase.Instance.SaveSingleData(itemData);
        }
    }

    public void SaveSingleData(int itemID)
    {
        SaveDataBase.Instance.SaveSingleData(ItemDatasSaveDic[itemID]);
    }

    //public void Initialize()
    //{   
    //    foreach(ItemData item in GlobalDataTable.Instance.Item.ItemDic.Values)
    //    {
    //        HaveData(item, itemDatas);
    //    }

    //}
    //public void HaveData(ItemData item, List<ItemSavaData> CurSaveList)
    //{
    //    var itemDatas = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);

    //    var FindData = itemDatas.FindIndex(x => x.ID == item.ID);
    //    if(FindData != -1) 
    //    {
    //        CurSaveList.Add(itemDatas[FindData]);
    //    }
    //    else
    //    {
    //        ItemSavaData itemSava = new ItemSavaData()
    //        { 
    //            ID = item.ID,
    //            Value = 0,
    //        };
    //        CurSaveList.Add(itemSava);
    //    }
    //}

    public ItemSavaData GetSaveData(int ID)
    {
        return ItemDatasSaveDic[ID];
    }

    public List<ItemSavaData> GetSaveList()
    {
        List<ItemSavaData> list = new List<ItemSavaData>();
        foreach (ItemSavaData item in ItemDatasSaveDic.Values)
        {
            if (item.Value > 0)
            {
                list.Add(item);
            }
        }

        return list;

    }

    public void SetitemCount(int ID, int Amount)
    {
        var item = ItemDatasSaveDic[ID];
        item.Value += Amount;
        SaveSingleData(ID);
    }

    public void Initialize()
    {
        // 모든 아이템 초기화
        for (int i = 1; i < GlobalDataTable.Instance.Item.ItemDic.Count + 1; i++)
        {
            int itemID = GlobalDataTable.Instance.Item.ItemDic[i].ID;
            ItemSavaData newItemData = new ItemSavaData();
            newItemData.ID = itemID;

            // 저장된 데이터가 있는지 확인
            var foundData = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);
            if (foundData != null && foundData.Find(x => x.ID == itemID) != null)
            {
                // 저장된 데이터 로드
                var savedData = foundData.Find(x => x.ID == itemID);
                newItemData.Value = savedData.Value;
                newItemData.Savetype = SaveType.Item;

            }
            else
            {
                // 새 데이터 생성
                newItemData.Value = 0;
                newItemData.Savetype = SaveType.Item;
                SaveDataBase.Instance.SaveSingleData(newItemData);
                Debug.Log("item data NO");
            }

            // 사전에 저장
            ItemDatasSaveDic[itemID] = newItemData;
        }
    }

    //public void Save()
    //{
    //    foreach(var item in itemDatas) 
    //    {
    //        SaveDataBase.Instance.SaveSingleData(item);
    //    }
    //}
}
