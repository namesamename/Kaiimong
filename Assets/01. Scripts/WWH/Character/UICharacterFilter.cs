using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class UICharacterFilter : MonoBehaviour                  //���� ��� ����

    
    
{
    // public List<> selectedAttributes = new();             // ���õ� �Ӽ� ����Ʈ
    // public List<Grade> selectedGrades = new();        // ���õ� ��͵� ����Ʈ




    // ĳ���Ͱ� ���� ���� ���ǿ� �´��� Ȯ��

    public List<Character> Characters(List<Character> allCharacter)
    {
        List<Character> filteredCharacters = new List<Character>();
        foreach (Character character in allCharacter)                   
        {
           // if (selectedAttributes.Contains(character.Attribute) &&             //�Ӽ��� ����� ���õ� ���� ���ǿ� ���Ե� ��츸 �߰�
           //  selectedGrades.Contains(character.Grade))
            {
                filteredCharacters.Add(character);                              // ���� ������ ����Ʈ�� �߰�
            }
        }
        return filteredCharacters;
    }

    /*ex���ݷ� 200�� ĳ���͸� �����ֱ�

    public List<Character> characters(List<Character> allCharacter) 
    {
        List<Character> attackcharacters = new List<Character>();
        foreach (Character @char in allCharacter) //ĳ���� �ȿ� ĳ���� �ڷ����� ���� ĳ����1�� ��θ� �����´�.
        {
            if (@char.Defense < 100)

            {
                attackcharacters.Add(@char);

            }
        }
        return attackcharacters;
    }*/

    public void OnClickApplyFilter()  // ��ư�� �����ؼ� ����� �Լ�
    {

    }
}
