using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //필터 기능 적용
{
     public List<CharacterType> selectedAttributes = new();             // 선택된 속성 리스트
     public List<Grade> selectedGrades = new();                         // 선택된 희귀도 리스트




    // 캐릭터가 현재 필터 조건에 맞는지 확인

    public List<Character> Characters(List<Character> allCharacter)               // 필터된 캐릭터를 저장할 리스트 생성
    {
        List<Character> filteredCharacters = new List<Character>();
        foreach (Character character in allCharacter)                             // 모든 캐릭터를 하나씩 확인
        {
             if (selectedAttributes.Contains(character.CharacterType) &&            // 속성과 등급이 선택된 필터 조건에 포함되는지
              selectedGrades.Contains(character.Grade))                         // 캐릭터의 등급이 선택된 필터 조건에 포함되는지 확인
            {
                filteredCharacters.Add(character);                               // 조건 만족시 리스트에 추가
            }
        }
        return filteredCharacters;
    }

    public void OnClickApplyFilter()  // 버튼에 연결해서 사용할 함수
    {
        // 저장된 모든 캐릭터 세이브 데이터 가져오기
        List<CharacterSaveData> saveDataList = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        
        // isEquip이 true인 캐릭터만 필터링

        List<CharacterSaveData> equippedData = new List<CharacterSaveData>();   // 장착 캐릭터 저장용 리스트

        foreach (CharacterSaveData data in saveDataList)                        // 모든 저장 데이터를 순회
        {
            if (data.IsEquiped)                                                 // 장착 여부 확인
            {
                equippedData.Add(data);                                         // 장착된 캐릭터만 추가
            }
        }

        // ScriptableObject 기반 Character로 변환

        List<Character> allCharacters = new();
        foreach (var saveData in equippedData)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);
            if (character != null)
                allCharacters.Add(character);
        }

        // 속성 + 등급 필터 적용
        List<Character> filteredCharacters = Characters(allCharacters);  // 현재 조건에 맞는 캐릭터 추출

        
        // 정렬 스크립트가 있다면 적용 (예: 희귀도 오름/내림)

        UICharacterSort sorter = FindObjectOfType<UICharacterSort>();
        if (sorter != null)
        {
 //           filteredCharacters = sorter.CharacterDecide(filteredCharacters);
        }

        // 슬롯 생성
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
//            spawner.SpawnFromSaveData(filteredCharacters);
        }
    }
}
