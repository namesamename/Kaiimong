using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }        // �ٸ� ��ũ��Ʈ���� ���� �����ϱ� ���� �̱���

    [Header("Inventory Panels")]
    [SerializeField] private GameObject itemInventoryPanel;        // ������ �� UI �г�
    [SerializeField] private GameObject consumableInventoryPanel;  // �Ҹ�ǰ �� UI �г�

    [Header("Inventory Info UI")]
    [SerializeField] private TextMeshProUGUI itemCountText;        // ����/�ִ� ���� ǥ�� �ؽ�Ʈ
    [SerializeField] private GameObject inventoryUIRoot;           // �κ��丮�� �ִ� ��� UI (��, ����, ��� ��)���ϳ��� �θ� ������Ʈ

    [Header("Slot Generation")]
    [SerializeField] private GameObject slotPrefab;                // ���� ������ (�������� ������ ���� UI)
    [SerializeField] private Transform itemSlotPanel;              // ������ �� ���� ���� Content ������Ʈ
    [SerializeField] private Transform consumableSlotPanel;        // �Ҹ�ǰ �� ���� ���� Content ������Ʈ

    private List<GameObject> spawnedSlots = new List<GameObject>(); // ������ ���� ������Ʈ���� �����ϴ� ����Ʈ

    private List<ItemData> itemList = new List<ItemData>();        // �Ϲ� ������ ����� ����Ʈ
    private List<ItemData> consumableList = new List<ItemData>();  // �Ҹ�ǰ ������ ����� ����Ʈ

    private InventorySlot selectedSlot;                             // ���� ���õ� ���� (Outline ȿ����)

    private void Awake()
    {
        if (Instance == null) Instance = this;       // �̱��� �ν��Ͻ� �ʱ�ȭ
        else Destroy(gameObject);                    // �ߺ� ����
    }

    private void Start()                            
    {
        OpenItemInventory();                         // ���� �� �⺻���� ������ �� ����
    }


    public void AddItem(ItemData item)
    {
        if (item.Type == EItemType.Consumable)       // �Ҹ�ǰ�� ���
        {
            consumableList.Add(item);                            // �Ҹ�ǰ ����Ʈ �߰�
            SpawnSlots(consumableList, consumableSlotPanel);     // �Ҹ�ǰ ���� ����
            UpdateItemCount(consumableList.Count);               // ���� ǥ�� ����
        }
        else                                          // �Ϲ� �������� ���
        {
            itemList.Add(item);                                  // ������ ����Ʈ �߰�
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
            UpdateItemCount(consumableList.Count);               // ���� ǥ�� ����
        }
        else
        {
            itemList.Remove(item);                              // ������ ����Ʈ ����
            SpawnSlots(itemList, itemSlotPanel);                
            UpdateItemCount(itemList.Count);
        }
    }

    public void OpenItemInventory()
    {
        itemInventoryPanel.SetActive(true);                      // ������ �� Ȱ��ȭ
        consumableInventoryPanel.SetActive(false);               // �Ҹ�ǰ �� ��Ȱ��ȭ
        SpawnSlots(itemList, itemSlotPanel);                     // ������ ���� ����
        UpdateItemCount(itemList.Count);                         // ���� �ؽ�Ʈ ����
    }


    public void OpenConsumableInventory()
    {
        itemInventoryPanel.SetActive(false);                     // ������ �� ��Ȱ��ȭ
        consumableInventoryPanel.SetActive(true);                // �Ҹ�ǰ �� Ȱ��ȭ
        SpawnSlots(consumableList, consumableSlotPanel);         // �Ҹ�ǰ ���� ����
        UpdateItemCount(consumableList.Count);
    }

 
    public void OpenInventory()
    {
        inventoryUIRoot.SetActive(true);                         // �κ��丮 UI ��ü Ȱ��ȭ
        OpenItemInventory();                                     // �⺻ ���� �������κ�
    }


    public void CloseInventory()
    {
        inventoryUIRoot.SetActive(false);                        // �κ��丮 UI ��Ȱ��ȭ
    }


    public void UpdateItemCount(int current)
    {
        int max = 100;                                           // �ִ밪
        itemCountText.text = $"{current} / {max}";              // �ؽ�Ʈ: "���� / �ִ�"
    }


    private void SpawnSlots(List<ItemData> dataList, Transform parentPanel)
    {
        ClearSlots();                                            // ���� ���� ����

        foreach (ItemData item in dataList)
        {
            GameObject slotObj = Instantiate(slotPrefab, parentPanel);   // ���� ������ ���� �� �θ� ����
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.SetSlot(item);                                          // ���Կ� ������ ������ ����
            spawnedSlots.Add(slotObj);                                   // ���� ����Ʈ�� �߰�
        }
    }

    //  ���� ���� ����
    private void ClearSlots()
    {
        foreach (GameObject obj in spawnedSlots)
        {
            Destroy(obj);
        }
        spawnedSlots.Clear();
    }
}