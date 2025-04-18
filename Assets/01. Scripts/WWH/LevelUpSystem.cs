using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class LevelUpSystem
{
    static List<int> PlayerNeedExp = new List<int>();
    static List<int> needExp = new List<int>(); // �� ������ �ʿ��� ����ġ ���� �ƴ�
    static List<int> needGold = new List<int>();
    static List<int> needamulet = new List<int>();
    static int[] MaxLevel = new int[4] { 20, 40, 60, 90 };
    private static void Init()
    {
        for (int i = 1; i <= 90; i++)
        {
            PlayerNeedExp.Add(i);
            needExp.Add(i);
            needGold.Add(i);
            needamulet.Add(i);
        }
    }
    public static void GainEXP(int EXP, CharacterCarrier character)
    {

        if(EXP <= 0 )
        {
            return;
        }
        character.CharacterSaveData.CumExp += EXP;
        LevelUpCheck(character);

    }


    public static void BattleLevelup(int EXP, CharacterCarrier character)
    {
        if (EXP <= 0)
        {
            return;
        }

        int NextNeedEXP = 0;
        for (int i = 0; i < character.CharacterSaveData.Level; i++)
        {
            NextNeedEXP += needExp[i];
        }

        character.CharacterSaveData.CumExp += EXP;

        if (NextNeedEXP < character.CharacterSaveData.CumExp)
        {
            character.CharacterSaveData.CumExp -= EXP;
            LevelUp(true, character);
        }

    }

    public static void LevelUpCheck(CharacterCarrier character)
    {

        int NextNeedEXP = 0;
        int NextNeedGold = 0;
        int NextNeedAMulet = 0;
        for (int i = 0; i < character.CharacterSaveData.Level; i++)
        {
            NextNeedEXP += needExp[i];
            NextNeedGold += needGold[i];
            NextNeedAMulet += needamulet[i];
        }
        LevelUp(IsLevelUpOk(character, NextNeedEXP, NextNeedGold, NextNeedAMulet), character);
    }

    public static bool IsLevelUpOk(CharacterCarrier character, int EXP, int Gold, int Amulet)
    {
        if (character.CharacterSaveData.Level < MaxLevel[character.CharacterSaveData.Level])
        {
            return false;
        }
        if (CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP) < Amulet)
        {
            return false;
        }
        if (character.CharacterSaveData.CumExp < EXP)
        {
            return false;
        }
        if (CurrencyManager.Instance.GetCurrency(CurrencyType.Gold) < Gold)
        {
            return false;
        }
        else
        {
            character.CharacterSaveData.CumExp -= EXP;
            CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -Gold);
            CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -Amulet);
            return true;
        }
    }

    public static void LevelUp(bool Ok, CharacterCarrier character)
    {
        if (Ok)
        {
            character.CharacterSaveData.Level += 1;
            //ĳ���� ���ο��� ������ ���� �������� �޼ҵ� �־����
            character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);
        }
    }


    public static void LevelUpMax(CharacterCarrier character)
    {

        int maxLevelForCurrentStage = MaxLevel[character.CharacterSaveData.Necessity];


        if (character.CharacterSaveData.Level >= maxLevelForCurrentStage)
        {
            Debug.Log($"�̹� ���� ���� �ܰ��� �ִ� ����({maxLevelForCurrentStage})�� �����߽��ϴ�.");
            return;
        }

        int currentLevel = character.CharacterSaveData.Level;
        int availableExp = character.CharacterSaveData.CumExp;
        int availableGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        int availableAmulet = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);

        int oldLevel = currentLevel;

  
        for (int targetLevel = currentLevel + 1; targetLevel <= maxLevelForCurrentStage; targetLevel++)
        {
            int expNeeded = needExp[targetLevel - 1];
            int goldNeeded = needGold[targetLevel - 1];
            int amuletNeeded = needamulet[targetLevel - 1];

 
            if (availableExp < expNeeded || availableGold < goldNeeded || availableAmulet < amuletNeeded)
            {
                break;
            }


            availableExp -= expNeeded;
            availableGold -= goldNeeded;
            availableAmulet -= amuletNeeded;


            currentLevel = targetLevel;
        }

        // ������ �������� �̷�������� ĳ���� ���� ������Ʈ
        if (currentLevel > oldLevel)
        {
            // ����� �ڿ��� ���
            int expUsed = character.CharacterSaveData.CumExp - availableExp;
            int goldUsed = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold) - availableGold;
            int amuletUsed = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP) - availableAmulet;

            // �ڿ� ����
            character.CharacterSaveData.CumExp -= availableExp;
            CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -availableGold);
            CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -availableAmulet);

            // ���� ����
            int levelsGained = currentLevel - oldLevel;
            character.CharacterSaveData.Level = currentLevel;

            // ���� ������Ʈ
            character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);

            // ������ �̺�Ʈ ȣ��
            Debug.Log($"���� {oldLevel}���� {currentLevel}�� {levelsGained}���� ���! (�Ҹ�: ����ġ {expUsed}, ��� {goldUsed}, ���� {amuletUsed})");
        }
        else
        {
            Debug.Log("�������� �ʿ��� �ڿ��� �����մϴ�.");
        }
    }



}
