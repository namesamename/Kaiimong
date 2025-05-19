using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDataTable
{
    public Dictionary<int,Shop> ShopDic = new Dictionary<int, Shop>();

    public void Initialize()
    {
        Shop[]ShopSO = Resources.LoadAll<Shop>("Shop");
        for(int i=0; i <ShopSO.Length;i++)
        {
            ShopDic[ShopSO[i].ID] = ShopSO[i];
        }
    }
}
