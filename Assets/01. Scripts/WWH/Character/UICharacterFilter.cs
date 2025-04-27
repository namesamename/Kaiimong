using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //���� ��� ����
{
    public List<CharacterAttackType> selectedAttributes = new();    // ���õ� �Ӽ� ����Ʈ
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



