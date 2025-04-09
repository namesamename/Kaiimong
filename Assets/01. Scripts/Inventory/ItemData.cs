using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : ScriptableObject
{
    public string ItemName;       // ������ �̸�
    public Sprite Icon;           // ������ ������ �̹���
    public int ID;                // ������ ���� ID
    public EItemType Type;        // ������ Ÿ�� (���, �Ҹ�ǰ ������)
}

public enum EItemType
{
    Item,                         // ������
    Consumable,                   // �Ҹ�ǰ ������   
}
