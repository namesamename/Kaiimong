using System;

[System.Serializable]
public class SaveInstance 
{
    public SaveType Savetype;
    public int ID;
}
[System.Serializable]
public class CharacterSaveData : SaveInstance
{
    public int Level;
    public int Recognition;
    public int Necessity;
    public bool IsEquiped;
    public float Love;
    public int CumEXP;
}
[System.Serializable]
public class CurrencySaveData : SaveInstance
{

    public string UserName = "JIHwan";
    public int purchaseCount;
    public int UserLevel;
    public int UserEXP;
    public int CurrentStaminaMax;
    public int CharacterEXP;
    public int GoldValue;
    public int DIAValue;
    public int GachaValue;
    public int ActivityValue;
    public DateTime date;

}

public class StageSaveData : SaveInstance
{
    public bool ClearedStage;
    public bool StageOpen;
}
public class ChapterSaveData : SaveInstance
{
    public bool ChapterOpen;
}

public class ItemSavaData : SaveInstance
{
    public int Value;
}

public class QuestSaveData : SaveInstance
{
    public int CurValue;
    public QuestType QuestType;
    public bool IsComplete;
    public bool IsCan;
}

public class TimeSaveData : SaveInstance
{
    public DateTime lastDailyReset;
    public DateTime lastWeeklyReset;
}

public class UICharacterIDSaveData : SaveInstance
{
    public int Lobby;
    public int FirstProfile;
    public int SecondProfile;
    public int ThirdProfile;
    public int FourthProfile;
    public string IntroduceText;
}




