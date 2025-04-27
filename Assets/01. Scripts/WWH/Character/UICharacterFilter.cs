using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //필터 기능 적용
{
    [Header("필터 패널")]
    [SerializeField] private GameObject filterPanel;       // 에디터에서 연결할 FilterPanel

    [Header("필터 상태")]
    public List<CharacterType> selectedAttributes = new();    // 선택된 속성 리스트

    public List<Grade> selectedGrades = new();                // 선택된 희귀도 리스트



    private void Start()
    {
        // 시작할 때 필터 패널 숨김
        if (filterPanel != null)
            filterPanel.SetActive(false);
    }

   // Filter 버튼 클릭 시 호출
    public void OnClickToggleFilterPanel()
    {
        if (filterPanel == null) return;
        filterPanel.SetActive(!filterPanel.activeSelf);
    }



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

        // 1) 필수 인스턴스 체크
        if (GlobalDataTable.Instance?.character == null)
        {
            Debug.LogWarning("[필터] GlobalDataTable.character가 아직 null입니다.");
            return;
        }
        if (character == null) return;

        // 2) 전체 저장 데이터 불러와 (Character, SaveData) 쌍으로 변환
        var rawSaves = SaveDataBase.Instance
            .GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        var allPairs = new List<(Character chr, CharacterSaveData save)>();
        foreach (var save in rawSaves)
        {
            var chr = GlobalDataTable.Instance.character.GetCharToID(save.ID);
            if (chr != null)
                allPairs.Add((chr, save));
        }
        Debug.Log($"[필터 전] 전체 캐릭터 수: {allPairs.Count}");

        // 3) 선택된 속성/등급 조건에 맞게 바로 필터링
        var filteredPairs = new List<(Character chr, CharacterSaveData save)>();
        foreach (var pair in allPairs)
        {
            bool matchesAttribute = selectedAttributes.Count == 0
                                    || selectedAttributes.Contains(pair.chr.CharacterType);
            bool matchesGrade = selectedGrades.Count == 0
                                    || selectedGrades.Contains(pair.chr.Grade);

            if (matchesAttribute && matchesGrade)
                filteredPairs.Add(pair);
        }
        Debug.Log($"[필터 후] 필터링된 캐릭터 수: {filteredPairs.Count}");

        // 4) 스포너에 넘겨서 재생성
        spawner.SpawnFromSortedList(filteredPairs);
        Debug.Log("[필터] 슬롯 생성 완료");

        // 5) 필터 패널 자동 닫기 (선택 사항)
        if (filterPanel != null)
            filterPanel.SetActive(false);
    }

    // 체크박스 토글로 속성 선택/해제될 때 호출

    public void OnAttributeToggle(CharacterType type, bool isOn)
    {
        if (isOn)
        {
            if (!selectedAttributes.Contains(type))
                selectedAttributes.Add(type);
        }
        else
        {
            selectedAttributes.Remove(type);
        }
    }

    // 체크박스 토글로 등급 선택/해제될 때 호출
    public void OnGradeToggle(Grade grade, bool isOn)
    {
        if (isOn)
        {
            if (!selectedGrades.Contains(grade))
                selectedGrades.Add(grade);
        }
        else
        {
            selectedGrades.Remove(grade);
        }
    }


    // Spirit 토글 바인딩용
    public void OnToggleSpirit(bool isOn)
    {
        OnAttributeToggle(CharacterType.Spirit, isOn);
    }

    // Physics 토글 바인딩용
    public void OnTogglePhysics(bool isOn)
    {
        OnAttributeToggle(CharacterType.Physics, isOn);
    }

    // S 등급 토글 바인딩용
    public void OnToggleGradeS(bool isOn)
    {
        OnGradeToggle(Grade.S, isOn);
    }

    // A 등급 토글 바인딩용
    public void OnToggleGradeA(bool isOn)
    {
        OnGradeToggle(Grade.A, isOn);
    }

    // B 등급 토글 바인딩용
    public void OnToggleGradeB(bool isOn)
    {
        OnGradeToggle(Grade.B, isOn);
    }




    /*  // 저장된 모든 캐릭터 세이브 데이터 가져오기
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
     } */

    // Checkbox가 켜질 때마다 호출될 메서드
}



