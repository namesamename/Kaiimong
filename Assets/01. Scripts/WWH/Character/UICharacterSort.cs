using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using JetBrains.Annotations;

public class UICharacterSort : MonoBehaviour                   // 레벨 자동, 희귀도 전용
{
    public bool isAscending = false;                           // 희귀도 정렬 방향 (false = 내림차순, true = 오름차순)

    [SerializeField] private TextMeshProUGUI sortButtonText;   // 희귀도 정렬 버튼에 표시될 텍스트


    private void Start()
    {
        UpdateSortText();                                      // 시작 시 정렬 텍스트 설정
    }

    public void OnClickRaritySort()                            // 희귀도 정렬 버튼 클릭 시 실행
    {
        isAscending = !isAscending;                            // 방향 토글 (오름 <-> 내림)
        UpdateSortText();                                      // 버튼 텍스트 변경
    }

    private void UpdateSortText()                              // 정렬 버튼 텍스트 갱신
    {
        if (isAscending == true)                               // 오름차순일 때
        {
            sortButtonText.text = "희귀도 ▲";             // 오름 텍스트 표시
        }
        else                                                   // 내림차순일 때
        {
            sortButtonText.text = "희귀도 ▼";             // 내림 텍스트 표시
        }
    }

    public List<CharacterCarrier> SortByLevel(List<CharacterCarrier> levellist)                                // 레벨 자동 정렬 (내림차순)
    {
        return levellist.OrderByDescending(characterLevel => characterLevel.CharacterSaveData.Level).ToList(); // 레벨 큰 순
    }


    public List<Character> SortByRarity(List<Character> list)          // 희귀도 정렬 (버튼 클릭 시 방향 적용)
    {
        if (isAscending)                                                                        // 오름차순
            return list.OrderBy(characterUp => characterUp.Grade).ToList();                     // 낮은 등급 → 높은 등급
        else                                                                                    // 내림차순
            return list.OrderByDescending(characterDown => characterDown.Grade).ToList();       // 높은 등급 → 낮은 등급 
    }
}
