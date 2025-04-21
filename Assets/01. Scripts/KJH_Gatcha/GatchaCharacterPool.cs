using System.Collections.Generic;
using UnityEngine;

public class GatchaCharacterPool : MonoBehaviour //Resource/char에 있는 캐릭터들을 받아오는 클래스.
{
    public static GatchaCharacterPool Instance { get; private set; }

    private Dictionary<Grade, List<Character>> charactersByGrade = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Initialize();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        var characters = Resources.LoadAll<Character>("Char");

        foreach (var character in characters)
        {
            if (!charactersByGrade.ContainsKey(character.Grade))
                charactersByGrade[character.Grade] = new List<Character>();

            charactersByGrade[character.Grade].Add(character);

            // 캐릭터 로딩 확인 로그
            Debug.Log($"[캐릭터 로딩] {character.Grade} - {character.Name}");
        }

        // 전체 풀 출력 로그
        foreach (var grade in charactersByGrade.Keys)
        {
            Debug.Log($"==={grade} 등급 캐릭터 목록===");

            foreach (var c in charactersByGrade[grade])
            {
                Debug.Log($"ㆍ{c.Name}");
            }
        }
    }

    public List<Character> GetCharactersByGrade(Grade grade)
    {
        return charactersByGrade.ContainsKey(grade) ? charactersByGrade[grade] : new List<Character>();
    }
}
