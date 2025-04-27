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
        ApplySort(); // ó���� ���� ������������ ����
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

    // �׻� ��ü 12�� ���� & ���� �����
    private void ApplySort()
    {


        // 1) �ʼ� �ν��Ͻ� üũ
        if (GlobalDataTable.Instance == null || GlobalDataTable.Instance.character == null)
        {
            Debug.LogWarning("[ApplySort] GlobalDataTable.character�� ���� null�Դϴ�. �ٽ� �õ��ϼ���.");
            return;
        }

        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner == null)
        {
            Debug.LogWarning("[ApplySort] UICharacterSlotSpawner�� ã�� �� �����ϴ�.");
            return;
        }

        // 2) ��ü ���嵥���� �ҷ��� (Character, SaveData) ������ ����Ʈ�� ���
        var rawSaves = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        var pairs = new List<(Character, CharacterSaveData)>();
        foreach (var save in rawSaves)
        {
            Character chr = GlobalDataTable.Instance.character.GetCharToID(save.ID);
            if (chr != null)
                pairs.Add((chr, save));
        }

        // 3) ����
        if (currentSortType == SortType.Level)
        {
            // ���� ��������, ���� ������ �� ��͵� ��������
            pairs.Sort((a, b) =>
            {
                int compare = b.Item2.Level.CompareTo(a.Item2.Level);
                if (compare == 0)
                    compare = ((int)b.Item1.Grade).CompareTo((int)a.Item1.Grade);
                return compare;
            });
        }
        else // SortType.Rarity
        {
            // ��͵� ��������, ���� ��͵��� �� ���� ��������
            pairs.Sort((a, b) =>
            {
                int compare = ((int)b.Item1.Grade).CompareTo((int)a.Item1.Grade);
                if (compare == 0)
                    compare = b.Item2.Level.CompareTo(a.Item2.Level);
                return compare;
            });
        }

        // 4) �����ʿ� �Ѱܼ� ���� �����
        spawner.SpawnFromSortedList(pairs);

        //�׻� ������ ĳ���͸� �����ϰ� �����ֱ�
        /*    private void ApplySort()
        {
            // 1) �ʼ� �ν��Ͻ� üũ
    if (GlobalDataTable.Instance == null || GlobalDataTable.Instance.character == null)
    {
        Debug.LogWarning("[ApplySort] GlobalDataTable.character�� ���� null�Դϴ�.");
        return;
    }

    UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
    if (spawner == null)
    {
        Debug.LogWarning("[ApplySort] UICharacterSlotSpawner�� ã�� �� �����ϴ�.");
        return;
    }

    // 2) ������ ĳ���͸� (Character, SaveData) ����Ʈ�� ���
    var list = new List<(Character chr, CharacterSaveData save)>();
    foreach (var save in SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character))
    {
        if (!save.IsEquiped) 
            continue;

        Character chr = GlobalDataTable.Instance.character.GetCharToID(save.ID);
        if (chr != null)
            list.Add((chr, save));
    }

    // 3) �� ���� Sort
    list.Sort((a, b) =>
    {
        int cmp;
        if (currentSortType == SortType.Level)
        {
            // ���� �������� �� ���� �����̸� ��͵� ��������
            cmp = b.save.Level.CompareTo(a.save.Level);
            if (cmp == 0)
                cmp = ((int)b.chr.Grade).CompareTo((int)a.chr.Grade);
        }
        else
        {
            // ��͵� �������� �� ���� ��͵��� ���� ��������
            cmp = ((int)b.chr.Grade).CompareTo((int)a.chr.Grade);
            if (cmp == 0)
                cmp = b.save.Level.CompareTo(a.save.Level);
        }
        return cmp;
    });

    // 4) �����ʿ� �Ѱ� �����
    spawner.SpawnFromSortedList(list);
      }*/
    }
}
