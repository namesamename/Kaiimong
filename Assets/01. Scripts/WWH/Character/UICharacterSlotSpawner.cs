using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;
// using static GatchaTable;

public class UICharacterSlotSpawner : MonoBehaviour
{


    GameObject SlotPrefabs;

    List<CharacterSaveData> HaveCharacters;      //캐릭터 목록

    private List<GameObject> spawnedSlots = new List<GameObject>(); // 생성된 슬롯들 추적

    List<CharacterSaveData> characterSave = new List<CharacterSaveData>();     //CharacterSaveData 타입 담은 리스트


    private void Awake()
    {
        SlotPrefabs = Resources.Load<GameObject>("CharacterSlot/CharacterSelectSlot");
    }

    private void Start()
    {
        SaveDataSet();
    }

  
    public void SaveDataSet()
    {

        List<CharacterSaveData> ownedSaves = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        foreach(CharacterSaveData data in ownedSaves)
        {
            if(data.IsEquiped)
            {
                HaveCharacters.Add(data);
            }
        }
    }



    public List<CharacterSaveData> GradeFilter(List<CharacterSaveData> saveDatas)
    {

        List<CharacterSaveData> EndList = new List<CharacterSaveData>();

        List<Character> CharacterList = new List<Character>();
        if (saveDatas != null)
        {
   
            foreach (CharacterSaveData item in saveDatas)
            {

                var CharacterData = GlobalDataTable.Instance.character.GetCharToID(item.ID);
                CharacterList.Add(CharacterData);
            }

            List<Character> GradeSortList = CharacterList.OrderBy(item => item.Grade).ToList();

            foreach (Character grade in GradeSortList)
            {
                var Characterdata = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, grade.ID);
                EndList.Add(Characterdata);
            }
          

            return EndList;
        }
        else
        {
            return new List<CharacterSaveData>();
        }
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

          
            GameObject slotObj = Instantiate(SlotPrefabs, transform);       // 슬롯 생성 및 배치

            slotObj.GetComponent<CharacterSlot>().SetSlot(saveData, character);


            spawnedSlots.Add(slotObj);                               
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


  


   
}
