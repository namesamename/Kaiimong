using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string ItemName;       // 아이템 이름
    public Sprite Icon;           // 아이템 아이콘 이미지
    public int ID;                // 아이템 고유 ID
    public EItemType Type;        // 아이템 타입 (장비, 소모품 같은거)
}

public enum EItemType
{
    Equipment,                    // 장비 아이템
    Consumable,                   // 소모품 아이템
    Resource                      // 자원/재료 아이템
}
