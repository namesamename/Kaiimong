using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI nameText;           // ĳ���� �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI levelText;          // ĳ���� ���� �ؽ�Ʈ
    [SerializeField] private Image attributeIcon;                // �Ӽ� ������ �̹���
    [SerializeField] private GameObject equippedMark;            // ���� �� ��ũ

    private CharacterSaveData characterData;                     // ĳ���� ���� ������ ����
    private int characterID;                                     // ĳ���� ���� ID ����

    public void SetSlot(CharacterCarrier carrier)                // ���Կ� ĳ���� ������ �����ϴ� �Լ�
    {
        characterData = carrier.CharacterSaveData;               // ���� ������ ��������
        characterID = characterData.ID;                          // ĳ���� ���� ID ����

        nameText.text = carrier.visual.name;                     // �̸� �ؽ�Ʈ ����
        levelText.text = $"Lv. {characterData.Level}";           // ���� �ؽ�Ʈ ����


        if (carrier.visual.icon != null)                         // �Ӽ� ������ ����
            attributeIcon.sprite = carrier.visual.icon;
        else
            Debug.Log($"�Ӽ� �������� �����ϴ�: ID {characterID}");


           if (equippedMark != null)                             // ���� ���ο� ���� ǥ�� ON/OFF
               equippedMark.SetActive(characterData.IsEquiped);
    }


    public CharacterSaveData GetCharacterData()                 // ���Կ� ����� ĳ���� �����͸� �ܺο��� ����� �� �ְ� ��ȯ
    {
        return characterData;
    }


    public int GetCharacterID()                                 // ������ ĳ���� ���� ID ��ȯ
    {
        return characterID;
    }
}