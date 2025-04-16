using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIInventory : MonoBehaviour
{
    public static UIInventory Instance { get; private set; }          // 싱글톤 접근용

    [Header("Inventory Panels")]
    [SerializeField] private GameObject itemInventoryButton;        // 아이템 탭 UI 패널
    [SerializeField] private GameObject consumableInventoryButton;  // 소모품 탭 UI 패널

    [Header("Inventory Info UI")]
    [SerializeField] private TextMeshProUGUI itemslotCountText;     // 아이템 슬롯 현재/최대 개수 표시 텍스트

    [Header("Slot Generation")]
    [SerializeField] private GameObject[] slotPrefabs;             // 회귀도 슬롯 프리팹 배열
    [SerializeField] private Transform itemSlotPanel;              // 아이템 탭 안의 슬롯 Content 오브젝트
    [SerializeField] private Transform consumableSlotPanel;        // 소모품 탭 안의 슬롯 Content 오브젝트

    [Header("Scroll Views")]
    [SerializeField] private GameObject itemScrollView;            // 아이템 스크롤 뷰 오브젝트
    [SerializeField] private GameObject consumableScrollView;      // 소모품 스크롤 뷰 오브젝트

    private List<GameObject> spawnedSlots = new List<GameObject>(); // 생성된 슬롯 오브젝트들을 추적하는 리스트


    private List<ItemData> itemList = new List<ItemData>();        // 일반 아이템 저장용 리스트
    private List<ItemData> consumableList = new List<ItemData>();  // 소모품 아이템 저장용 리스트



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject); // 중복 방지
            return;
        }

        Instance = this;
    }

    private void Start()                            
    {
        // 테스트용 아이템 생성
        ItemData testItemS = ScriptableObject.CreateInstance<ItemData>();
        testItemS.ItemName = "전설 아이템";
        testItemS.Rarity = ERarity.S;
        testItemS.Type = EItemType.Item;

        ItemData testItemA = ScriptableObject.CreateInstance<ItemData>();
        testItemA.ItemName = "엄청 좋은 아이템";
        testItemA.Rarity = ERarity.A;
        testItemA.Type = EItemType.Item;

        ItemData testItemB = ScriptableObject.CreateInstance<ItemData>();
        testItemB.ItemName = "좋은 아이템";
        testItemB.Rarity = ERarity.B;
        testItemB.Type = EItemType.Item;

        ItemData testItemC = ScriptableObject.CreateInstance<ItemData>();
        testItemC.ItemName = "조금 좋은 아이템";
        testItemC.Rarity = ERarity.C;
        testItemC.Type = EItemType.Item;

        ItemData testItemD = ScriptableObject.CreateInstance<ItemData>();
        testItemD.ItemName = "그냥 아이템";
        testItemD.Rarity = ERarity.D;
        testItemD.Type = EItemType.Item;

        // 인벤토리에 추가
        AddItem(testItemS);
        AddItem(testItemA);
        AddItem(testItemB);
        AddItem(testItemC);
        AddItem(testItemD);

        OpenItemInventory();                         // 시작 시 기본으로 아이템 탭 열기
    }

    private List<ItemData> SortByRarity(List<ItemData> list)           // 희귀도 순 정렬 (S → D 순)
    {
        return list.OrderBy(item => item.Rarity).ToList();
    }

    public void AddItem(ItemData item)
    {
        Debug.Log($"[AddItem 호출됨] {item.ItemName} / 타입: {item.Type}");

        if (item.Type == EItemType.Consumable)       // 소모품일 경우
        {
            consumableList.Add(item);                            // 소모품 리스트 추가
            consumableList = SortByRarity(consumableList);       // 소모품 회귀도 정렬
            SpawnSlots(consumableList, consumableSlotPanel);     // 소모품 슬롯 생성
            UpdateItemCount(consumableList.Count);               // 수량 표시 갱신
        }
        else                                          // 일반 아이템일 경우
        {
            itemList.Add(item);                                  // 아이템 리스트 추가
            itemList = SortByRarity(itemList);                   // 아이템 회귀도 정렬
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
            UpdateItemCount(consumableList.Count);               // 소모품 수량 표시 갱신
        }
        else
        {
            itemList.Remove(item);                              // 아이템 리스트 제거
            SpawnSlots(itemList, itemSlotPanel);                // 슬롯 재생성
            UpdateItemCount(itemList.Count);                    // 아이템 수량 표시 갱신
        }
    }

    public void OpenItemInventory()
    {

        itemInventoryButton.SetActive(true);                      // 아이템 탭 활성화
        consumableInventoryButton.SetActive(true);               

        itemScrollView.SetActive(true);                           // 아이템 스크롤 뷰 켜기
        consumableScrollView.SetActive(false);                    // 소모품 스크롤 뷰 끄기

        itemList = SortByRarity(itemList);                        // 아이템 리스트 정렬
        SpawnSlots(itemList, itemSlotPanel);                      // 아이템 슬롯 생성
        UpdateItemCount(itemList.Count);                          // 수량 텍스트 갱신
    }



    public void OpenConsumableInventory()
    {
        itemInventoryButton.SetActive(true);                     
        consumableInventoryButton.SetActive(true);                // 소모품 탭 활성화

        itemScrollView.SetActive(false);                         // 아이템 스크롤 뷰 끄기
        consumableScrollView.SetActive(true);                    // 아이템 스크롤 뷰 켜기

        consumableList = SortByRarity(consumableList);           // 소모품 리스트 정렬
        SpawnSlots(consumableList, consumableSlotPanel);         // 소모품 슬롯 생성
        UpdateItemCount(consumableList.Count);
    }   
    public void UpdateItemCount(int current)
    {
        int max = 100;                                           // 최대값
        itemslotCountText.text = $"{current} / {max}";           // 텍스트: "현재 / 최대"
    }


    private void SpawnSlots(List<ItemData> dataList, Transform parentPanel)     // 슬롯들을 생성하고 아이템 데이터를 적용
    {
        ClearSlots();                                                           // 기존 슬롯 제거

        List<ItemData> sortedList = SortByRarity(dataList);                     // 정렬된 리스트 사용
        Debug.Log($"[슬롯 생성] 총 {sortedList.Count}개의 슬롯 생성 시작");

        foreach (ItemData item in sortedList)                                     // 전달받은 아이템 리스트를 순회

        {
            GameObject prefab = GetSlotPrefabByRarity(item.Rarity);      // 희귀도에 맞는 슬롯 프리팹 선택
            GameObject slotObj = Instantiate(prefab, parentPanel);       // 슬롯 프리팹 생성 및 부모 설정

            Debug.Log($"[슬롯 생성] {item.ItemName} / {item.Rarity}");

            InventorySlot slot = slotObj.GetComponent<InventorySlot>();  // 생성된 슬롯 오브젝트에서 InventorySlot 컴포넌트를 가져옴
            slot.SetSlot(item);                                          // 슬롯에 아이템 데이터 적용
            spawnedSlots.Add(slotObj);                                   // 추적 리스트에 추가
        }
    }

    private void ClearSlots()                                           // 인벤토리 슬롯 초기화
    {
        foreach (GameObject obj in spawnedSlots)                        // 생성된 모든 슬롯 오브젝트를 하나씩 순회하며 파괴
        {
            Destroy(obj);                                               // 슬롯 오브젝트 제거
        }
        Debug.Log($"[슬롯 초기화] 이전 슬롯 {spawnedSlots.Count}개 제거");
        spawnedSlots.Clear();            
        
    }

    private GameObject GetSlotPrefabByRarity(ERarity rarity)           // 희귀도에 따라 슬롯 프리팹 반환
    {
        int index = EnumToIndex(rarity);                                      // enum을 정수(int) 인덱스로 사용


        if (index < 0 || index >= slotPrefabs.Length)
        {
            Debug.LogWarning($"[경고] slotPrefabs 배열에 '{rarity}'에 해당하는 슬롯 프리팹이 없습니다. 기본 프리팹(S)을 사용합니다.");
            return slotPrefabs[0]; 
        }

        if (slotPrefabs[index] == null)
        {
            Debug.LogWarning($"[경고] 희귀도 {rarity} 슬롯 프리팹이 null입니다. 기본 프리팹(S)을 사용합니다.");
            return slotPrefabs[0];
        }

        return slotPrefabs[index];                                     // 정상 범위면 해당 프리팹 반환
    }

    private int EnumToIndex(ERarity rarity)                         // ERarity → int 인덱스로 안전하게 변환
    {
        return (int)rarity;
    }
}