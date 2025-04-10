using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance { get; private set; }        // �ٸ� ��ũ��Ʈ���� ���� �����ϱ� ���� �̱���

    [Header("Inventory Panels")]
    [SerializeField] private GameObject itemInventoryButton;        // ������ �� UI �г�
    [SerializeField] private GameObject consumableInventoryButton;  // �Ҹ�ǰ �� UI �г�

    [Header("Inventory Info UI")]
    [SerializeField] private TextMeshProUGUI itemslotCountText;        // ������ ���� ����/�ִ� ���� ǥ�� �ؽ�Ʈ

    [Header("Slot Generation")]
    [SerializeField] private GameObject slotPrefab;                // ���� ������ (�������� ������ ���� UI)
    [SerializeField] private Transform itemSlotPanel;              // ������ �� ���� ���� Content ������Ʈ
    [SerializeField] private Transform consumableSlotPanel;        // �Ҹ�ǰ �� ���� ���� Content ������Ʈ

    [Header("Scroll Views")]
    [SerializeField] private GameObject itemScrollView;            // ������ ��ũ�� �� ������Ʈ
    [SerializeField] private GameObject consumableScrollView;      // �Ҹ�ǰ ��ũ�� �� ������Ʈ

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
        itemInventoryButton.SetActive(true);                      // ������ �� Ȱ��ȭ
        consumableInventoryButton.SetActive(true);               

        itemScrollView.SetActive(true);                           // ������ ��ũ�� �� �ѱ�
        consumableScrollView.SetActive(false);                    // �Ҹ�ǰ ��ũ�� �� ����

        SpawnSlots(itemList, itemSlotPanel);                      // ������ ���� ����
        UpdateItemCount(itemList.Count);                          // ���� �ؽ�Ʈ ����
    }


    public void OpenConsumableInventory()
    {
        itemInventoryButton.SetActive(true);                     
        consumableInventoryButton.SetActive(true);                // �Ҹ�ǰ �� Ȱ��ȭ

        itemScrollView.SetActive(false);                         // ������ ��ũ�� �� ����
        consumableScrollView.SetActive(true);                    // ������ ��ũ�� �� �ѱ�

        SpawnSlots(consumableList, consumableSlotPanel);         // �Ҹ�ǰ ���� ����
        UpdateItemCount(consumableList.Count);
    }   
    public void UpdateItemCount(int current)
    {
        int max = 100;                                           // �ִ밪
        itemslotCountText.text = $"{current} / {max}";           // �ؽ�Ʈ: "���� / �ִ�"
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

    private void ClearSlots()
    {
        foreach (GameObject obj in spawnedSlots)
        {
            Destroy(obj);
        }
        spawnedSlots.Clear();
    }
}