using System.Collections.Generic;
using UnityEngine;

public class SilHumManager : MonoBehaviour
{
    void Start()
    {
        // Step 1: �׽�Ʈ�� ĳ���� ���� ������ ����
        var testData = new CharacterDataBase.CharacterSaveData
        {
            characterId = "Mika", // enum ���� string���� �־��ٰ� ����
            Level = 5,
            Recognition = 10,
            Necessity = 20,
            Savetype = SaveType.Character
        };

        // Step 2: CharacterDataBase�� �߰�
        CharacterDataBase.Instance.SaveDatas.Clear();
        CharacterDataBase.Instance.SaveDatas.Add(testData);

        // Step 3: SaveSystem���� ����
        GameSaveSystem.Instance.SaveData(new List<SaveInstance> { testData });

        Debug.Log("���� �Ϸ�!");

        // Step 4: ����� ������ �ҷ�����
        List<SaveInstance> loaded = new List<SaveInstance>();   
        loaded = GameSaveSystem.Instance.Load(SaveType.Character);

        // Step 5: �ҷ��� �����͸� ĳ���� �����ͷ� ĳ���� �� ���
        foreach (var item in loaded)
        {
            if (item is CharacterDataBase.CharacterSaveData character)
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
