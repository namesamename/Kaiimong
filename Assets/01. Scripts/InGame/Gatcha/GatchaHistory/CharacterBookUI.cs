using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class CharacterBookUI : MonoBehaviour
{
    [SerializeField] private Transform contentRoot;
    [SerializeField] private GameObject itemPrefab;

    private async void Start()
    {
        await LoadCharacterBook();
    }

    private async Task LoadCharacterBook()
    {
        // ���� UI ������ ����
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        List<Character> characters = new();

        // ID 1~12�� ĳ���͸� ���������� �ҷ�����
        for (int id = 1; id <= 12; id++)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(id);
            if (character != null)
            {
                characters.Add(character);
            }
            else
            {
                Debug.LogWarning($"[����] ID {id}�� ĳ���͸� ã�� �� �����ϴ�.");
            }
        }

        // ��� S �� A �� B ������ ����
        characters.Sort((a, b) => GetGradeOrder(a.Grade).CompareTo(GetGradeOrder(b.Grade)));

        // ���� UI �׸� ����
        foreach (var character in characters)
        {
            GameObject go = Instantiate(itemPrefab, contentRoot);
            var ui = go.GetComponent<CharacterBookItem>();
            await ui.Setup(character);
        }
    }

    // ���� ������ ���� ��� �켱���� ����
    private int GetGradeOrder(Grade grade)
    {
        return grade switch
        {
            Grade.S => 0,
            Grade.A => 1,
            Grade.B => 2,
            _ => 3
        };
    }
}
