using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelUpSystem
{

    static List<int> needExp = new List<int>(); // �� ������ �ʿ��� ����ġ ���� �ƴ�
    static List<int> needGold = new List<int>();
    static List<int> needamulet = new List<int>();

    private static void Init()
    {
        for (int i = 0; i < 90; i++)
        {
            needExp.Add(i);
            needGold.Add(i);
            needamulet.Add(i);
        }
    }
    public static void GainEXP(int EXP, CharacterCarrier character)
    {
        character.CharacterSaveData.CumExp += EXP;
        LevelUpCheck(character);

    }

    public static void LevelUpCheck(CharacterCarrier character)
    {
        int NextNeedEXP  = 0;
        for (int i = 0; i < character.CharacterSaveData.Level; i++) 
        {
            NextNeedEXP += needExp[i];
        }

        if(character.CharacterSaveData.Necessity == 0)
        {
            //if(character.CharacterSaveData.Level)
        }
        else if(character.CharacterSaveData.Necessity ==1)
        {

        }
        else if(character.CharacterSaveData.Necessity == 2)
        {

        }
        else if(character.CharacterSaveData.Necessity == 3)
        {

        }



        //if(character.CharacterSaveData.CumExp > NextNeedEXP ) 
        //{
        //    character.CharacterSaveData.Level += 1;
        //    //ĳ���� ���ο��� ������ ���� �������� �޼ҵ� �־����
        //    character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);
        //}

    }

}
