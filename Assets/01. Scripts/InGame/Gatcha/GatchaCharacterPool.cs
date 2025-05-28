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
        var characters = Resources.LoadAll<Character>("CharacterSO"); //리소스의 Char 폴더에 있은 오브젝트를 로드해오기 

        foreach (var character in characters) //foreach로 
        {
            if (!charactersByGrade.ContainsKey(character.Grade))
                charactersByGrade[character.Grade] = new List<Character>();

            charactersByGrade[character.Grade].Add(character);

            // 캐릭터 로딩 확인 로그
        }

        // 전체 풀 출력 로그
        foreach (var grade in charactersByGrade.Keys)
        {

            foreach (var c in charactersByGrade[grade])
            {
            }
        }
    }

    public List<Character> GetCharactersByGrade(Grade grade)
    {
        return charactersByGrade.ContainsKey(grade) ? charactersByGrade[grade] : new List<Character>();
    }
}
