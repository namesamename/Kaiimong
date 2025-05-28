using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PackageGroup", menuName = "Shop/PackageGroup")]
public class PackageGroup : ScriptableObject
{
    public int ShopID;
    public List<Package> Packages;
}