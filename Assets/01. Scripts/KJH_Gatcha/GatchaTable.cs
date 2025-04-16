using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

public class GatchaTable : MonoBehaviour
{
    public enum Grade
    {
        S,
        A,
        B
    }

    [System.Serializable]
    public class CharacterData
    {
        public string name;
        public Grade grade;

        public CharacterData(string name, Grade grade)
        {
            this.name = name;
            this.grade = grade;
        }
    }


    public static List<CharacterData> sCharacters = new List<CharacterData>
    {
        new CharacterData("KimSSSS", Grade.S),
        new CharacterData("LeeSS", Grade.S)
    };

    public static List<CharacterData> aCharacters = new List<CharacterData>
    {
        new CharacterData("KooAAA", Grade.A),
        new CharacterData("ParkAA", Grade.A)
    };

    public static List<CharacterData> bCharacters = new List<CharacterData>
    {
        new CharacterData("ChoiBB", Grade.B),
        new CharacterData("YoonBB", Grade.B),
        new CharacterData("HanBB", Grade.B),
        new CharacterData("dfgnBB", Grade.B),

    };

    public static CharacterData GetRandomCharacter()
    {
        float rand = Random.Range(0f, 100f);

        Grade chosenGrade;

        if (rand < 3f)  // È®·ü
            chosenGrade = Grade.S;
        else if (rand < 20f)
            chosenGrade = Grade.A;
        else
            chosenGrade = Grade.B;

        List<CharacterData> pool = GetPool(chosenGrade);
        return pool[Random.Range(0, pool.Count)];
    }

    private static List<CharacterData> GetPool(Grade grade)
    {
        switch (grade)
        {
            case Grade.S: return sCharacters;
            case Grade.A: return aCharacters;
            case Grade.B: return bCharacters;
            default: return bCharacters;
        }
    }
}

