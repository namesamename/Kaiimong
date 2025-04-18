using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class LevelUpSystem
{
    static List<int> PlayerNeedExp = new List<int>();
    static List<int> needExp = new List<int>(); // 각 레벨에 필요한 경험치 누적 아님
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
            //캐릭터 내부에서 레벨에 따른 스탯조정 메소드 넣어놓기
            character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);
        }
    }


    public static void LevelUpMax(CharacterCarrier character)
    {

        int maxLevelForCurrentStage = MaxLevel[character.CharacterSaveData.Necessity];


        if (character.CharacterSaveData.Level >= maxLevelForCurrentStage)
        {
            Debug.Log($"이미 현재 인지 단계의 최대 레벨({maxLevelForCurrentStage})에 도달했습니다.");
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

        // 실제로 레벨업이 이루어졌으면 캐릭터 정보 업데이트
        if (currentLevel > oldLevel)
        {
            // 사용한 자원량 계산
            int expUsed = character.CharacterSaveData.CumExp - availableExp;
            int goldUsed = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold) - availableGold;
            int amuletUsed = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP) - availableAmulet;

            // 자원 차감
            character.CharacterSaveData.CumExp -= availableExp;
            CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -availableGold);
            CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -availableAmulet);

            // 레벨 설정
            int levelsGained = currentLevel - oldLevel;
            character.CharacterSaveData.Level = currentLevel;

            // 스탯 업데이트
            character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);

            // 레벨업 이벤트 호출
            Debug.Log($"레벨 {oldLevel}에서 {currentLevel}로 {levelsGained}레벨 상승! (소모: 경험치 {expUsed}, 골드 {goldUsed}, 부적 {amuletUsed})");
        }
        else
        {
            Debug.Log("레벨업에 필요한 자원이 부족합니다.");
        }
    }



}
