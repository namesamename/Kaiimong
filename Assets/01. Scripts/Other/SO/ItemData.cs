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
    public int ObtainLocationCount;     // 획득 경로
    public string FirstLocation;
    public string SecondLocation;
    public string ThridLocation;
    public float Probability;              // 드랍 확률


    private void OnValidate()
    {
        switch (Grade)
        {
            case ERarity.S:
                Probability = 20;
                break;
            case ERarity.A:
                Probability = 20;
                break;
            case ERarity.B:
                Probability = 20;
                break;
            case ERarity.C:
                Probability = 20;
                break;
            case ERarity.D:
                Probability = 20;
                break;
        }

    }
}

