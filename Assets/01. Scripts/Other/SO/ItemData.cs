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
    public int ObtainLocationCount;     // ȹ�� ���
    public string FirstLocation;
    public string SecondLocation;
    public string ThridLocation;
    public float Probability;              // ��� Ȯ��


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

