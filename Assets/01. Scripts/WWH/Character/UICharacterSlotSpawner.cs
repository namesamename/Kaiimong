using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GatchaTable;

public class UICharacterSlotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] slotPrefabs;              // 슬롯 프리팹
    [SerializeField] private Transform slotParent;                  // 슬롯들이 들어갈 Content

    [SerializeField] private List<Character> defaultcharacters;      //캐릭터 목록

    private List<GameObject> spawnedSlots = new List<GameObject>(); // 생성된 슬롯들 추적

    List<CharacterSaveData> characterSave = new List<CharacterSaveData>();     //CharacterSaveData 타입 담은 리스트



    private void Start()
    {
        Debug.Log("[테스트] UICharacterSlotSpawner 스크립트가 실행되고 있음");
        CharacterDecide();
        LoadAllCharacters();
        //LoadAllCharacters(characters(characterSave));
    }

    public void SpawnFromSaveData(List<CharacterSaveData> saveDataList)     // 저장된 캐릭터 데이터 기반으로 슬롯 생성
    {
        ClearSlots();                                                            // 기존 슬롯 제거

        foreach (var saveData in saveDataList)                                   // 저장된 캐릭터 데이터 각각 확인
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);  // 저장된 ID를 통해 SO 변환

            if (character == null)                                              // ID에 맞는 캐릭터가 없으면 스킵
            {
                Debug.Log($"[SlotSpawner] ID {saveData.ID}에 해당하는 캐릭터를 찾을 수 없음");
                continue;
            }

            Debug.Log($"[슬롯 생성] {character.Grade} 등급의 프리팹으로 슬롯 생성 중...");

            GameObject prefab = GetSlotPrefabByGrade(character.Grade);  // 희귀도에 맞는 프리팹 선택
            GameObject slotObj = Instantiate(prefab, slotParent);       // 슬롯 생성 및 배치

            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();
            slot.SetSlot(saveData, character);                          // 슬롯에 데이터 적용

            spawnedSlots.Add(slotObj);                                  // 생성 슬롯 리스트에 추가
        }
    }

    public void SpawnFromFilteredCharacters(List<Character> filteredList)       // 필터/정렬된 Character 리스트 기반으로 생성
    {
        ClearSlots();  // 기존 슬롯 제거


        List<CharacterSaveData> saveDataList = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);    // 저장된 캐릭터 세이브 데이터를 미리 가져오기

        foreach (Character character in filteredList)                       // 필터링된 캐릭터 리스트 확인
        {
            CharacterSaveData saveData = null;


            foreach (CharacterSaveData data in saveDataList)                //  캐릭터 ID에 해당하는 저장 데이터를 찾기
            {
                if (data.ID == character.ID)
                {
                    saveData = data;
                    break;                                                  // 매칭되면 루프 종료
                }
            }


            GameObject prefab = GetSlotPrefabByGrade(character.Grade);      // 슬롯 프리팹 선택
            GameObject slotObj = Instantiate(prefab, slotParent);           // 슬롯 생성

            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();


            if (saveData != null)                                          // 저장 데이터가 있으면 함께 전달, 없으면 기본 캐릭터만
            {
                slot.SetSlot(saveData, character);
            }
            else
            {
                // slot.SetSlot(character);                            // 저장 데이터가 없을 경우 캐릭터만 설정
            }

            spawnedSlots.Add(slotObj); // 생성된 슬롯 저장
        }
    }
    public void ClearSlots()                                                     // 생성된 모든 슬롯 제거
    {
        foreach (var obj in spawnedSlots)
        {
            Destroy(obj);                                                   // 슬롯 오브젝트 삭제
        }
        spawnedSlots.Clear();                                               // 리스트 초기화
    }

    private int EnumToIndex(Grade grade)                                   // Grade(enum)를 int로 변환
    {
        return (int)grade;
    }

    private GameObject GetSlotPrefabByGrade(Grade grade)                   // 등급에 따라 슬롯 프리팹 반환
    {
        int index = EnumToIndex(grade);                                    // enum을 인덱스로 변환

        if (index < 0 || index >= slotPrefabs.Length)                     // 배열 범위를 벗어나면
        {
            Debug.Log($"[슬롯 프리팹 오류] {grade}에 맞는 슬롯이 없어서 기본 슬롯을 반환.");
            return slotPrefabs[0];                                        // 기본슬롯 변환
        }

        return slotPrefabs[index];                                       // 정상 범위면 해당 인덱스의 슬롯 프리팹 반환
    }

    public void LoadAllCharacters()
    {
        Character[] allCharacters = Resources.LoadAll<Character>("Char");
        if (allCharacters.Length > 0)
        {
            Debug.Log($"[캐릭터 자동 로드] 총 {allCharacters.Length}개 캐릭터 불러옴");

            ClearSlots();                                                               // 기존 슬롯 초기화

            foreach (var character in allCharacters)
            {
                // 저장 데이터가 있는 경우 가져오기

                CharacterSaveData saveData = SaveDataBase.Instance
                    .GetSaveInstanceList<CharacterSaveData>(SaveType.Character)
                    .Find(data => data.ID == character.ID);

                // 슬롯 생성

                GameObject prefab = GetSlotPrefabByGrade(character.Grade);               // 희귀도에 따라 프리팹 선택
                GameObject slotObj = Instantiate(prefab, slotParent);                   // 슬롯 생성 및 부모 설정

                CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();

                if (saveData != null)
                    slot.SetSlot(saveData, character);                                  // 저장된 데이터가 있으면 적용

                spawnedSlots.Add(slotObj);                                              // 추적 리스트에 추가
            }
        }
        else
        {
            Debug.Log("저장된 캐릭터가 없습니다.");
        }
    }

    //public void LoadAllCharacters(List<Character> allCharacters)
    //{
    //    if (allCharacters.Count > 0)
    //    {
    //        Debug.Log($"[캐릭터 자동 로드] 총 {allCharacters.Count}개 캐릭터 불러옴");

    //        ClearSlots();                                                               // 기존 슬롯 초기화

    //        foreach (var character in allCharacters)
    //        {
    //            // 저장 데이터가 있는 경우 가져오기

    //            CharacterSaveData saveData = SaveDataBase.Instance
    //                .GetSaveInstanceList<CharacterSaveData>(SaveType.Character)
    //                .Find(data => data.ID == character.ID);

    //            // 슬롯 생성

    //            GameObject prefab = GetSlotPrefabByGrade(character.Grade);               // 희귀도에 따라 프리팹 선택
    //            GameObject slotObj = Instantiate(prefab, slotParent);                   // 슬롯 생성 및 부모 설정

    //            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();

    //            if (saveData != null)
    //                slot.SetSlot(saveData, character);                                  // 저장된 데이터가 있으면 적용

    //            spawnedSlots.Add(slotObj);                                              // 추적 리스트에 추가
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("저장된 캐릭터가 없습니다.");
    //    }
    //}

    public void CharacterDecide()                                                           //내가 가진 캐릭터 중 장착된 캐릭터만 characterSaves 리스트에 추가
    {
        List<CharacterSaveData> characterSaveDatas = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);  // 저장된 모든 캐릭터 세이브 데이터 불러오기

        foreach (CharacterSaveData characterSaveData in characterSaveDatas)                 // 모든 세이브 데이터를 하나하나 확인
        {
            if (characterSaveData.IsEquiped)                                                // 장착된 캐릭터인지 확인
            {
                characterSave.Add(characterSaveData);                                      // 장착된 캐릭터만 리스트에 저장
            }
        }
    }

    public List<Character> characters(List<CharacterSaveData> characterSaveDatas)   //CharacterSaveData 리스트를 CharacterScriptObject로 바꿈
    {
        List<Character> characters = new List<Character>();                         // 변환된 캐릭터 데이터를 저장할 리스트

        foreach (CharacterSaveData characterSaveData in characterSaveDatas)         // 받은 세이브 데이터를 하나씩 순회하면서
        {
            characters.Add(GlobalDataTable.Instance.character.GetCharToID(characterSaveData.ID));   // ID를 기준으로 캐릭터 데이터 테이블에서 ScriptableObject를 찾아 추가
        }
        return characters;                                                                          // 완성된 리스트 반환
    }

}
