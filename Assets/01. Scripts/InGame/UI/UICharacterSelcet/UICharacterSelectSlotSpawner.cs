
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UICharacterSelectSlotSpawner : MonoBehaviour
{


    private GameObject SlotPrefabs;
    private List<GameObject> spawnedSlots = new List<GameObject>(); // 생성된 슬롯들 추적


    public bool IsGradeOrder = false;
    public bool IsLevelOrder = false;  

    private void Awake()
    {
        SlotPrefabs = Resources.Load<GameObject>("CharacterSlot/CharacterSelectSlot");
    }


   
    public void GradeDescendingFilter(List<CharacterSaveData> saveDatas)
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

            List<Character> GradeSortList = CharacterList.OrderByDescending(item => item.Grade).
                ThenByDescending(Item => SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, Item.ID).Level).ToList();

            foreach (Character grade in GradeSortList)
            {
                var Characterdata = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, grade.ID);
                EndList.Add(Characterdata);
            }
            IsGradeOrder = !IsGradeOrder;
            SpawnFromSaveData(EndList);
        }
        else
        {
            Debug.Log("null");
        }
  
    }

    public void LevelFilter(List<CharacterSaveData> saveDatas)
    {

        if (saveDatas != null)
        {
            List<CharacterSaveData> GradeSortList = saveDatas.OrderBy(item => item.Level).ThenByDescending(item => GlobalDataTable.Instance.character.GetCharToID(item.ID).Grade).ToList();

            IsLevelOrder = !IsLevelOrder;
            SpawnFromSaveData(GradeSortList);
 
        }
        else
        {
            Debug.Log("null");
        }
    }

    public void  LevelDescendingFilter(List<CharacterSaveData> saveDatas)
    {

        if (saveDatas != null)
        {
            List<CharacterSaveData> GradeSortList = saveDatas.OrderByDescending(item => item.Level).ThenBy(item => GlobalDataTable.Instance.character.GetCharToID(item.ID).Grade).ToList();
            IsLevelOrder = !IsLevelOrder;
            SpawnFromSaveData(GradeSortList);

        }
        else
        {
            Debug.Log("null");
        }

    }


    public void GradeFilter(List<CharacterSaveData> saveDatas)
    {

        List<CharacterSaveData> EndList = new List<CharacterSaveData>();

        List<Character> CharacterList = new List<Character>();
        if (saveDatas != null)
        {
   
            foreach (CharacterSaveData Save in saveDatas)
            {

                var CharacterData = GlobalDataTable.Instance.character.GetCharToID(Save.ID);
                CharacterList.Add(CharacterData);
            }

            List<Character> GradeSortList = CharacterList.OrderBy(item => item.Grade).
                ThenByDescending(Item => SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, Item.ID).Level).ToList();

            foreach (Character grade in GradeSortList)
            {
                var Characterdata = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, grade.ID);
                EndList.Add(Characterdata);
            }
            IsGradeOrder = !IsGradeOrder;
            SpawnFromSaveData(EndList);
        }
        else
        {
            Debug.Log("null");
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

                continue;
            }

    

          
            GameObject slotObj = Instantiate(SlotPrefabs, transform);       // 슬롯 생성 및 배치

            slotObj.GetComponent<CharacterSelectSlot>().SetSlot(saveData, character);


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
