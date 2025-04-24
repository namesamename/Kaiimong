using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
public class UICharacterSort : MonoBehaviour
{
    public bool isLevelAscending = false;     // 레벨 정렬 방향
    public bool isGradeAscending = false;    // 희귀도 정렬 방향

    [SerializeField] private TextMeshProUGUI levelSortButtonText;   // 레벨 정렬 버튼 텍스트
    [SerializeField] private TextMeshProUGUI gradeSortButtonText;  // 희귀도 정렬 버튼 텍스트

    private void Start()
    {
        OnClickLevelSort();
        OnClickRaritySort();
    }

    // 캐릭터/세이브 데이터 쌍으로 리스트를 받아 정렬 후 반환
    public List<(Character character, CharacterSaveData saveData)> SortByLevel(List<(Character character, CharacterSaveData saveData)> levelList)
    {
        // 레벨(세이브) 내림차순, 동일 레벨이면 희귀도(Grade) 내림차순
        if (isLevelAscending)
        {
            Debug.Log("[Sort] 레벨 오름차순 → 희귀도 내림차순");
            return levelList.OrderBy(x => x.saveData.Level)
                       .ThenByDescending(y => y.character.Grade)
                       .ToList();
        }
        else
        {
            Debug.Log("[Sort] 레벨 내림차순 → 희귀도 내림차순");
            return levelList.OrderByDescending(xx => xx.saveData.Level)
                       .ThenByDescending(yy => yy.character.Grade)
                       .ToList();
        }
    }

    public List<(Character character, CharacterSaveData saveData)> SortByRarity(List<(Character character, CharacterSaveData saveData)> gradeList)
    {
        // 희귀도(Grade) 내림차순, 동일 희귀도면 레벨(세이브) 내림차순
        if (isGradeAscending)
        {
            Debug.Log("[Sort] 희귀도 오름차순 → 레벨 내림차순");
            return gradeList.OrderBy(x => x.character.Grade)
                       .ThenByDescending(y => y.saveData.Level)
                       .ToList();
        }
        else
        {
            Debug.Log("[Sort] 희귀도 내림차순 → 레벨 내림차순");
            return gradeList.OrderByDescending(aa => aa.character.Grade)
                       .ThenByDescending(bb => bb.saveData.Level)
                       .ToList();
        }
    }

    // 버튼 클릭 이벤트에 연결
    public void OnClickLevelSort()
    {
        isLevelAscending = !isLevelAscending;
        UpdateLevelSortText();
        ApplySort();
    }

    public void OnClickRaritySort()
    {
        isGradeAscending = !isGradeAscending;
        UpdateRaritySortText();
        ApplySort();
    }

    private void UpdateLevelSortText()
    {
        levelSortButtonText.text = isLevelAscending ? "레벨▲" : "레벨▼";
    }

    private void UpdateRaritySortText()
    {
        gradeSortButtonText.text = isGradeAscending ? "희귀도▲" : "희귀도▼";
    }


    private void ApplySort()
    {
        // 현재 장착된 캐릭터 데이터만 가져오기
        List<CharacterSaveData> allSave = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        Debug.Log($"[ApplySort] 저장된 캐릭터 데이터 개수: {allSave.Count}");

        List<CharacterSaveData> equipped = new List<CharacterSaveData>();   // 장착된 캐릭터만 담을 리스트

        foreach (CharacterSaveData data in allSave)                         // 저장된 모든 캐릭터 데이터 순회
        {
            if (data.IsEquiped)                                             // 장착 상태라면
            {
                equipped.Add(data);                                         // 리스트에 추가
            }
        }
        Debug.Log($"[ApplySort] 장착된 캐릭터 수: {equipped.Count}");

        // (Character, SaveData) 쌍으로 묶기
        List<(Character, CharacterSaveData)> pairList = new();
        foreach (var saveData in equipped)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);
            if (character != null)
            {
                pairList.Add((character, saveData));
                Debug.Log($"[ApplySort] 추가: {character.Name}, Grade: {character.Grade}, Level: {saveData.Level}");
            }
        }

        // 정렬 실행 (둘 중 선택해서 사용)
        // List<(Character, CharacterSaveData)> sorted = SortByLevel(pairList);
        List<(Character, CharacterSaveData)> sorted = SortByRarity(pairList);

        Debug.Log("[ApplySort] 정렬 완료!");

        // 슬롯 생성기로 전달
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnFromSortedList(sorted);
            Debug.Log("[ApplySort] 슬롯 생성기 실행!");
        }
        else
        {
            Debug.LogWarning("[ApplySort] 슬롯 생성기 없음!");
        }
    }
}
