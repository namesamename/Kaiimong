using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIItemFilter : MonoBehaviour                // 타입 및 희귀도 체크
{
    public List<EItemType> selectedTypes = new();        // 체크된 아이템 타입
    public List<ERarity> selectedRarities = new();       // 체크된 희귀도

    
    public bool IsItemVisible(ItemData item)                        // 필터 조건에 해당하는 아이템만 true 반환
    {
        bool typeMatch = selectedTypes.Contains(item.ItemType);     // 타입 일치
        bool rarityMatch = selectedRarities.Contains(item.Grade);   // 희귀도 일치
        return typeMatch && rarityMatch;                            // 둘 다 만족해야 true
    }
}
