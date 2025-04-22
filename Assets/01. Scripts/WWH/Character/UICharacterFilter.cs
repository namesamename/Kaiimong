using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UICharacterFilter : MonoBehaviour                  //필터 기능 적용

    
    
{
    // public List<> selectedAttributes = new();             // 선택된 속성 리스트
    // public List<Grade> selectedGrades = new();        // 선택된 희귀도 리스트




    // 캐릭터가 현재 필터 조건에 맞는지 확인

    public List<Character> Characters(List<Character> allCharacter)
    {
        List<Character> filteredCharacters = new List<Character>();
        foreach (Character character in allCharacter)                   
        {
           // if (selectedAttributes.Contains(character.Attribute) &&             //속성과 등급이 선택된 필터 조건에 포함될 경우만 추가
           //  selectedGrades.Contains(character.Grade))
            {
                filteredCharacters.Add(character);                              // 조건 만족시 리스트에 추가
            }
        }
        return filteredCharacters;
    }

    /*ex공격력 200인 캐릭터만 보여주기

    public List<Character> characters(List<Character> allCharacter) 
    {
        List<Character> attackcharacters = new List<Character>();
        foreach (Character @char in allCharacter) //캐릭터 안에 캐릭터 자료형을 가진 캐릭터1의 모두를 가져온다.
        {
            if (@char.Defense < 100)

            {
                attackcharacters.Add(@char);

            }
        }
        return attackcharacters;
    }*/

    public void OnClickApplyFilter()  // 버튼에 연결해서 사용할 함수
    {

    }
}
