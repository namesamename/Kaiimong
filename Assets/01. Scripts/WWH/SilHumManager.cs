using System.Collections.Generic;
using UnityEngine;

public class SilHumManager : MonoBehaviour
{
    void Start()
    {
        // Step 1: �׽�Ʈ�� ĳ���� ���� ������ ����
        var testData = new CharacterSaveData()
        {
            characterId = "Mika", // enum ���� string���� �־��ٰ� ����
            Level = 5,
            Recognition = 10,
            Necessity = 20,
            Savetype = SaveType.Character
        };

        // Step 2: CharacterDataBase�� �߰�
        SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character).Clear();
        SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character).Add(testData);

        // Step 3: SaveSystem���� ����
        GameSaveSystem.SaveData(new List<SaveInstance> { testData });

        Debug.Log("���� �Ϸ�!");

        // Step 4: ����� ������ �ҷ�����
        List<SaveInstance> loaded = new List<SaveInstance>();   
        loaded = GameSaveSystem.Load(SaveType.Character);

        // Step 5: �ҷ��� �����͸� ĳ���� �����ͷ� ĳ���� �� ���
        foreach (var item in loaded)
        {
            if (item is CharacterSaveData character)
            {
                Debug.Log($"�ҷ��� ĳ����: ID={character.characterId}, ����={character.Level}, ȣ����={character.Recognition}, �ʿ䵵={character.Necessity}");
            }
            else
            {
                Debug.Log(item.ToString());
                Debug.Log("Null");
            }
        }
    }
}
