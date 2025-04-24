using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //필터 기능 적용
{
    public List<CharacterType> selectedAttributes = new();    // 선택된 속성 리스트
    public List<Grade> selectedGrades = new();                // 선택된 희귀도 리스트

    // 특정 조건(속성/등급)에 맞는 캐릭터 리스트 반환
    public List<Character> FilterCharacters(List<Character> allCharacters)
    {
        List<Character> filteredCharacters = new List<Character>();

        foreach (Character character in allCharacters) // 모든 캐릭터 순회
        {
            // 조건: 선택된 속성 + 등급이 모두 일치해야 리스트에 추가
            if ((selectedAttributes.Count == 0 || selectedAttributes.Contains(character.CharacterType)) &&
                (selectedGrades.Count == 0 || selectedGrades.Contains(character.Grade)))
            {
                filteredCharacters.Add(character);
            }
        }
        return filteredCharacters;
    }

    // 버튼에 연결해서 필터 적용
    public void OnClickApplyFilter()
    {
        // 저장된 모든 캐릭터 세이브 데이터 가져오기
        List<CharacterSaveData> allSaves = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        // 장착 캐릭터만 추출
        List<CharacterSaveData> equipped = new List<CharacterSaveData>();
        foreach (CharacterSaveData data in allSaves)
        {
            if (data.IsEquiped)
                equipped.Add(data);
        }
        Debug.Log($"[필터] 장착 캐릭터: {equipped.Count}명");

        // CharacterSaveData → Character SO 변환
        List<Character> allCharacters = new List<Character>();
        foreach (CharacterSaveData save in equipped)
        {
            Character so = GlobalDataTable.Instance.character.GetCharToID(save.ID);
            if (so != null)
                allCharacters.Add(so);
        }
        Debug.Log($"[필터] SO 변환된 캐릭터: {allCharacters.Count}명");

        // 선택된 조건으로 필터 적용
        List<Character> filtered = FilterCharacters(allCharacters);
        Debug.Log($"[필터] 최종 필터링 캐릭터: {filtered.Count}명");

        // SlotSpawner로 전달
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnFromFilteredCharacters(filtered);
            Debug.Log("[필터] 슬롯 스포너 호출: 슬롯 생성 완료");
        }
        else
        {
            Debug.LogWarning("슬롯 생성기(UICharacterSlotSpawner)를 찾지 못했습니다.");
        }
    }
}
