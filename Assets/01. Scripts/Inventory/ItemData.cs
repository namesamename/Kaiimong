using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public int ID;                   // 고유 ID
    public string ItemName;          // 아이템 이름
    public string Description;       // 아이템 설명
    public Sprite Icon;              // 아이콘 이미지

    [Header("속성")]
    public EItemType Type;                 // 아이템 타입
    public EConsumableType ConsumableType; // 소모품 세부 분류
    public ERarity Rarity;                 // 희귀도

    [Header("게임 내 수치")]
    public int MaxStackCount = 999;  // 최대 중첩 수량
}

public enum EItemType 
{ 
    Item,            // 아이템
    Consumable       // 소모품
}

public enum EConsumableType //소모품 타입
{
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