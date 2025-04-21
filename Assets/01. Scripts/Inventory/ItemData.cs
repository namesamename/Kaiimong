using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemData", menuName = "Inventory/ItemData")]
public class ItemData : SO
{
    [Header("�⺻ ����")]
    public string Name;                    // ������ �̸�
    public ERarity Grade;                  // ��͵�

    [Header("�Ӽ�")]
    public EItemType ItemType;                 // ������ Ÿ��
    public int ConsumeID;

    [Header("���� �� ��ġ")]
    public string IconPath;
    public int MaxStackCount;  // �ִ� ��ø ����

    [Header("ȹ�� ���")]
    public string Description;        // ����
    public string ObtainLocation;     // ȹ�� ���
    public bool IsStack;

    public float Probability;              // ��� Ȯ��


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
    Item,            // ������
    Consume       // �Ҹ�ǰ
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