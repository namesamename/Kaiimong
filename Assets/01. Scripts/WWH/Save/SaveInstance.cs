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
    public float CurHp;
    public int SpecialMoveGauge;
}
[System.Serializable]
public class CurrencySaveData : SaveInstance
{
    public int UserLevel;
    public int UserEXP;
    public int CharacterEXP;
    public int GoldValue;
    public int DIAValue;
    public int GachaValue;
    public int ActivityValue;
}

public class StageSaveData : SaveInstance
{
    public bool StageCleared;
}
