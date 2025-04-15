using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance { get; private set; }          // �̱��� ���ٿ�

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



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // �ߺ� ����
            return;
        }

        Instance = this;
    }

    private void Start()                            
    {
        // �׽�Ʈ�� ������ ����
        ItemData testItemS = ScriptableObject.CreateInstance<ItemData>();
        testItemS.ItemName = "���� ������";
        testItemS.Rarity = ERarity.S;
        testItemS.Type = EItemType.Item;

        ItemData testItemA = ScriptableObject.CreateInstance<ItemData>();
        testItemA.ItemName = "��û ���� ������";
        testItemA.Rarity = ERarity.A;
        testItemA.Type = EItemType.Item;

        ItemData testItemB = ScriptableObject.CreateInstance<ItemData>();
        testItemB.ItemName = "���� ������";
        testItemB.Rarity = ERarity.B;
        testItemB.Type = EItemType.Item;

        ItemData testItemC = ScriptableObject.CreateInstance<ItemData>();
        testItemC.ItemName = "���� ���� ������";
        testItemC.Rarity = ERarity.C;
        testItemC.Type = EItemType.Item;

        ItemData testItemD = ScriptableObject.CreateInstance<ItemData>();
        testItemD.ItemName = "�׳� ������";
        testItemD.Rarity = ERarity.D;
        testItemD.Type = EItemType.Item;

        // �κ��丮�� �߰�
        AddItem(testItemS);
        AddItem(testItemA);
        AddItem(testItemB);
        AddItem(testItemC);
        AddItem(testItemD);

        OpenItemInventory();                         // ���� �� �⺻���� ������ �� ����
    }

    private List<ItemData> SortByRarity(List<ItemData> list)           // ��͵� �� ���� (S �� D ��)
    {
        return list.OrderBy(item => item.Rarity).ToList();
    }

    public void AddItem(ItemData item)
    {
        Debug.Log($"[AddItem ȣ���] {item.ItemName} / Ÿ��: {item.Type}");

        if (item.Type == EItemType.Consumable)       // �Ҹ�ǰ�� ���
        {
            consumableList.Add(item);                            // �Ҹ�ǰ ����Ʈ �߰�
            consumableList = SortByRarity(consumableList);       // �Ҹ�ǰ ȸ�͵� ����
            SpawnSlots(consumableList, consumableSlotPanel);     // �Ҹ�ǰ ���� ����
            UpdateItemCount(consumableList.Count);               // ���� ǥ�� ����
        }
        else                                          // �Ϲ� �������� ���
        {
            itemList.Add(item);                                  // ������ ����Ʈ �߰�
            itemList = SortByRarity(itemList);                   // ������ ȸ�͵� ����
            SpawnSlots(itemList, itemSlotPanel);                 // ������ ���� ����
            UpdateItemCount(itemList.Count);                     // ���� ǥ�� ����
        }
    }


    public void RemoveItem(ItemData item)
    {
        if (item.Type == EItemType.Consumable)
        {
            consumableList.Remove(item);                         // �Ҹ�ǰ ����Ʈ ����
            SpawnSlots(consumableList, consumableSlotPanel);     // ���� �����
            UpdateItemCount(consumableList.Count);               // �Ҹ�ǰ ���� ǥ�� ����
        }
        else
        {
            itemList.Remove(item);                              // ������ ����Ʈ ����
            SpawnSlots(itemList, itemSlotPanel);                // ���� �����
            UpdateItemCount(itemList.Count);                    // ������ ���� ǥ�� ����
        }
    }

    public void OpenItemInventory()
    {

        itemInventoryButton.SetActive(true);                      // ������ �� Ȱ��ȭ
        consumableInventoryButton.SetActive(true);               

        itemScrollView.SetActive(true);                           // ������ ��ũ�� �� �ѱ�
        consumableScrollView.SetActive(false);                    // �Ҹ�ǰ ��ũ�� �� ����

        itemList = SortByRarity(itemList);                        // ������ ����Ʈ ����
        SpawnSlots(itemList, itemSlotPanel);                      // ������ ���� ����
        UpdateItemCount(itemList.Count);                          // ���� �ؽ�Ʈ ����
    }



    public void OpenConsumableInventory()
    {
        itemInventoryButton.SetActive(true);                     
        consumableInventoryButton.SetActive(true);                // �Ҹ�ǰ �� Ȱ��ȭ

        itemScrollView.SetActive(false);                         // ������ ��ũ�� �� ����
        consumableScrollView.SetActive(true);                    // ������ ��ũ�� �� �ѱ�

        consumableList = SortByRarity(consumableList);           // �Ҹ�ǰ ����Ʈ ����
        SpawnSlots(consumableList, consumableSlotPanel);         // �Ҹ�ǰ ���� ����
        UpdateItemCount(consumableList.Count);
    }   
    public void UpdateItemCount(int current)
    {
        int max = 100;                                           // �ִ밪
        itemslotCountText.text = $"{current} / {max}";           // �ؽ�Ʈ: "���� / �ִ�"
    }


    private void SpawnSlots(List<ItemData> dataList, Transform parentPanel)     // ���Ե��� �����ϰ� ������ �����͸� ����
    {
        ClearSlots();                                                           // ���� ���� ����

        List<ItemData> sortedList = SortByRarity(dataList);                     // ���ĵ� ����Ʈ ���
        Debug.Log($"[���� ����] �� {sortedList.Count}���� ���� ���� ����");

        foreach (ItemData item in sortedList)                                     // ���޹��� ������ ����Ʈ�� ��ȸ

        {
            GameObject prefab = GetSlotPrefabByRarity(item.Rarity);      // ��͵��� �´� ���� ������ ����
            GameObject slotObj = Instantiate(prefab, parentPanel);       // ���� ������ ���� �� �θ� ����

            Debug.Log($"[���� ����] {item.ItemName} / {item.Rarity}");

            InventorySlot slot = slotObj.GetComponent<InventorySlot>();  // ������ ���� ������Ʈ���� InventorySlot ������Ʈ�� ������
            slot.SetSlot(item);                                          // ���Կ� ������ ������ ����
            spawnedSlots.Add(slotObj);                                   // ���� ����Ʈ�� �߰�
        }
    }

    private void ClearSlots()                                           // �κ��丮 ���� �ʱ�ȭ
    {
        foreach (GameObject obj in spawnedSlots)                        // ������ ��� ���� ������Ʈ�� �ϳ��� ��ȸ�ϸ� �ı�
        {
            Destroy(obj);                                               // ���� ������Ʈ ����
        }
        Debug.Log($"[���� �ʱ�ȭ] ���� ���� {spawnedSlots.Count}�� ����");
        spawnedSlots.Clear();            
        
    }

    private GameObject GetSlotPrefabByRarity(ERarity rarity)           // ��͵��� ���� ���� ������ ��ȯ
    {
        int index = EnumToIndex(rarity);                                      // enum�� ����(int) �ε����� ���


        if (index < 0 || index >= slotPrefabs.Length)
        {
            Debug.LogWarning($"[���] slotPrefabs �迭�� '{rarity}'�� �ش��ϴ� ���� �������� �����ϴ�. �⺻ ������(S)�� ����մϴ�.");
            return slotPrefabs[0]; 
        }

        if (slotPrefabs[index] == null)
        {
            Debug.LogWarning($"[���] ��͵� {rarity} ���� �������� null�Դϴ�. �⺻ ������(S)�� ����մϴ�.");
            return slotPrefabs[0];
        }

        return slotPrefabs[index];                                     // ���� ������ �ش� ������ ��ȯ
    }

    private int EnumToIndex(ERarity rarity)                         // ERarity �� int �ε����� �����ϰ� ��ȯ
    {
        return (int)rarity;
    }
}