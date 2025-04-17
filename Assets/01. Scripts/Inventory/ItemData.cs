using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("�⺻ ����")]
    public string ID;                      // ������ ���� ID
    public string Name;                    // ������ �̸�
    public ERarity Grade;                  // ��͵�
    public string Stage;                   // ��� ��������
    public float Probability;              // ��� Ȯ��

    [Header("�Ҹ�ǰ ����")]
    public string UseType;                  // ���з�
    public int Value;                       // ȿ�� ��ġ

    [Header("�Ӽ�")]
    public EItemType Type;                 // ������ Ÿ��
    public EConsumableType ConsumableType; // �Ҹ�ǰ ���� �з�

    [Header("���� �� ��ġ")]
    public int MaxStackCount = 999;  // �ִ� ��ø ����

    [Header("ȹ�� ���")]
    public string ObtainLocation;     // ȹ�� ���

    public Sprite Icon;
}

public enum EItemType 
{   None,
    Item,            // ������
    Consumable       // �Ҹ�ǰ
}

public enum EConsumableType //�Ҹ�ǰ Ÿ��
{   None,
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