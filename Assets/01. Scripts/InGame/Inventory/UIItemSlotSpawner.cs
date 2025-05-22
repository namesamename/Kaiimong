
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIItemSlotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private Transform slotParent;
    private List<GameObject> spawnedSlots = new List<GameObject>();

        void Start()
    {
        ShowOwnedItems();
    }

    // 소유 중인 아이템만 슬롯으로 생성
    public void ShowOwnedItems()
    {
        // SO에서 모든 아이템 정보 로드
        ItemData[] allItems = Resources.LoadAll<ItemData>("Item");

        // 저장된 인벤토리 데이터 가져오기
        List<ItemSavaData> ownedItems = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);

        // (ItemData, ItemSaveData) 매칭하여 슬롯 생성
        ClearSlots();

        foreach (var itemSave in ownedItems)
        {
            ItemData itemSO = allItems.FirstOrDefault(item => item.ID == itemSave.ID);
            if (itemSO != null && itemSave.Value > 0)  // 수량 0 이상만
            {
                GameObject slotObj = Instantiate(slotPrefab, slotParent);
                InventorySlot slot = slotObj.GetComponent<InventorySlot>();
                slot.SetSlot(itemSO, itemSave.Value);
                spawnedSlots.Add(slotObj);
            }
        }
    }

    private void ClearSlots()
    {
        foreach (var obj in spawnedSlots) Destroy(obj);
        spawnedSlots.Clear();
    }
}
