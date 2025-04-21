using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : SO
{
    [Header("기본 정보")]
    public string Name;                    // 아이템 이름
    public ERarity Grade;                  // 희귀도

    [Header("속성")]
    public EItemType ItemType;                 // 아이템 타입
    public int ConsumeID;

    [Header("게임 내 수치")]
    public string IconPath;
    public int MaxStackCount;  // 최대 중첩 수량

    [Header("획득 경로")]
    public string Description;        // 설명
    public string ObtainLocation;     // 획득 경로
    public bool IsStack;

    public float Probability;              // 드랍 확률


    private void OnValidate()
    {
        switch (Grade)
        {
            case ERarity.S:
                Probability = 0.2F;
                break;
            case ERarity.A:
                Probability = 0.2F;
                break;
            case ERarity.B:
                Probability = 0.2F;
                break;
            case ERarity.C:
                Probability = 0.2F;
                break;
            case ERarity.D:
                Probability = 0.2F;
                break;
        }

    }
}

public enum EItemType 
{   None,
    Item,            // 아이템
    Consume       // 소모품
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