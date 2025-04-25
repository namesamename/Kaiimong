using System.Collections;
public interface ISavable
{
    public void Save();
}
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
}
[System.Serializable]
public class CurrencySaveData : SaveInstance
{

    public string UserName = "JIHwan";
    public int UserLevel;
    public int UserEXP;
    public int CurrentStaminaMax;
    public int CharacterEXP;
    public int GoldValue;
    public int DIAValue;
    public int GachaValue;
    public int ActivityValue;
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

