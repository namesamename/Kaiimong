using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //���� ��� ����
{
    public List<CharacterType> selectedAttributes = new();    // ���õ� �Ӽ� ����Ʈ
    public List<Grade> selectedGrades = new();                // ���õ� ��͵� ����Ʈ

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
        // ����� ��� ĳ���� ���̺� ������ ��������
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
        }
    }
}
