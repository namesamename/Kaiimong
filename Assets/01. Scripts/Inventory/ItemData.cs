using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("�⺻ ����")]
    public int ID;                   // ���� ID
    public string ItemName;          // ������ �̸�
    public string Description;       // ������ ����
    public Sprite Icon;              // ������ �̹���

    [Header("�Ӽ�")]
    public EItemType Type;                 // ������ Ÿ��
    public EConsumableType ConsumableType; // �Ҹ�ǰ ���� �з�
    public ERarity Rarity;                 // ��͵�

    [Header("���� �� ��ġ")]
    public int MaxStackCount = 999;  // �ִ� ��ø ����
}

public enum EItemType 
{ 
    Item,            // ������
    Consumable       // �Ҹ�ǰ
}

public enum EConsumableType //�Ҹ�ǰ Ÿ��
{
    EnergyItem,     // Ȱ���� ���� ������
    Currency,       // �Ⱦ� ��ȭ
    Box            // ������ �ڽ�
}
public enum ERarity 
{
    S,  // S��
    A,  // A��
    B,  // B��
    C,  // C��
    D   // D��
}