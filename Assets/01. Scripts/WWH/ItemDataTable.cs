using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDataTable 
{
    public Dictionary<int, ItemData> ItemDic = new Dictionary<int, ItemData>();
    public Dictionary<int, Consume> ConItemDic = new Dictionary<int, Consume>();
    public void Initialize()
    {
        ItemData[] currencySOs = Resources.LoadAll<ItemData>("Item");
        Consume[] ConSO = Resources.LoadAll<Consume>("Cons");
        for (int i = 0; i < currencySOs.Length; i++)
        {
            ItemDic[currencySOs[i].ID] = currencySOs[i];
        }

        for (int i = 0;i < ConSO.Length; i++) 
        {
            ConItemDic[ConSO[i].ID] = ConSO[i];
        }
    }

    public List<ItemData> GetItemList()
    {
        return ItemDic.Values.ToList();
    }

    public List <Consume> GetConsumeList() 
    {
        return ConItemDic.Values.ToList();
    }
    public ItemData GetItemDataToID(int ID) 
    {
        if (ItemDic.ContainsKey(ID))
        {
            return ItemDic[ID];
        }
        return null;
    }
    public Consume GetConsume(int ID) 
    {
        if (ConItemDic.ContainsKey(ID))
        {
            return ConItemDic[ID];
        }
        return null;
    }

    public int GetItemLength()
    {
        return ItemDic.Count;
    }
}
