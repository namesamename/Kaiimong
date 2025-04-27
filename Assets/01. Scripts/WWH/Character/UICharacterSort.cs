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
        ApplySort(); // 처음엔 레벨 내림차순으로 정렬
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

    // 항상 전체 12명만 정렬 & 슬롯 재생성
    private void ApplySort()
    {


        // 1) 필수 인스턴스 체크
        if (GlobalDataTable.Instance == null || GlobalDataTable.Instance.character == null)
        {
            Debug.LogWarning("[ApplySort] GlobalDataTable.character가 아직 null입니다. 다시 시도하세요.");
            return;
        }

        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner == null)
        {
            Debug.LogWarning("[ApplySort] UICharacterSlotSpawner를 찾을 수 없습니다.");
            return;
        }

        // 2) 전체 저장데이터 불러와 (Character, SaveData) 쌍으로 리스트에 담기
        var rawSaves = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        var pairs = new List<(Character, CharacterSaveData)>();
        foreach (var save in rawSaves)
        {
            Character chr = GlobalDataTable.Instance.character.GetCharToID(save.ID);
            if (chr != null)
                pairs.Add((chr, save));
        }

        // 3) 정렬
        if (currentSortType == SortType.Level)
        {
            // 레벨 내림차순, 동일 레벨일 땐 희귀도 내림차순
            pairs.Sort((a, b) =>
            {
                int compare = b.Item2.Level.CompareTo(a.Item2.Level);
                if (compare == 0)
                    compare = ((int)b.Item1.Grade).CompareTo((int)a.Item1.Grade);
                return compare;
            });
        }
        else // SortType.Rarity
        {
            // 희귀도 내림차순, 동일 희귀도일 땐 레벨 내림차순
            pairs.Sort((a, b) =>
            {
                int compare = ((int)b.Item1.Grade).CompareTo((int)a.Item1.Grade);
                if (compare == 0)
                    compare = b.Item2.Level.CompareTo(a.Item2.Level);
                return compare;
            });
        }

        // 4) 스포너에 넘겨서 슬롯 재생성
        spawner.SpawnFromSortedList(pairs);

        //항상 장착된 캐릭터만 정렬하고 보여주기
        /*    private void ApplySort()
        {
            // 1) 필수 인스턴스 체크
    if (GlobalDataTable.Instance == null || GlobalDataTable.Instance.character == null)
    {
        Debug.LogWarning("[ApplySort] GlobalDataTable.character가 아직 null입니다.");
        return;
    }

    UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
    if (spawner == null)
    {
        Debug.LogWarning("[ApplySort] UICharacterSlotSpawner를 찾을 수 없습니다.");
        return;
    }

    // 2) 장착된 캐릭터만 (Character, SaveData) 리스트에 담기
    var list = new List<(Character chr, CharacterSaveData save)>();
    foreach (var save in SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character))
    {
        if (!save.IsEquiped) 
            continue;

        Character chr = GlobalDataTable.Instance.character.GetCharToID(save.ID);
        if (chr != null)
            list.Add((chr, save));
    }

    // 3) 한 번에 Sort
    list.Sort((a, b) =>
    {
        int cmp;
        if (currentSortType == SortType.Level)
        {
            // 레벨 내림차순 → 동일 레벨이면 희귀도 내림차순
            cmp = b.save.Level.CompareTo(a.save.Level);
            if (cmp == 0)
                cmp = ((int)b.chr.Grade).CompareTo((int)a.chr.Grade);
        }
        else
        {
            // 희귀도 내림차순 → 동일 희귀도면 레벨 내림차순
            cmp = ((int)b.chr.Grade).CompareTo((int)a.chr.Grade);
            if (cmp == 0)
                cmp = b.save.Level.CompareTo(a.save.Level);
        }
        return cmp;
    });

    // 4) 스포너에 넘겨 재생성
    spawner.SpawnFromSortedList(list);
      }*/
    }
}
