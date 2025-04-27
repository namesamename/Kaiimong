using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

        //��� ĳ���� ��������

        // ��� ���̺� ������(12��) �ҷ�����
        List<CharacterSaveData> allSaves =
            SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        // (Character, SaveData) ������ ��ȯ
        var allPairs = allSaves
            .Select(d => (GlobalDataTable.Instance.character.GetCharToID(d.ID), d))
            .Where(p => p.Item1 != null)
            .ToList();

        Debug.Log($"[���� ��] ��ü ĳ���� ��: {allPairs.Count}");

        // SO�� �̾Ƽ� ���͸�
        var soList = allPairs.Select(p => p.Item1).ToList();
        var filteredSO = FilterCharacters(soList);

        // ���͵� SO�� �ٽ� (SO,SaveData) ������ ��Ī
        var filteredPairs = filteredSO
            .Select(so => allPairs.Find(p => p.Item1.ID == so.ID))
            .Where(p => p.Item1 != null)
            .ToList();

        Debug.Log($"[���� ��] ���͸��� ĳ���� ��: {filteredPairs.Count}");

        // �����ʿ� �Ѱܼ� ��ü ���͸��� �� �׸���
        var spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnFromSortedList(filteredPairs);
            Debug.Log("[����] ���� ���� �Ϸ�");
        }
        else
        {
            Debug.LogWarning("[����] ���� �����⸦ ã�� ���߽��ϴ�.");
        }
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



