using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public static class LevelUpSystem
{
    public static List<int> PlayerNeedExp = new List<int>();
    public static List<int> needGold = new List<int>();
    public static List<int> needamulet = new List<int>();
    public static List<int> needLove = new List<int>();
    static int[] MaxLevel = new int[4] { 20, 40, 60, 90 };
    public static void Init()
    {
        for (int i = 1; i <= 90; i++)
        {
            PlayerNeedExp.Add(i);
            needGold.Add(i);
            needamulet.Add(i);
            needLove.Add(i);
        }
    }
    //public static void GainEXP(int EXP, CharacterCarrier character)
    //{

    //    if(EXP <= 0 )
    //    {
    //        return;
    //    }
    //    character.CharacterSaveData.CumExp += EXP;
    //    LevelUpCheck(character);

    //}
    //public static void BattleLevelup(int EXP, CharacterCarrier character)
    //{
    //    if (EXP <= 0)
    //    {
    //        return;
    //    }

    //    int NextNeedEXP = 0;
    //    for (int i = 0; i < character.CharacterSaveData.Level; i++)
    //    {
    //        NextNeedEXP += needExp[i];
    //    }

    //    character.CharacterSaveData.CumExp += EXP;

    //    if (NextNeedEXP < character.CharacterSaveData.CumExp)
    //    {
    //        character.CharacterSaveData.CumExp -= EXP;
    //        LevelUp(BattleLevelupOK(character), character);
    //    }

    //}
    //public static bool BattleLevelupOK(CharacterCarrier character)
    //{
    //    if (character.CharacterSaveData.Level == MaxLevel[character.CharacterSaveData.Level])
    //    {
    //        return false;
    //    }
    //    return true;
    //}


    public static void LevelUp( int Level,int UseGold, int UseAmulet, CharacterSaveData saveData)
    {
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -UseGold);
        CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -UseAmulet);
        saveData.Level += Level;
    }
    //public static void LevelUpCheck(CharacterCarrier character)
    //{

    //    int NextNeedEXP = 0;
    //    int NextNeedGold = 0;
    //    int NextNeedAMulet = 0;
    //    for (int i = 0; i < character.CharacterSaveData.Level; i++)
    //    {
    //        NextNeedEXP += needExp[i];
    //        NextNeedGold += needGold[i];
    //        NextNeedAMulet += needamulet[i];
    //    }
    //    LevelUp(IsLevelUpOk(character, NextNeedEXP, NextNeedGold, NextNeedAMulet), character);
    //}

    //public static bool IsLevelUpOk(CharacterCarrier character, int EXP, int Gold, int Amulet)
    //{
    //    if (character.CharacterSaveData.Level < MaxLevel[character.CharacterSaveData.Level])
    //    {
    //        return false;
    //    }
    //    if (CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP) < Amulet)
    //    {
    //        return false;
    //    }
    //    if (character.CharacterSaveData.CumExp < EXP)
    //    {
    //        return false;
    //    }
    //    if (CurrencyManager.Instance.GetCurrency(CurrencyType.Gold) < Gold)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        character.CharacterSaveData.CumExp -= EXP;
    //        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -Gold);
    //        CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -Amulet);
    //        return true;
    //    }
    //}

    //public static void LevelUp(bool Ok, CharacterCarrier character)
    //{
    //    if (Ok)
    //    {
    //        character.CharacterSaveData.Level += 1;
    //        character.CharacterSaveData.CumExp += needExp[character.CharacterSaveData.Level];
    //        //캐릭터 내부에서 레벨에 따른 스탯조정 메소드 넣어놓기
    //        character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);
    //    }
    //}


    //public static (int MaxLevel, int EXP, int Gold, int Amulet) OneLevelUPInfo(CharacterSaveData character)
    //{
    //    int MaxLevelinCurrentNece = MaxLevel[character.Necessity];
    //    if (character.Level == MaxLevelinCurrentNece)
    //    {
    //        Debug.Log($"이미 현재 인지 단계의 최대 레벨({MaxLevelinCurrentNece})에 도달했습니다.");
    //        return new(character.Level, 0, 0, 0);
    //    }
    //    int CurLevel = character.Level;
        
    //    int expNeeded = needExp[CurLevel];
    //    int goldNeeded = needGold[CurLevel];
    //    int amuletNeeded = needamulet[CurLevel];

    //    return (CurLevel + 1, expNeeded, goldNeeded, amuletNeeded);
    //}

    //public static (int MaxLevel,int EXP , int Gold, int Amulet) MaxLevelupInfo(CharacterCarrier character)
    //{
    //    int MaxLevelinCurrentNece = MaxLevel[character.CharacterSaveData.Necessity];
    //    if (character.CharacterSaveData.Level >= MaxLevelinCurrentNece)
    //    {
    //        Debug.Log($"이미 현재 인지 단계의 최대 레벨({MaxLevelinCurrentNece})에 도달했습니다.");
    //        return new(character.CharacterSaveData.Level, 0, 0, 0);
    //    }

    //    int CurGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
    //    int CurAmulet = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);
    //    int CurLevel = character.CharacterSaveData.Level;
    //    int CurExp = character.CharacterSaveData.CumExp;

    //    int TotalExpNeeded = 0;
    //    int TotalGoldNeeded = 0;
    //    int TotalAmuletNeeded = 0;
    //    int TargetLevel = CurLevel;

    //    for (int level = CurLevel + 1; level <= MaxLevelinCurrentNece; level++)
    //    {
    //        int expNeeded = needExp[level - 1];
    //        int goldNeeded = needGold[level - 1];
    //        int amuletNeeded = needamulet[level - 1];

    //        if (CurExp >= expNeeded && CurGold >= goldNeeded && CurAmulet >= amuletNeeded)
    //        {
    //            CurExp -= expNeeded;
    //            CurGold -= goldNeeded;
    //            CurAmulet -= amuletNeeded;

    //            TotalExpNeeded += expNeeded;
    //            TotalGoldNeeded += goldNeeded;
    //            TotalAmuletNeeded += amuletNeeded;

    //            TargetLevel = level;
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }

    //    return (TargetLevel, TotalExpNeeded, TotalGoldNeeded, TotalAmuletNeeded);
    //}

    //public static void LevelUpMax(CharacterCarrier character)
    //{
    //    int MaxLevelinCurrentNece = MaxLevel[character.CharacterSaveData.Necessity];

    //    if (character.CharacterSaveData.Level >= MaxLevelinCurrentNece)
    //    {
    //        Debug.Log($"이미 현재 인지 단계의 최대 레벨({MaxLevelinCurrentNece})에 도달했습니다.");
    //        return;
    //    }

    //    int CurLevel = character.CharacterSaveData.Level;
    //    int CurExp = character.CharacterSaveData.CumExp;
    //    int CurGold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
    //    int CurAmulet = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP);
    //    int oldLevel = CurLevel;
    //    for (int targetLevel = CurLevel + 1; targetLevel <= MaxLevelinCurrentNece; targetLevel++)
    //    {
    //        int expNeeded = needExp[targetLevel - 1];
    //        int goldNeeded = needGold[targetLevel - 1];
    //        int amuletNeeded = needamulet[targetLevel - 1];

 
    //        if (CurExp < expNeeded || CurGold < goldNeeded || CurAmulet < amuletNeeded)
    //        {
    //            break;
    //        }


    //        CurExp -= expNeeded;
    //        CurGold -= goldNeeded;
    //        CurAmulet -= amuletNeeded;


    //        CurLevel = targetLevel;
    //    }

    //    // 실제로 레벨업이 이루어졌으면 캐릭터 정보 업데이트
    //    if (CurLevel > oldLevel)
    //    {
    //        // 사용한 자원량 계산
    //        int ExpUsed = character.CharacterSaveData.CumExp - CurExp;
    //        int GoldUsed = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold) - CurGold;
    //        int AmuletUsed = CurrencyManager.Instance.GetCurrency(CurrencyType.CharacterEXP) - CurAmulet;

    //        // 자원 차감
    //        character.CharacterSaveData.CumExp = ExpUsed;
    //        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -GoldUsed);
    //        CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, -AmuletUsed);

    //        // 레벨 설정
    //        int levelsGained = CurLevel - oldLevel;
    //        character.CharacterSaveData.Level = CurLevel;

    //        // 스탯 업데이트
    //        character.SetstatToLevel(character.CharacterSaveData.Level, character.CharacterSaveData.ID);

    //        // 레벨업 이벤트 호출
    //        Debug.Log($"레벨 {oldLevel}에서 {CurLevel}로 {levelsGained}레벨 상승! (소모: 경험치 {ExpUsed}, 골드 {GoldUsed}, 부적 {AmuletUsed})");
    //    }
    //    else
    //    {
    //        Debug.Log("레벨업에 필요한 자원이 부족합니다.");
    //    }
    //}



}
