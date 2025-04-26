using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.VisualScripting;
public enum SortType { Level, Rarity }

public class UICharacterSort : MonoBehaviour
{
    // 정렬 방향 플래그
    public bool isLevelAscending = false;     // 레벨 정렬 방향
    public bool isGradeAscending = false;     // 희귀도 정렬 방향

    // 현재 정렬 기준
    private SortType currentSortType = SortType.Level;

    [SerializeField] private TextMeshProUGUI levelSortButtonText;   // 레벨 정렬 버튼 텍스트
    [SerializeField] private TextMeshProUGUI gradeSortButtonText;   // 희귀도 정렬 버튼 텍스트

    private void Start()
    {
        // 내림차순(기본값)으로 정렬 및 버튼 텍스트 초기화
        currentSortType = SortType.Level;
        UpdateLevelSortText();
        UpdateRaritySortText();
       // ApplySort(); // 처음엔 레벨 내림차순으로 정렬
    }

    // 캐릭터/세이브 데이터 쌍 리스트 정렬(레벨)
    public List<(Character character, CharacterSaveData saveData)> SortByLevel(List<(Character character, CharacterSaveData saveData)> levelList)
    {
        // 레벨 기준 정렬 (동일 레벨이면 희귀도 내림차순)
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
            return levelList.OrderByDescending(x => x.saveData.Level)
                            .ThenByDescending(y => y.character.Grade)
                            .ToList();
        }
    }

    // 캐릭터/세이브 데이터 쌍 리스트 정렬(희귀도)
    public List<(Character character, CharacterSaveData saveData)> SortByRarity(List<(Character character, CharacterSaveData saveData)> gradeList)
    {
        // 희귀도 기준 정렬 (동일 희귀도면 레벨 내림차순)
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
            return gradeList.OrderByDescending(x => x.character.Grade)
                            .ThenByDescending(y => y.saveData.Level)
                            .ToList();
        }
    }

    // 레벨 정렬 버튼
    public void OnClickLevelSort()
    {
        isLevelAscending = !isLevelAscending;          // 방향 토글
        currentSortType = SortType.Level;              // 기준 설정
        UpdateLevelSortText();
        ApplySort();
    }

    // 희귀도 정렬 버튼
    public void OnClickRaritySort()
    {
        isGradeAscending = !isGradeAscending;          // 방향 토글
        currentSortType = SortType.Rarity;             // 기준 설정
        UpdateRaritySortText();
        ApplySort();
    }

    // 레벨 정렬 버튼 텍스트 갱신
    private void UpdateLevelSortText()
    {
        levelSortButtonText.text = isLevelAscending ? "레벨▲" : "레벨▼";
    }

    // 희귀도 정렬 버튼 텍스트 갱신
    private void UpdateRaritySortText()
    {
        gradeSortButtonText.text = isGradeAscending ? "희귀도▲" : "희귀도▼";
    }

    // 정렬 & 슬롯 재생성
    private void ApplySort()
    {
       
        
        //전체 데이터 가져오기
        
        // ↓ 장착 여부 필터 제거: 전체 리스트 사용
        var allSave = SaveDataBase.Instance
            .GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        // (Character, SaveData) 쌍으로 만듦
        var pairs = allSave
            .Select(d => (GlobalDataTable.Instance.character.GetCharToID(d.ID), d))
            .Where(p => p.Item1 != null)
            .ToList();

        // 정렬
        List<(Character, CharacterSaveData)> sorted = currentSortType == SortType.Level
            ? SortByLevel(pairs)
            : SortByRarity(pairs);

        // 스포너에 넘겨서 **전체** 재생성
        var spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
            spawner.SpawnFromSortedList(sorted);
        else
            Debug.LogWarning("[ApplySort] 슬롯 생성기를 찾을 수 없습니다.");



        /*
         * //  저장된 캐릭터 데이터 가져오기 (장착 캐릭터만)
        List<CharacterSaveData> allSave = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        List<CharacterSaveData> equipped = allSave.Where(data => data.IsEquiped).ToList();

        //  (Character, SaveData) 쌍 생성
        List<(Character, CharacterSaveData)> pairList = new();
        foreach (var saveData in equipped)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);
            if (character != null)
                pairList.Add((character, saveData));
        }

        // 정렬 기준에 따라 분기
        List<(Character, CharacterSaveData)> sorted;
        if (currentSortType == SortType.Level)
            sorted = SortByLevel(pairList);
        else
            sorted = SortByRarity(pairList);

        // 슬롯 생성기로 전달 (UI 갱신)
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
            spawner.SpawnFromSortedList(sorted);
        }
        else
        {
            Debug.LogWarning("[ApplySort] 슬롯 생성기 없음!");
        }*/
    }
}
