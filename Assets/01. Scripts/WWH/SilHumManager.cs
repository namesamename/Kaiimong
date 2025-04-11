using System.Collections.Generic;
using UnityEngine;

public class SilHumManager : MonoBehaviour
{
    void Start()
    {
        // Step 1: 테스트용 캐릭터 저장 데이터 생성
        var testData = new CharacterSaveData()
        {
            characterId = "Mika", // enum 값을 string으로 넣었다고 가정
            Level = 5,
            Recognition = 10,
            Necessity = 20,
            Savetype = SaveType.Character
        };

        // Step 2: CharacterDataBase에 추가
        SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character).Clear();
        SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character).Add(testData);

        // Step 3: SaveSystem으로 저장
        GameSaveSystem.SaveData(new List<SaveInstance> { testData });

        Debug.Log("저장 완료!");

        // Step 4: 저장된 데이터 불러오기
        List<SaveInstance> loaded = new List<SaveInstance>();   
        loaded = GameSaveSystem.Load(SaveType.Character);

        // Step 5: 불러온 데이터를 캐릭터 데이터로 캐스팅 후 출력
        foreach (var item in loaded)
        {
            if (item is CharacterSaveData character)
            {
                Debug.Log($"불러온 캐릭터: ID={character.characterId}, 레벨={character.Level}, 호감도={character.Recognition}, 필요도={character.Necessity}");
            }
            else
            {
                Debug.Log(item.ToString());
                Debug.Log("Null");
            }
        }
    }
}
