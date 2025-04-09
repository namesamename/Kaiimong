using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }        // 다른 스크립트에서 쉽게 접근하기 위한 싱글톤

    [Header("Inventory Panels")]
    [SerializeField] private GameObject itemInventoryPanel;        // 아이템 탭 UI 패널
    [SerializeField] private GameObject consumableInventoryPanel;  // 소모품 탭 UI 패널

    [Header("Inventory Info UI")]
    [SerializeField] private TextMeshProUGUI itemCountText;        // 현재/최대 개수 표시 텍스트
    [SerializeField] private GameObject inventoryUIRoot;           // 인벤토리에 있는 모든 UI (탭, 슬롯, 배경 등)을하나의 부모 오브젝트

    [Header("Slot Generation")]
    [SerializeField] private GameObject slotPrefab;                // 슬롯 프리팹 (동적으로 생성될 슬롯 UI)
    [SerializeField] private Transform itemSlotPanel;              // 아이템 탭 안의 슬롯 Content 오브젝트
    [SerializeField] private Transform consumableSlotPanel;        // 소모품 탭 안의 슬롯 Content 오브젝트

    private List<GameObject> spawnedSlots = new List<GameObject>(); // 생성된 슬롯 오브젝트들을 추적하는 리스트

    private List<ItemData> itemList = new List<ItemData>();        // 일반 아이템 저장용 리스트
    private List<ItemData> consumableList = new List<ItemData>();  // 소모품 아이템 저장용 리스트

    private InventorySlot selectedSlot;                             // 현재 선택된 슬롯 (Outline 효과용)

    private void Awake()
    {
        if (Instance == null) Instance = this;       // 싱글톤 인스턴스 초기화
        else Destroy(gameObject);                    // 중복 방지
    }

    private void Start()                            
    {
        OpenItemInventory();                         // 시작 시 기본으로 아이템 탭 열기
    }


    public void AddItem(ItemData item)
    {
        if (item.Type == EItemType.Consumable)       // 소모품일 경우
        {
            consumableList.Add(item);                            // 소모품 리스트 추가
            SpawnSlots(consumableList, consumableSlotPanel);     // 소모품 슬롯 생성
            UpdateItemCount(consumableList.Count);               // 수량 표시 갱신
        }
        else                                          // 일반 아이템일 경우
        {
            itemList.Add(item);                                  // 아이템 리스트 추가
            SpawnSlots(itemList, itemSlotPanel);                 // 아이템 슬롯 생성
            UpdateItemCount(itemList.Count);                     // 수량 표시 갱신
        }
    }


    public void RemoveItem(ItemData item)
    {
        if (item.Type == EItemType.Consumable)
        {
            consumableList.Remove(item);                         // 소모품 리스트 제거
            SpawnSlots(consumableList, consumableSlotPanel);     // 슬롯 재생성
            UpdateItemCount(consumableList.Count);               // 수량 표시 갱신
        }
        else
        {
            itemList.Remove(item);                              // 아이템 리스트 제거
            SpawnSlots(itemList, itemSlotPanel);                
            UpdateItemCount(itemList.Count);
        }
    }

    public void OpenItemInventory()
    {
        itemInventoryPanel.SetActive(true);                      // 아이템 탭 활성화
        consumableInventoryPanel.SetActive(false);               // 소모품 탭 비활성화
        SpawnSlots(itemList, itemSlotPanel);                     // 아이템 슬롯 생성
        UpdateItemCount(itemList.Count);                         // 수량 텍스트 갱신
    }


    public void OpenConsumableInventory()
    {
        itemInventoryPanel.SetActive(false);                     // 아이템 탭 비활성화
        consumableInventoryPanel.SetActive(true);                // 소모품 탭 활성화
        SpawnSlots(consumableList, consumableSlotPanel);         // 소모품 슬롯 생성
        UpdateItemCount(consumableList.Count);
    }

 
    public void OpenInventory()
    {
        inventoryUIRoot.SetActive(true);                         // 인벤토리 UI 전체 활성화
        OpenItemInventory();                                     // 기본 탭은 아이템인벤
    }


    public void CloseInventory()
    {
        inventoryUIRoot.SetActive(false);                        // 인벤토리 UI 비활성화
    }


    public void UpdateItemCount(int current)
    {
        int max = 100;                                           // 최대값
        itemCountText.text = $"{current} / {max}";              // 텍스트: "현재 / 최대"
    }


    private void SpawnSlots(List<ItemData> dataList, Transform parentPanel)
    {
        ClearSlots();                                            // 기존 슬롯 제거

        foreach (ItemData item in dataList)
        {
            GameObject slotObj = Instantiate(slotPrefab, parentPanel);   // 슬롯 프리팹 생성 및 부모 설정
            InventorySlot slot = slotObj.GetComponent<InventorySlot>();
            slot.SetSlot(item);                                          // 슬롯에 아이템 데이터 적용
            spawnedSlots.Add(slotObj);                                   // 추적 리스트에 추가
        }
    }

    //  기존 슬롯 삭제
    private void ClearSlots()
    {
        foreach (GameObject obj in spawnedSlots)
        {
            Destroy(obj);
        }
        spawnedSlots.Clear();
    }
}