using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataTable : MonoBehaviour
{
    public Dictionary<int, ItemData> ItemDic = new Dictionary<int, ItemData>();
    public void Initialize()
    {
        ItemData[] currencySOs = Resources.LoadAll<ItemData>("Item");
        for (int i = 0; i < currencySOs.Length; i++)
        {
            ItemDic[currencySOs[i].ID] = currencySOs[i];
        }
    }
    public ItemData GetItemDataToID(int ID) 
    {
        if (ItemDic.ContainsKey(ID))
        {
            return ItemDic[ID];
        }
        return null;
    }

    public int GetLength()
    {
        return ItemDic.Count;
    }
}
