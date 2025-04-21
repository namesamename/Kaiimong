using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIItemFilter : MonoBehaviour                // Ÿ�� �� ��͵� üũ
{
    public List<EItemType> selectedTypes = new();        // üũ�� ������ Ÿ��
    public List<ERarity> selectedRarities = new();       // üũ�� ��͵�

    
    public bool IsItemVisible(ItemData item)                        // ���� ���ǿ� �ش��ϴ� �����۸� true ��ȯ
    {
        bool typeMatch = selectedTypes.Contains(item.ItemType);     // Ÿ�� ��ġ
        bool rarityMatch = selectedRarities.Contains(item.Grade);   // ��͵� ��ġ
        return typeMatch && rarityMatch;                            // �� �� �����ؾ� true
    }
}
