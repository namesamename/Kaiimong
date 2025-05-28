using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance { get; private set; }          // �̱��� ���ٿ�


    public GameObject SlotPrefab;


    [Header("Inventory Panels")]
    [SerializeField] private GameObject itemInventoryButton;        // ������ �� UI �г�
    [SerializeField] private GameObject consumableInventoryButton;  // �Ҹ�ǰ �� UI �г�

    [Header("Inventory Info UI")]
    [SerializeField] private TextMeshProUGUI itemslotCountText;     // ������ ���� ����/�ִ� ���� ǥ�� �ؽ�Ʈ

    [Header("Slot Generation")]
    [SerializeField] private GameObject[] slotPrefabs;             // ȸ�͵� ���� ������ �迭
    [SerializeField] private Transform itemSlotPanel;              // ������ �� ���� ���� Content ������Ʈ
    [SerializeField] private Transform consumableSlotPanel;        // �Ҹ�ǰ �� ���� ���� Content ������Ʈ

    [Header("Scroll Views")]
    [SerializeField] private GameObject itemScrollView;            // ������ ��ũ�� �� ������Ʈ
    [SerializeField] private GameObject consumableScrollView;      // �Ҹ�ǰ ��ũ�� �� ������Ʈ

    private List<GameObject> spawnedSlots = new List<GameObject>(); // ������ ���� ������Ʈ���� �����ϴ� ����Ʈ

    private List<ItemData> itemList = new List<ItemData>();        // �Ϲ� ������ ����� ����Ʈ
    private List<ItemData> consumableList = new List<ItemData>();  // �Ҹ�ǰ ������ ����� ����Ʈ

    [SerializeField] private List<ItemData> defaultItems;         // �Ϲ� ������ ���
    [SerializeField] private List<ItemData> defaultConsumables;   // �Ҹ�ǰ ���


    private Button[] button;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // �ߺ� ����
            return;
        }
        button = GetComponentsInChildren<Button>();
        Instance = this;
        SlotPrefab = Resources.Load<GameObject>("ItemSlot/Slot");


    }

    private void Start()
    {
        OpenItemInventory();                         // ���� �� �⺻���� ������ �� ����

        button[0].onClick.AddListener(() => SceneLoader.Instance.ChanagePreScene());
        button[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
    }

    private List<ItemData> SortByRarity()           // ��͵� �� ���� (S �� D ��)
    {
        List<ItemSavaData> HaveItem = ItemManager.Instance.GetSaveList();
        if (HaveItem != null)
        {
            List<ItemData> List = new List<ItemData>();
            foreach (ItemSavaData item in HaveItem)
            {
        
                var itemData = GlobalDataTable.Instance.Item.GetItemDataToID(item.ID);
                List.Add(itemData);
            }

            return List.OrderBy(item => item.Grade).ToList();
        }

        return new List<ItemData>();
    }

    private List<ItemData> ComsumableSortByRarity()           // ��͵� �� ���� (S �� D ��)
    {

        List<ItemSavaData> HaveItem = ItemManager.Instance.GetSaveList();
        List<ItemData > ConsumList = new List<ItemData>();
        if (HaveItem != null)
        {
            List<ItemData> List = new List<ItemData>();
            foreach (ItemSavaData item in HaveItem)
            {

                var itemData = GlobalDataTable.Instance.Item.GetItemDataToID(item.ID);
                List.Add(itemData);
            }

            List<ItemData> GradeSortList =  List.OrderBy(item => item.Grade).ToList();

            foreach (ItemData item in GradeSortList) 
            {
                if(item.ItemType == EItemType.Consume)
                {
                    ConsumList.Add(item);
                }
            }

            return ConsumList;
        }
        else
        {
            return new List<ItemData>();
        }

  
    }

    public void AddItem(int itemId, int count = 1)
    {
        var existingItem = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item).FirstOrDefault(item => item.ID == itemId);
        var itemData = GlobalDataTable.Instance.Item.GetItemDataToID(itemId);

        if (existingItem != null && existingItem.Value > 1)
        {

            int newCount = existingItem.Value + count;
            if (newCount <= itemData.MaxStackCount)
            {
        
                existingItem.Value += newCount;
            }
            else
            {
 
                var remainingCount = newCount - itemData.MaxStackCount;
                existingItem.Value = itemData.MaxStackCount;

                // ���� �������� �� ������ ����
                AddItem(itemId, remainingCount);
            }
        }
        else
        {

            var newItem = new ItemSavaData { ID = itemId, Value = count };
            SaveDataBase.Instance.SaveSingleData(newItem);
        }

        if (itemData.ItemType == EItemType.Consume)       // �Ҹ�ǰ�� ���
        {
            consumableList.Add(itemData);                            // �Ҹ�ǰ ����Ʈ �߰�
            consumableList = SortByRarity();       // �Ҹ�ǰ ȸ�͵� ����
            SpawnSlots(consumableSlotPanel, consumableList);     // �Ҹ�ǰ ���� ����
            UpdateItemCount(consumableList.Count);               // ���� ǥ�� ����
        }
        else                                          // �Ϲ� �������� ���
        {
            itemList.Add(itemData);                                  // ������ ����Ʈ �߰�
            itemList = SortByRarity();                   // ������ ȸ�͵� ����
            SpawnSlots(itemSlotPanel, itemList);                 // ������ ���� ����
            UpdateItemCount(itemList.Count);                     // ���� ǥ�� ����
        }
    }


    public void RemoveItem(ItemData item)
    {
        if (item.ItemType == EItemType.Consume)
        {
            consumableList.Remove(item);                         // �Ҹ�ǰ ����Ʈ ����
            SpawnSlots(consumableSlotPanel, consumableList);     // ���� �����
            UpdateItemCount(consumableList.Count);               // �Ҹ�ǰ ���� ǥ�� ����
        }
        else
        {
            itemList.Remove(item);                              // ������ ����Ʈ ����
            SpawnSlots(itemSlotPanel, itemList);                // ���� �����
            UpdateItemCount(itemList.Count);                    // ������ ���� ǥ�� ����
        }
    }

    public void OpenItemInventory()
    {

        itemInventoryButton.SetActive(true);                      // ������ �� Ȱ��ȭ
        consumableInventoryButton.SetActive(true);

        itemScrollView.SetActive(true);                           // ������ ��ũ�� �� �ѱ�
        consumableScrollView.SetActive(false);                    // �Ҹ�ǰ ��ũ�� �� ����

        itemList = SortByRarity();                        // ������ ����Ʈ ����
        SpawnSlots(itemSlotPanel, itemList);                      // ������ ���� ����
        UpdateItemCount(itemList.Count);                          // ���� �ؽ�Ʈ ����
    }

    public void OpenConsumableInventory()
    {
        itemInventoryButton.SetActive(true);
        consumableInventoryButton.SetActive(true);                // �Ҹ�ǰ �� Ȱ��ȭ

        itemScrollView.SetActive(false);                         // ������ ��ũ�� �� ����
        consumableScrollView.SetActive(true);                    // ������ ��ũ�� �� �ѱ�

        consumableList = ComsumableSortByRarity();           // �Ҹ�ǰ ����Ʈ ����
        SpawnSlots(consumableSlotPanel, consumableList);         // �Ҹ�ǰ ���� ����
        UpdateItemCount(consumableList.Count);


    }
    public void UpdateItemCount(int current)
    {
        itemslotCountText.text = $"{current} / {100}";           // �ؽ�Ʈ: "���� / �ִ�"
    }


    private void SpawnSlots(Transform parentPanel, List<ItemData> items)     // ���Ե��� �����ϰ� ������ �����͸� ����
    {

        ClearSlots();

        //Debug.Log("Summon");

        //Dictionary<int, int> itemIDAndValue = new Dictionary<int, int>();
        //foreach (var saveData in ItemManager.Instance.GetSaveList())
        //{
        //    Debug.Log(saveData);
        //    if (itemIDAndValue.ContainsKey(saveData.ID))
        //        itemIDAndValue[saveData.ID] += saveData.Value;
        //    else
        //        itemIDAndValue[saveData.ID] = saveData.Value;
        //}
        //List<(ItemData itemData, int count)> itemsAndValue = new List<(ItemData, int)>();
        //foreach (var kvp in itemIDAndValue)
        //{
        //    Debug.Log(kvp);
        //    var itemData = GlobalDataTable.Instance.Item.GetItemDataToID(kvp.Key);
        //    itemsAndValue.Add((itemData, kvp.Value));
        //}


        //itemsAndValue = itemsAndValue.OrderBy(item => item.itemData.Grade).ToList();

        foreach (var Item  in items)
        {
            GameObject slotObj = Instantiate(SlotPrefab, parentPanel);

            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.SetSlot(Item, ItemManager.Instance.GetItemSaveData(Item.ID).Value);  // �����۰� ���� ����
            spawnedSlots.Add(slotObj);
        }

        //ClearSlots();                                                           // ���� ���� ����
        //List<ItemData> sortedList = SortByRarity();                     // ���ĵ� ����Ʈ ���
        //Debug.Log($"[���� ����] �� {sortedList.Count}���� ���� ���� ����");

        //foreach (ItemData item in sortedList)                                     // ���޹��� ������ ����Ʈ�� ��ȸ
        //{
        //    GameObject prefab = GetSlotPrefabByRarity(item.Grade);      // ��͵��� �´� ���� ������ ����
        //    GameObject slotObj = Instantiate(prefab, parentPanel);       // ���� ������ ���� �� �θ� ����

        //    Debug.Log($"[���� ����] {item.Name} / {item.Grade}");

        //    InventorySlot slot = slotObj.GetComponent<InventorySlot>();  // ������ ���� ������Ʈ���� InventorySlot ������Ʈ�� ������
        //    slot.SetSlot(item);                                          // ���Կ� ������ ������ ����
        //    spawnedSlots.Add(slotObj);                                   // ���� ����Ʈ�� �߰�
        //}
    }

    private void ClearSlots()                                           // �κ��丮 ���� �ʱ�ȭ
    {
        foreach (GameObject obj in spawnedSlots)                        // ������ ��� ���� ������Ʈ�� �ϳ��� ��ȸ�ϸ� �ı�
        {
            Destroy(obj);                                               // ���� ������Ʈ ����
        }
        spawnedSlots.Clear();

    }

    //private GameObject GetSlotPrefabByRarity(ERarity rarity)           // ��͵��� ���� ���� ������ ��ȯ
    //{
    //    int index = EnumToIndex(rarity);                                      // enum�� ����(int) �ε����� ���
    //    if (index < 0 || index >= slotPrefabs.Length)
    //    {
    //        Debug.LogWarning($"[���] slotPrefabs �迭�� '{rarity}'�� �ش��ϴ� ���� �������� �����ϴ�. �⺻ ������(S)�� ����մϴ�.");
    //        return slotPrefabs[0];
    //    }
    //    if (slotPrefabs[index] == null)
    //    {
    //        Debug.LogWarning($"[���] ��͵� {rarity} ���� �������� null�Դϴ�. �⺻ ������(S)�� ����մϴ�.");
    //        return slotPrefabs[0];
    //    }
    //    return slotPrefabs[index];                                     // ���� ������ �ش� ������ ��ȯ
    //}

    //private int EnumToIndex(ERarity rarity)                         // ERarity �� int �ε����� �����ϰ� ��ȯ
    //{
    //    return (int)rarity;
    //}


}