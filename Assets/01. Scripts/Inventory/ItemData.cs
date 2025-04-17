using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string ID;                      // 아이템 고유 ID
    public string Name;                    // 아이템 이름
    public ERarity Grade;                  // 희귀도
    public string Stage;                   // 드랍 스테이지
    public float Probability;              // 드랍 확률

    [Header("소모품 정보")]
    public string UseType;                  // 사용분류
    public int Value;                       // 효과 수치

    [Header("속성")]
    public EItemType Type;                 // 아이템 타입
    public EConsumableType ConsumableType; // 소모품 세부 분류

    [Header("게임 내 수치")]
    public int MaxStackCount = 999;  // 최대 중첩 수량

    [Header("획득 경로")]
    public string ObtainLocation;     // 획득 경로

    public Sprite Icon;
}

public enum EItemType 
{   None,
    Item,            // 아이템
    Consumable       // 소모품
}

public enum EConsumableType //소모품 타입
{   None,
    EnergyItem,     // 활동력 충전 아이템
    Currency,       // 픽업 재화
    Box            // 아이템 박스
}
public enum ERarity 
{
    S,  // S급
    A,  // A급
    B,  // B급
    C,  // C급
    D   // D급
}