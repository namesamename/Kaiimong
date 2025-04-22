using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using JetBrains.Annotations;

public class UICharacterSort : MonoBehaviour                   // ���� �ڵ�, ��͵� ����
{
    public bool isAscending = false;                           // ��͵� ���� ���� (false = ��������, true = ��������)

    [SerializeField] private TextMeshProUGUI sortButtonText;   // ��͵� ���� ��ư�� ǥ�õ� �ؽ�Ʈ

    List<CharacterSaveData> characterSaves = new List<CharacterSaveData>();     //CharacterSaveData Ÿ�� ���� ����Ʈ

    private void Start()
    {
        UpdateSortText();                                      // ���� �� ���� �ؽ�Ʈ ����
    }

    public void OnClickRaritySort()                            // ��͵� ���� ��ư Ŭ�� �� ����
    {
        isAscending = !isAscending;                            // ���� ��� (���� <-> ����)
        UpdateSortText();                                      // ��ư �ؽ�Ʈ ����
    }

    private void UpdateSortText()                              // ���� ��ư �ؽ�Ʈ ����
    {
        if (isAscending == true)                               // ���������� ��
        {
            sortButtonText.text = "��͵� ��";             // ���� �ؽ�Ʈ ǥ��
        }
        else                                                   // ���������� ��
        {
            sortButtonText.text = "��͵� ��";             // ���� �ؽ�Ʈ ǥ��
        }
    }

    public List<CharacterCarrier> SortByLevel(List<CharacterCarrier> levellist)                                // ���� �ڵ� ���� (��������)
    {
        return levellist.OrderByDescending(characterLevel => characterLevel.CharacterSaveData.Level).ToList(); // ���� ū ��
    }


    public List<Character> SortByRarity(List<Character> list)          // ��͵� ���� (��ư Ŭ�� �� ���� ����)
    {
        if (isAscending)                                                                        // ��������
            return list.OrderBy(characterUp => characterUp.Grade).ToList();                     // ���� ��� �� ���� ���
        else                                                                                    // ��������
            return list.OrderByDescending(characterDown => characterDown.Grade).ToList();       // ���� ��� �� ���� ��� 
    }

    public List<Character> characters(List<CharacterSaveData> characterSaveDatas)   //CharacterSaveData ����Ʈ�� CharacterScriptObject�� �ٲ�
    {
        List<Character> characters = new List<Character>();                         // ��ȯ�� ĳ���� �����͸� ������ ����Ʈ

        foreach (CharacterSaveData characterSaveData in characterSaveDatas)         // ���� ���̺� �����͸� �ϳ��� ��ȸ�ϸ鼭
        {
            characters.Add(GlobalDataTable.Instance.character.GetCharToID(characterSaveData.ID));   // ID�� �������� ĳ���� ������ ���̺��� ScriptableObject�� ã�� �߰�
        }
        return characters;                                                                          // �ϼ��� ����Ʈ ��ȯ
    }
}
