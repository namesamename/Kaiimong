using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI nameText;           // ĳ���� �̸� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI levelText;          // ĳ���� ���� �ؽ�Ʈ
 // [SerializeField] private Image attributeIcon;                // �Ӽ� ������ �̹���
    [SerializeField] private GameObject equippedMark;            // ���� �� ��ũ

    private CharacterSaveData characterData;                     // ĳ���� ���� ������ ����
    private Character characterSO;
    private int characterID;                                     // ĳ���� ���� ID ����

    public CharacterSaveData GetCharacterData() => characterData;
    public int GetCharacterID() => characterSO != null ? characterSO.ID : -1;

    public void SetSlot(CharacterSaveData saveData, Character character)                // ���Կ� ĳ���� ������ �����ϴ� �Լ�
    {
        characterData = saveData;                      // ����� ����, ���� ����, ID ����
        characterSO = character;
        RefreshUI();
        characterID = saveData.ID;                     // ĳ���� ID ����

        nameText.text = character.Name;                // ĳ���� �̸� ǥ��
        levelText.text = $"Lv. {saveData.Level}";      // ĳ���� ���� ǥ��

        Debug.Log($"[SetSlot] ���Կ� ����: {character.Name} (ID:{character.ID}) Lv.{saveData.Level} Grade:{character.Grade}");
       /* if (attributeIcon != null && character.AttributeIcon!= null)
        {
            attributeIcon.sprite = character.AttributeIcon; // �Ӽ� ������ ǥ��
        }
        else
        {
            Debug.LogWarning($"[CharacterSlot] �Ӽ� �������� �����ϴ�: ID {characterID}");
        }*/

        if (equippedMark != null)
        {
            equippedMark.SetActive(saveData.IsEquiped); // ���� ���ο� ���� ��ũ Ȱ��ȭ
        }
    }


    public CharacterSaveData GetCharacterData()                 // ���Կ� ����� ĳ���� �����͸� �ܺο��� ����� �� �ְ� ��ȯ
    {
        return characterData;
    }


    public int GetCharacterID()                                 // ������ ĳ���� ���� ID ��ȯ
    {
        return characterID;
    }

    public void SetSlotFromGacha(Character character)
    {
        characterSO = character;

        // �� ���̺굥���� ���� (�ʱ� ���������� ���� ����)
        characterData = new CharacterSaveData
        {
            ID = characterSO.ID,
            Level = 1,
            IsEquiped = false,
        };

        // DataBase�� ����
     //   SaveDataBase.Instance.AddOrUpdateSave(characterData);

        // 3) UI ����
        RefreshUI();
        Debug.Log($"[Gacha] �ű� ���� ���� & ���� ����: {characterSO.Name} (ID:{characterSO.ID})");
    }

    private void RefreshUI()
    {
        if (characterSO == null || characterData == null) return;

        // �̸�������
        nameText.text = characterSO.Name;
        levelText.text = $"Lv. {characterData.Level}";

        // �Ӽ� ������
      //  if (attributeIcon != null && characterSO.AttributeIcon != null)
      //      attributeIcon.sprite = characterSO.AttributeIcon;;

        // ���� ��ũ
        if (equippedMark != null)
            equippedMark.SetActive(characterData.IsEquiped);
    }
}