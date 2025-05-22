
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

    // ���� ���� �����۸� �������� ����
    public void ShowOwnedItems()
    {
        // SO���� ��� ������ ���� �ε�
        ItemData[] allItems = Resources.LoadAll<ItemData>("Item");

        // ����� �κ��丮 ������ ��������
        List<ItemSavaData> ownedItems = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);

        // (ItemData, ItemSaveData) ��Ī�Ͽ� ���� ����
        ClearSlots();

        foreach (var itemSave in ownedItems)
        {
            ItemData itemSO = allItems.FirstOrDefault(item => item.ID == itemSave.ID);
            if (itemSO != null && itemSave.Value > 0)  // ���� 0 �̻�
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
