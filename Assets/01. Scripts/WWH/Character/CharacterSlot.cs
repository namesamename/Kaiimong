using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharacterSlot : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI nameText;           // 캐릭터 이름 텍스트
    [SerializeField] private TextMeshProUGUI levelText;          // 캐릭터 레벨 텍스트
 // [SerializeField] private Image attributeIcon;                // 속성 아이콘 이미지
    [SerializeField] private GameObject equippedMark;            // 장착 중 마크

    private CharacterSaveData characterData;                     // 캐릭터 저장 데이터 참조
    private Character characterSO;
    private int characterID;                                     // 캐릭터 고유 ID 저장

    public CharacterSaveData GetCharacterData() => characterData;
    public int GetCharacterID() => characterSO != null ? characterSO.ID : -1;

    public void SetSlot(CharacterSaveData saveData, Character character)                // 슬롯에 캐릭터 정보를 설정하는 함수
    {
        characterData = saveData;                      // 저장된 레벨, 장착 여부, ID 저장
        characterSO = character;
        RefreshUI();
        characterID = saveData.ID;                     // 캐릭터 ID 저장

        nameText.text = character.Name;                // 캐릭터 이름 표시
        levelText.text = $"Lv. {saveData.Level}";      // 캐릭터 레벨 표시

        Debug.Log($"[SetSlot] 슬롯에 적용: {character.Name} (ID:{character.ID}) Lv.{saveData.Level} Grade:{character.Grade}");
       /* if (attributeIcon != null && character.AttributeIcon!= null)
        {
            attributeIcon.sprite = character.AttributeIcon; // 속성 아이콘 표시
        }
        else
        {
            Debug.LogWarning($"[CharacterSlot] 속성 아이콘이 없습니다: ID {characterID}");
        }*/

        if (equippedMark != null)
        {
            equippedMark.SetActive(saveData.IsEquiped); // 장착 여부에 따라 마크 활성화
        }
    }


    public CharacterSaveData GetCharacterData()                 // 슬롯에 연결된 캐릭터 데이터를 외부에서 사용할 수 있게 반환
    {
        return characterData;
    }


    public int GetCharacterID()                                 // 슬롯의 캐릭터 고유 ID 반환
    {
        return characterID;
    }

    public void SetSlotFromGacha(Character character)
    {
        characterSO = character;

        // 새 세이브데이터 생성 (초기 레벨·장착 여부 지정)
        characterData = new CharacterSaveData
        {
            ID = characterSO.ID,
            Level = 1,
            IsEquiped = false,
        };

        // DataBase에 저장
     //   SaveDataBase.Instance.AddOrUpdateSave(characterData);

        // 3) UI 갱신
        RefreshUI();
        Debug.Log($"[Gacha] 신규 저장 생성 & 슬롯 세팅: {characterSO.Name} (ID:{characterSO.ID})");
    }

    private void RefreshUI()
    {
        if (characterSO == null || characterData == null) return;

        // 이름·레벨
        nameText.text = characterSO.Name;
        levelText.text = $"Lv. {characterData.Level}";

        // 속성 아이콘
      //  if (attributeIcon != null && characterSO.AttributeIcon != null)
      //      attributeIcon.sprite = characterSO.AttributeIcon;;

        // 장착 마크
        if (equippedMark != null)
            equippedMark.SetActive(characterData.IsEquiped);
    }
}