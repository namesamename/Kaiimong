using System.Collections;

[System.Serializable]
public class SaveInstance 
{
    public SaveType Savetype;
    public string ID;
}
[System.Serializable]
public class CharacterSaveData : SaveInstance
{
    public int Level;
    public int Recognition;
    public int Necessity;

}

[System.Serializable]
public class CurrencySaveData : SaveInstance
{
    public int GoldValue;
    public int DIAValue;
    public int GachaValue;
    public int ActivityValue;
}
