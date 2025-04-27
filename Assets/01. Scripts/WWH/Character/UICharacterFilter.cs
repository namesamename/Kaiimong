using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //���� ��� ����
{
    [Header("���� �г�")]
    [SerializeField] private GameObject filterPanel;       // �����Ϳ��� ������ FilterPanel

    [Header("���� ����")]
    public List<CharacterType> selectedAttributes = new();    // ���õ� �Ӽ� ����Ʈ

    public List<Grade> selectedGrades = new();                // ���õ� ��͵� ����Ʈ



    private void Start()
    {
        // ������ �� ���� �г� ����
        if (filterPanel != null)
            filterPanel.SetActive(false);
    }

   // Filter ��ư Ŭ�� �� ȣ��
    public void OnClickToggleFilterPanel()
    {
        if (filterPanel == null) return;
        filterPanel.SetActive(!filterPanel.activeSelf);
    }



    // Ư�� ����(�Ӽ�/���)�� �´� ĳ���� ����Ʈ ��ȯ
    public List<Character> FilterCharacters(List<Character> allCharacters)
    {
        List<Character> filteredCharacters = new List<Character>();

        foreach (Character character in allCharacters) // ��� ĳ���� ��ȸ
        {
            // ����: ���õ� �Ӽ� + ����� ��� ��ġ�ؾ� ����Ʈ�� �߰�
            if ((selectedAttributes.Count == 0 || selectedAttributes.Contains(character.CharacterType)) &&
                (selectedGrades.Count == 0 || selectedGrades.Contains(character.Grade)))
            {
                filteredCharacters.Add(character);
            }
        }
        return filteredCharacters;
    }

    // ��ư�� �����ؼ� ���� ����
    public void OnClickApplyFilter()
    {

        // 1) �ʼ� �ν��Ͻ� üũ
        if (GlobalDataTable.Instance?.character == null)
        {
            Debug.LogWarning("[����] GlobalDataTable.character�� ���� null�Դϴ�.");
            return;
        }
        if (character == null) return;

        // 2) ��ü ���� ������ �ҷ��� (Character, SaveData) ������ ��ȯ
        var rawSaves = SaveDataBase.Instance
            .GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        var allPairs = new List<(Character chr, CharacterSaveData save)>();
        foreach (var save in rawSaves)
        {
            var chr = GlobalDataTable.Instance.character.GetCharToID(save.ID);
            if (chr != null)
                allPairs.Add((chr, save));
        }
        Debug.Log($"[���� ��] ��ü ĳ���� ��: {allPairs.Count}");

        // 3) ���õ� �Ӽ�/��� ���ǿ� �°� �ٷ� ���͸�
        var filteredPairs = new List<(Character chr, CharacterSaveData save)>();
        foreach (var pair in allPairs)
        {
            bool matchesAttribute = selectedAttributes.Count == 0
                                    || selectedAttributes.Contains(pair.chr.CharacterType);
            bool matchesGrade = selectedGrades.Count == 0
                                    || selectedGrades.Contains(pair.chr.Grade);

            if (matchesAttribute && matchesGrade)
                filteredPairs.Add(pair);
        }
        Debug.Log($"[���� ��] ���͸��� ĳ���� ��: {filteredPairs.Count}");

        // 4) �����ʿ� �Ѱܼ� �����
        spawner.SpawnFromSortedList(filteredPairs);
        Debug.Log("[����] ���� ���� �Ϸ�");

        // 5) ���� �г� �ڵ� �ݱ� (���� ����)
        if (filterPanel != null)
            filterPanel.SetActive(false);
    }

    // üũ�ڽ� ��۷� �Ӽ� ����/������ �� ȣ��

    public void OnAttributeToggle(CharacterType type, bool isOn)
    {
        if (isOn)
        {
            if (!selectedAttributes.Contains(type))
                selectedAttributes.Add(type);
        }
        else
        {
            selectedAttributes.Remove(type);
        }
    }

    // üũ�ڽ� ��۷� ��� ����/������ �� ȣ��
    public void OnGradeToggle(Grade grade, bool isOn)
    {
        if (isOn)
        {
            if (!selectedGrades.Contains(grade))
                selectedGrades.Add(grade);
        }
        else
        {
            selectedGrades.Remove(grade);
        }
    }


    // Spirit ��� ���ε���
    public void OnToggleSpirit(bool isOn)
    {
        OnAttributeToggle(CharacterType.Spirit, isOn);
    }

    // Physics ��� ���ε���
    public void OnTogglePhysics(bool isOn)
    {
        OnAttributeToggle(CharacterType.Physics, isOn);
    }

    // S ��� ��� ���ε���
    public void OnToggleGradeS(bool isOn)
    {
        OnGradeToggle(Grade.S, isOn);
    }

    // A ��� ��� ���ε���
    public void OnToggleGradeA(bool isOn)
    {
        OnGradeToggle(Grade.A, isOn);
    }

    // B ��� ��� ���ε���
    public void OnToggleGradeB(bool isOn)
    {
        OnGradeToggle(Grade.B, isOn);
    }




    /*  // ����� ��� ĳ���� ���̺� ������ ��������
     List<CharacterSaveData> allSaves = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

     // ���� ĳ���͸� ����
     List<CharacterSaveData> equipped = new List<CharacterSaveData>();
     foreach (CharacterSaveData data in allSaves)
     {
         if (data.IsEquiped)
             equipped.Add(data);
     }
     Debug.Log($"[����] ���� ĳ����: {equipped.Count}��");

     // CharacterSaveData �� Character SO ��ȯ
     List<Character> allCharacters = new List<Character>();
     foreach (CharacterSaveData save in equipped)
     {
         Character so = GlobalDataTable.Instance.character.GetCharToID(save.ID);
         if (so != null)
             allCharacters.Add(so);
     }
     Debug.Log($"[����] SO ��ȯ�� ĳ����: {allCharacters.Count}��");

     // ���õ� �������� ���� ����
     List<Character> filtered = FilterCharacters(allCharacters);
     Debug.Log($"[����] ���� ���͸� ĳ����: {filtered.Count}��");

     // SlotSpawner�� ����
     UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
     if (spawner != null)
     {
         spawner.SpawnFromFilteredCharacters(filtered);
         Debug.Log("[����] ���� ������ ȣ��: ���� ���� �Ϸ�");
     }
     else
     {
         Debug.LogWarning("���� ������(UICharacterSlotSpawner)�� ã�� ���߽��ϴ�.");
     } */

    // Checkbox�� ���� ������ ȣ��� �޼���
}



