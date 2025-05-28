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
        // 기존 UI 아이템 제거
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        List<Character> characters = new();

        // ID 1~12번 캐릭터를 순차적으로 불러오기
        for (int id = 1; id <= 12; id++)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(id);
            if (character != null)
            {
                characters.Add(character);
            }
            else
            {
                Debug.LogWarning($"[도감] ID {id}번 캐릭터를 찾을 수 없습니다.");
            }
        }

        // 등급 S → A → B 순서로 정렬
        characters.Sort((a, b) => GetGradeOrder(a.Grade).CompareTo(GetGradeOrder(b.Grade)));

        // 도감 UI 항목 생성
        foreach (var character in characters)
        {
            GameObject go = Instantiate(itemPrefab, contentRoot);
            var ui = go.GetComponent<CharacterBookItem>();
            await ui.Setup(character);
        }
    }

    // 정렬 기준을 위한 등급 우선순위 지정
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
