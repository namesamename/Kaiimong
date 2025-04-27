using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
public enum SortType { Level, Rarity }

public class UICharacterSort : MonoBehaviour
{
    // ���� ���� �÷���
    public bool isLevelAscending = false;     // ���� ���� ����
    public bool isGradeAscending = false;     // ��͵� ���� ����

    // ���� ���� ����
    private SortType currentSortType = SortType.Level;

    [SerializeField] private TextMeshProUGUI levelSortButtonText;   // ���� ���� ��ư �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI gradeSortButtonText;   // ��͵� ���� ��ư �ؽ�Ʈ

    private void Start()
    {
        // ��������(�⺻��)���� ���� �� ��ư �ؽ�Ʈ �ʱ�ȭ
        currentSortType = SortType.Level;
        UpdateLevelSortText();
        UpdateRaritySortText();
       // ApplySort(); // ó���� ���� ������������ ����
    }

    // ĳ����/���̺� ������ �� ����Ʈ ����(����)
    public List<(Character character, CharacterSaveData saveData)> SortByLevel(List<(Character character, CharacterSaveData saveData)> levelList)
    {
        // ���� ���� ���� (���� �����̸� ��͵� ��������)
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
            return levelList.OrderByDescending(x => x.saveData.Level)
                            .ThenByDescending(y => y.character.Grade)
                            .ToList();
        }
    }

    // ĳ����/���̺� ������ �� ����Ʈ ����(��͵�)
    public List<(Character character, CharacterSaveData saveData)> SortByRarity(List<(Character character, CharacterSaveData saveData)> gradeList)
    {
        // ��͵� ���� ���� (���� ��͵��� ���� ��������)
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
            return gradeList.OrderByDescending(x => x.character.Grade)
                            .ThenByDescending(y => y.saveData.Level)
                            .ToList();
        }
    }

    // ���� ���� ��ư
    public void OnClickLevelSort()
    {
        isLevelAscending = !isLevelAscending;          // ���� ���
        currentSortType = SortType.Level;              // ���� ����
        UpdateLevelSortText();
        ApplySort();
    }

    // ��͵� ���� ��ư
    public void OnClickRaritySort()
    {
        isGradeAscending = !isGradeAscending;          // ���� ���
        currentSortType = SortType.Rarity;             // ���� ����
        UpdateRaritySortText();
        ApplySort();
    }

    // ���� ���� ��ư �ؽ�Ʈ ����
    private void UpdateLevelSortText()
    {
        levelSortButtonText.text = isLevelAscending ? "������" : "������";
    }

    // ��͵� ���� ��ư �ؽ�Ʈ ����
    private void UpdateRaritySortText()
    {
        gradeSortButtonText.text = isGradeAscending ? "��͵���" : "��͵���";
    }

    // ���� & ���� �����
    private void ApplySort()
    {
       
        
        //��ü ������ ��������
        
        // �� ���� ���� ���� ����: ��ü ����Ʈ ���
        var allSave = SaveDataBase.Instance
            .GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        // (Character, SaveData) ������ ����
        var pairs = allSave
            .Select(d => (GlobalDataTable.Instance.character.GetCharToID(d.ID), d))
            .Where(p => p.Item1 != null)
            .ToList();

        // ����
        List<(Character, CharacterSaveData)> sorted = currentSortType == SortType.Level
            ? SortByLevel(pairs)
            : SortByRarity(pairs);

        // �����ʿ� �Ѱܼ� **��ü** �����
        var spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
            spawner.SpawnFromSortedList(sorted);
        else
            Debug.LogWarning("[ApplySort] ���� �����⸦ ã�� �� �����ϴ�.");



        /*
         * //  ����� ĳ���� ������ �������� (���� ĳ���͸�)
        List<CharacterSaveData> allSave = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        List<CharacterSaveData> equipped = allSave.Where(data => data.IsEquiped).ToList();

        //  (Character, SaveData) �� ����
        List<(Character, CharacterSaveData)> pairList = new();
        foreach (var saveData in equipped)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);
            if (character != null)
                pairList.Add((character, saveData));
        }

        // ���� ���ؿ� ���� �б�
        List<(Character, CharacterSaveData)> sorted;
        if (currentSortType == SortType.Level)
            sorted = SortByLevel(pairList);
        else
            sorted = SortByRarity(pairList);

        // ���� ������� ���� (UI ����)
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnFromSortedList(sorted);
        }
        else
        {
            Debug.LogWarning("[ApplySort] ���� ������ ����!");
        }*/
    }
}
