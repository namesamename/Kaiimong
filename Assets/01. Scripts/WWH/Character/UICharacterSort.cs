using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
public class UICharacterSort : MonoBehaviour
{
    public bool isLevelAscending = false;     // ���� ���� ����
    public bool isGradeAscending = false;    // ��͵� ���� ����

    [SerializeField] private TextMeshProUGUI levelSortButtonText;   // ���� ���� ��ư �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI gradeSortButtonText;  // ��͵� ���� ��ư �ؽ�Ʈ

    private void Start()
    {
        OnClickLevelSort();
        OnClickRaritySort();
    }

    // ĳ����/���̺� ������ ������ ����Ʈ�� �޾� ���� �� ��ȯ
    public List<(Character character, CharacterSaveData saveData)> SortByLevel(List<(Character character, CharacterSaveData saveData)> levelList)
    {
        // ����(���̺�) ��������, ���� �����̸� ��͵�(Grade) ��������
        if (isLevelAscending)
        {
            Debug.Log("[Sort] ���� �������� �� ��͵� ��������");
            return levelList.OrderBy(x => x.saveData.Level)
                       .ThenByDescending(y => y.character.Grade)
                       .ToList();
        }
        else
        {
            Debug.Log("[Sort] ���� �������� �� ��͵� ��������");
            return levelList.OrderByDescending(xx => xx.saveData.Level)
                       .ThenByDescending(yy => yy.character.Grade)
                       .ToList();
        }
    }

    public List<(Character character, CharacterSaveData saveData)> SortByRarity(List<(Character character, CharacterSaveData saveData)> gradeList)
    {
        // ��͵�(Grade) ��������, ���� ��͵��� ����(���̺�) ��������
        if (isGradeAscending)
        {
            Debug.Log("[Sort] ��͵� �������� �� ���� ��������");
            return gradeList.OrderBy(x => x.character.Grade)
                       .ThenByDescending(y => y.saveData.Level)
                       .ToList();
        }
        else
        {
            Debug.Log("[Sort] ��͵� �������� �� ���� ��������");
            return gradeList.OrderByDescending(aa => aa.character.Grade)
                       .ThenByDescending(bb => bb.saveData.Level)
                       .ToList();
        }
    }

    // ��ư Ŭ�� �̺�Ʈ�� ����
    public void OnClickLevelSort()
    {
        isLevelAscending = !isLevelAscending;
        UpdateLevelSortText();
        ApplySort();
    }

    public void OnClickRaritySort()
    {
        isGradeAscending = !isGradeAscending;
        UpdateRaritySortText();
        ApplySort();
    }

    private void UpdateLevelSortText()
    {
        levelSortButtonText.text = isLevelAscending ? "������" : "������";
    }

    private void UpdateRaritySortText()
    {
        gradeSortButtonText.text = isGradeAscending ? "��͵���" : "��͵���";
    }


    private void ApplySort()
    {
        // ���� ������ ĳ���� �����͸� ��������
        List<CharacterSaveData> allSave = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        Debug.Log($"[ApplySort] ����� ĳ���� ������ ����: {allSave.Count}");

        List<CharacterSaveData> equipped = new List<CharacterSaveData>();   // ������ ĳ���͸� ���� ����Ʈ

        foreach (CharacterSaveData data in allSave)                         // ����� ��� ĳ���� ������ ��ȸ
        {
            if (data.IsEquiped)                                             // ���� ���¶��
            {
                equipped.Add(data);                                         // ����Ʈ�� �߰�
            }
        }
        Debug.Log($"[ApplySort] ������ ĳ���� ��: {equipped.Count}");

        // (Character, SaveData) ������ ����
        List<(Character, CharacterSaveData)> pairList = new();
        foreach (var saveData in equipped)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);
            if (character != null)
            {
                pairList.Add((character, saveData));
                Debug.Log($"[ApplySort] �߰�: {character.Name}, Grade: {character.Grade}, Level: {saveData.Level}");
            }
        }

        // ���� ���� (�� �� �����ؼ� ���)
        // List<(Character, CharacterSaveData)> sorted = SortByLevel(pairList);
        List<(Character, CharacterSaveData)> sorted = SortByRarity(pairList);

        Debug.Log("[ApplySort] ���� �Ϸ�!");

        // ���� ������� ����
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnFromSortedList(sorted);
            Debug.Log("[ApplySort] ���� ������ ����!");
        }
        else
        {
            Debug.LogWarning("[ApplySort] ���� ������ ����!");
        }
    }
}
