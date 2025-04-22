using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class UICharacterFilter : MonoBehaviour                  //���� ��� ����
{
     public List<CharacterType> selectedAttributes = new();             // ���õ� �Ӽ� ����Ʈ
     public List<Grade> selectedGrades = new();                         // ���õ� ��͵� ����Ʈ




    // ĳ���Ͱ� ���� ���� ���ǿ� �´��� Ȯ��

    public List<Character> Characters(List<Character> allCharacter)               // ���͵� ĳ���͸� ������ ����Ʈ ����
    {
        List<Character> filteredCharacters = new List<Character>();
        foreach (Character character in allCharacter)                             // ��� ĳ���͸� �ϳ��� Ȯ��
        {
             if (selectedAttributes.Contains(character.CharacterType) &&            // �Ӽ��� ����� ���õ� ���� ���ǿ� ���ԵǴ���
              selectedGrades.Contains(character.Grade))                         // ĳ������ ����� ���õ� ���� ���ǿ� ���ԵǴ��� Ȯ��
            {
                filteredCharacters.Add(character);                               // ���� ������ ����Ʈ�� �߰�
            }
        }
        return filteredCharacters;
    }

    public void OnClickApplyFilter()  // ��ư�� �����ؼ� ����� �Լ�
    {
        // ����� ��� ĳ���� ���̺� ������ ��������
        List<CharacterSaveData> saveDataList = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);

        
        // isEquip�� true�� ĳ���͸� ���͸�

        List<CharacterSaveData> equippedData = new List<CharacterSaveData>();   // ���� ĳ���� ����� ����Ʈ

        foreach (CharacterSaveData data in saveDataList)                        // ��� ���� �����͸� ��ȸ
        {
            if (data.IsEquiped)                                                 // ���� ���� Ȯ��
            {
                equippedData.Add(data);                                         // ������ ĳ���͸� �߰�
            }
        }

        // ScriptableObject ��� Character�� ��ȯ

        List<Character> allCharacters = new();
        foreach (var saveData in equippedData)
        {
            Character character = GlobalDataTable.Instance.character.GetCharToID(saveData.ID);
            if (character != null)
                allCharacters.Add(character);
        }

        // �Ӽ� + ��� ���� ����
        List<Character> filteredCharacters = Characters(allCharacters);  // ���� ���ǿ� �´� ĳ���� ����

        
        // ���� ��ũ��Ʈ�� �ִٸ� ���� (��: ��͵� ����/����)

        UICharacterSort sorter = FindObjectOfType<UICharacterSort>();
        if (sorter != null)
        {
 //           filteredCharacters = sorter.CharacterDecide(filteredCharacters);
        }

        // ���� ����
        UICharacterSlotSpawner spawner = FindObjectOfType<UICharacterSlotSpawner>();
        if (spawner != null)
        {
//            spawner.SpawnFromSaveData(filteredCharacters);
        }
    }
}
