using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UICharacterSlotSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] slotPrefabs;              // ���� ������
    [SerializeField] private Transform slotParent;                  // ���Ե��� �� Content

    [SerializeField] private List<Character> defaultcharacters;      //ĳ���� ���

    private List<GameObject> spawnedSlots = new List<GameObject>(); // ������ ���Ե� ����

    List<CharacterSaveData> characterSave = new List<CharacterSaveData>();     //CharacterSaveData Ÿ�� ���� ����Ʈ



    private void Start()
    {        // 1. �ӽ÷� ������ ĳ���� ���̺� ������ ����
        CharacterSaveData testSave1 = new CharacterSaveData();
        testSave1.ID = 2;        // ĳ���� SO�� �ִ� ID�� �Է� (��: 1)
        testSave1.Level = 6;    // ���ϴ� ����
        testSave1.IsEquiped = true; // ���� ����

        // 2. ���� (SaveDataBase�� �߰�)
        SaveDataBase.Instance.SaveSingleData(testSave1);

        // 3. �� �ٸ� ĳ���͵� �߰� ����
        CharacterSaveData testSave2 = new CharacterSaveData();
        testSave2.ID = 4;
        testSave2.Level = 5;
        testSave2.IsEquiped = true;

        SaveDataBase.Instance.SaveSingleData(testSave2);

        CharacterSaveData testSave3 = new CharacterSaveData();
        testSave3.ID = 6;
        testSave3.Level = 7;
        testSave3.IsEquiped = false;

        SaveDataBase.Instance.SaveSingleData(testSave3);
        
        CharacterSaveData testSave4 = new CharacterSaveData();
        testSave4.ID = 8;
        testSave4.Level = 2;
        testSave4.IsEquiped = false;

        SaveDataBase.Instance.SaveSingleData(testSave4);

        Debug.Log("�׽�Ʈ�� ĳ���� SaveData 4�� ���� �Ϸ�!");

        ShowOwnedCharacters();
        //LoadAllCharacters();
        //LoadAllCharacters(characters(characterSave));
    }

    // �� ���� ĳ����(��í �̱� ������ SaveDataBase�� �����)�� ������
    public void ShowOwnedCharacters()
    {
        //  �����(���� ����) ĳ���� ���̺� ������ ���� ������
        List<CharacterSaveData> ownedSaves = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        //  (Character, SaveData) ������ ����
        List<(Character character, CharacterSaveData saveData)> ownedPairs = new List<(Character, CharacterSaveData)>();
        foreach (var save in ownedSaves)
        {
            Character so = GlobalDataTable.Instance.character.GetCharToID(save.ID);
            if (so != null)
            {
                ownedPairs.Add((so, save));
                Debug.Log($"[ShowOwnedCharacters] �� ���� ĳ����: {so.Name} (ID:{so.ID}) Lv.{save.Level} Grade:{so.Grade}");
            }
            else
            {
                Debug.LogWarning($"[ShowOwnedCharacters] ID {save.ID} ĳ���� SO�� ã�� �� ����!");
            }
        }

    }
    public void SpawnFromSaveData(List<CharacterSaveData> saveDataList)     // ����� ĳ���� ������ ������� ���� ����
    {
        ClearSlots();                                                            // ���� ���� ����

        foreach (var saveData in saveDataList)                                   // ����� ĳ���� ������ ���� Ȯ��
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);  // ����� ID�� ���� SO ��ȯ

            if (character == null)                                              // ID�� �´� ĳ���Ͱ� ������ ��ŵ
            {
                Debug.Log($"[SlotSpawner] ID {saveData.ID}�� �ش��ϴ� ĳ���͸� ã�� �� ����");
                continue;
            }

            Debug.Log($"[���� ����] {character.Grade} ����� ���������� ���� ���� ��...");

            GameObject prefab = GetSlotPrefabByGrade(character.Grade);  // ��͵��� �´� ������ ����
            GameObject slotObj = Instantiate(prefab, slotParent);       // ���� ���� �� ��ġ

            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();
            slot.SetSlot(saveData, character);                          // ���Կ� ������ ����

            spawnedSlots.Add(slotObj);                                  // ���� ���� ����Ʈ�� �߰�
        }
    }

    public void SpawnFromSortedList(List<(Character character, CharacterSaveData saveData)> sortedList)         // (Character, SaveData) �� ����Ʈ ���� ����
    {
        ClearSlots(); // ���� ���� ����

        foreach (var pair in sortedList)
        {
            Character character = pair.character;
            CharacterSaveData saveData = pair.saveData;

            GameObject prefab = GetSlotPrefabByGrade(character.Grade); // ��޺� ������ ����
            GameObject slotObj = Instantiate(prefab, slotParent);

            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();
            slot.SetSlot(saveData, character); // ���Կ� ������ ����

            spawnedSlots.Add(slotObj); // ����Ʈ�� �߰�
        }
        Debug.Log($"[SlotSpawner] {spawnedSlots.Count}�� ���� ���� �Ϸ� (�� ���� ĳ���͸�)");
    }

    public void SpawnFromFilteredCharacters(List<Character> filteredList)       // ����/���ĵ� Character ����Ʈ ������� ����
    {
        ClearSlots();  // ���� ���� ����


        List<CharacterSaveData> saveDataList = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);    // ����� ĳ���� ���̺� �����͸� �̸� ��������

        foreach (Character character in filteredList)                       // ���͸��� ĳ���� ����Ʈ Ȯ��
        {
            CharacterSaveData saveData = null;


            foreach (CharacterSaveData data in saveDataList)                //  ĳ���� ID�� �ش��ϴ� ���� �����͸� ã��
            {
                if (data.ID == character.ID)
                {
                    saveData = data;
                    break;                                                  // ��Ī�Ǹ� ���� ����
                }
            }


            GameObject prefab = GetSlotPrefabByGrade(character.Grade);      // ���� ������ ����
            GameObject slotObj = Instantiate(prefab, slotParent);           // ���� ����

            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();


            if (saveData != null)                                          // ���� �����Ͱ� ������ �Բ� ����, ������ �⺻ ĳ���͸�
            {
                slot.SetSlot(saveData, character);
            }
            else
            {
                // slot.SetSlot(character);                            // ���� �����Ͱ� ���� ��� ĳ���͸� ����
            }

            spawnedSlots.Add(slotObj); // ������ ���� ����
        }
    }
    public void ClearSlots()                                                     // ������ ��� ���� ����
    {
        foreach (var obj in spawnedSlots)
        {
            Destroy(obj);                                                   // ���� ������Ʈ ����
        }
        spawnedSlots.Clear();                                               // ����Ʈ �ʱ�ȭ
    }

    private int EnumToIndex(Grade grade)                                   // Grade(enum)�� int�� ��ȯ
    {
        return (int)grade;
    }

    private GameObject GetSlotPrefabByGrade(Grade grade)                   // ��޿� ���� ���� ������ ��ȯ
    {
        int index = EnumToIndex(grade);                                    // enum�� �ε����� ��ȯ

        if (index < 0 || index >= slotPrefabs.Length)                     // �迭 ������ �����
        {
            Debug.Log($"[���� ������ ����] {grade}�� �´� ������ ��� �⺻ ������ ��ȯ.");
            return slotPrefabs[0];                                        // �⺻���� ��ȯ
        }

        return slotPrefabs[index];                                       // ���� ������ �ش� �ε����� ���� ������ ��ȯ
    }

    public void LoadAllCharacters()
    {
        Character[] allCharacters = Resources.LoadAll<Character>("Char");
        if (allCharacters.Length > 0)
        {
            Debug.Log($"[ĳ���� �ڵ� �ε�] �� {allCharacters.Length}�� ĳ���� �ҷ���");

            ClearSlots();                                                               // ���� ���� �ʱ�ȭ

            foreach (var character in allCharacters)
            {
                // ���� �����Ͱ� �ִ� ��� ��������

                CharacterSaveData saveData = SaveDataBase.Instance
                    .GetSaveInstanceList<CharacterSaveData>(SaveType.Character)
                    .Find(data => data.ID == character.ID);

                // ���� ����

                GameObject prefab = GetSlotPrefabByGrade(character.Grade);               // ��͵��� ���� ������ ����
                GameObject slotObj = Instantiate(prefab, slotParent);                   // ���� ���� �� �θ� ����

                CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();

                if (saveData != null)
                {
                    slot.SetSlot(saveData, character); // ����� �����Ͱ� ������ ���Կ� ����
                    Debug.Log($"[���� ����] ����� ĳ����: {character.Name} (ID: {character.ID}) | Lv.{saveData.Level} | Grade: {character.Grade}");
                }
                else
                {
                    Debug.Log($"[���� ����] ������� ���� ĳ����: {character.Name} (ID: {character.ID}) | Grade: {character.Grade}");
                }

                spawnedSlots.Add(slotObj);
            }
            Debug.Log($"[�� ���� ��] {spawnedSlots.Count}�� ���� ���� �Ϸ�");
        }
        else
        {
            Debug.Log("����� ĳ���Ͱ� �����ϴ�.");
        }
    }

    //public void LoadAllCharacters(List<Character> allCharacters)
    //{
    //    if (allCharacters.Count > 0)
    //    {
    //        Debug.Log($"[ĳ���� �ڵ� �ε�] �� {allCharacters.Count}�� ĳ���� �ҷ���");

    //        ClearSlots();                                                               // ���� ���� �ʱ�ȭ

    //        foreach (var character in allCharacters)
    //        {
    //            // ���� �����Ͱ� �ִ� ��� ��������

    //            CharacterSaveData saveData = SaveDataBase.Instance
    //                .GetSaveInstanceList<CharacterSaveData>(SaveType.Character)
    //                .Find(data => data.ID == character.ID);

    //            // ���� ����

    //            GameObject prefab = GetSlotPrefabByGrade(character.Grade);               // ��͵��� ���� ������ ����
    //            GameObject slotObj = Instantiate(prefab, slotParent);                   // ���� ���� �� �θ� ����

    //            CharacterSlot slot = slotObj.GetComponent<CharacterSlot>();

    //            if (saveData != null)
    //                slot.SetSlot(saveData, character);                                  // ����� �����Ͱ� ������ ����

    //            spawnedSlots.Add(slotObj);                                              // ���� ����Ʈ�� �߰�
    //        }
    //    }
    //    else
    //    {
    //        Debug.Log("����� ĳ���Ͱ� �����ϴ�.");
    //    }
    //}


    public List<Character> characters(List<CharacterSaveData> characterSaveDatas)   //CharacterSaveData ����Ʈ�� CharacterScriptObject�� �ٲ�
    {
        List<Character> characters = new List<Character>();                         // ��ȯ�� ĳ���� �����͸� ������ ����Ʈ

        foreach (CharacterSaveData characterSaveData in characterSaveDatas)         // ���� ���̺� �����͸� �ϳ��� ��ȸ�ϸ鼭
        {
            characters.Add(GlobalDataTable.Instance.character.GetCharToID(characterSaveData.ID));   // ID�� �������� ĳ���� ������ ���̺����� ScriptableObject�� ã�� �߰�
        }
        return characters;                                                                          // �ϼ��� ����Ʈ ��ȯ
    }
   
}
