using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageDataTable
{
    public Dictionary<int, List<Package>> PackageDic = new Dictionary<int, List<Package>>();

    public void Initialize()
    {
        List<Package> PackageSO = Resources.LoadAll<Package>("Package").ToList();
        foreach(var data in PackageSO)
        {
            if (!PackageDic.ContainsKey(data.ShopID))
                {
                PackageDic[data.ShopID] = new List<Package>();
            }
            PackageDic[data.ShopID].Add(data);
        }
    }
}
