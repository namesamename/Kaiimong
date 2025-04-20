using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataTable : MonoBehaviour
{
    public Dictionary<int, ItemSO> CurrencyDic = new Dictionary<int, ItemSO>();
    public void Initialize()
    {
        ItemSO[] currencySOs = Resources.LoadAll<ItemSO>("Item");
        for (int i = 0; i < currencySOs.Length; i++)
        {
            CurrencyDic[currencySOs[i].ID] = currencySOs[i];
        }
    }
    public ItemSO GetCurrencySOToEnum(int ID) 
    {
        if (CurrencyDic.ContainsKey(ID))
        {
            return CurrencyDic[ID];
        }
        return null;
    }
}
