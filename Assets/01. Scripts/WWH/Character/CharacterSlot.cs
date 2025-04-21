using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI nameText;           // 캐릭터 이름 텍스트
    [SerializeField] private TextMeshProUGUI levelText;          // 캐릭터 레벨 텍스트
    [SerializeField] private Image attributeIcon;                // 속성 아이콘 이미지
    [SerializeField] private GameObject equippedMark;            // 장착 중 마크

    private CharacterSaveData characterData;                     // 캐릭터 저장 데이터 참조
    private int characterID;                                     // 캐릭터 고유 ID 저장

    public void SetSlot(CharacterCarrier carrier)                // 슬롯에 캐릭터 정보를 설정하는 함수
    {
        characterData = carrier.CharacterSaveData;               // 저장 데이터 가져오기
        characterID = characterData.ID;                          // 캐릭터 고유 ID 저장

        nameText.text = carrier.visual.name;                     // 이름 텍스트 설정
        levelText.text = $"Lv. {characterData.Level}";           // 레벨 텍스트 설정


        if (carrier.visual.icon != null)                         // 속성 아이콘 설정
            attributeIcon.sprite = carrier.visual.icon;
        else
            Debug.Log($"속성 아이콘이 없습니다: ID {characterID}");


           if (equippedMark != null)                             // 장착 여부에 따라 표시 ON/OFF
               equippedMark.SetActive(characterData.IsEquiped);
    }


    public CharacterSaveData GetCharacterData()                 // 슬롯에 연결된 캐릭터 데이터를 외부에서 사용할 수 있게 반환
    {
        return characterData;
    }


    public int GetCharacterID()                                 // 슬롯의 캐릭터 고유 ID 반환
    {
        return characterID;
    }
}