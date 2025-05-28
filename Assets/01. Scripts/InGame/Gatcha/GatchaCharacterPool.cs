using System.Collections.Generic;
using UnityEngine;

public class GatchaCharacterPool : MonoBehaviour //Resource/char�� �ִ� ĳ���͵��� �޾ƿ��� Ŭ����.
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
        var characters = Resources.LoadAll<Character>("CharacterSO"); //���ҽ��� Char ������ ���� ������Ʈ�� �ε��ؿ��� 

        foreach (var character in characters) //foreach�� 
        {
            if (!charactersByGrade.ContainsKey(character.Grade))
                charactersByGrade[character.Grade] = new List<Character>();

            charactersByGrade[character.Grade].Add(character);

            // ĳ���� �ε� Ȯ�� �α�
        }

        // ��ü Ǯ ��� �α�
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
