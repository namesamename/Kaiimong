using System.Collections.Generic;
using UnityEngine;

public class ShopUIManager : MonoBehaviour
{
    [Header("데이터")]
    public List<PackageGroup> packageGroups;
    public List<Shop> shopData;

    [Header("UI")]
    public Transform contentParent; // ScrollView > Content
    public GameObject packageGroupItemPrefab; // 연결한 PackageGroupItemUI 프리팹

    private void Start()
    {
        LoadPackages();
    }

    private void LoadPackages()
    {
        foreach (var group in packageGroups)
        {
            Shop shop = shopData.Find(s => s.name == $"Shop_{group.ShopID}");

            if (shop == null)
            {
                Debug.LogWarning($"Shop 데이터 없음: ShopID {group.ShopID}");
                continue;
            }

            GameObject go = Instantiate(packageGroupItemPrefab, contentParent);
            var ui = go.GetComponent<PackageGroupItemUI>();
            ui.Setup(group, shop);
        }
    }
}
