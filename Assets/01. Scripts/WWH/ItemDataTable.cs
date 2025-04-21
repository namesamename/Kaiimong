using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataTable : MonoBehaviour
{
    public Dictionary<int, ItemData> CurrencyDic = new Dictionary<int, ItemData>();
    public void Initialize()
    {
        ItemData[] currencySOs = Resources.LoadAll<ItemData>("Item");
        for (int i = 0; i < currencySOs.Length; i++)
        {
            CurrencyDic[currencySOs[i].ID] = currencySOs[i];
        }
    }
    public ItemData GetCurrencySOToEnum(int ID) 
    {
        if (CurrencyDic.ContainsKey(ID))
        {
            return CurrencyDic[ID];
        }
        return null;
    }
}
