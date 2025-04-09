using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using static CharacterDataBase;
public enum SaveType
{
    Character,
    Item,
}
public class GameSaveSystem : Singleton<GameSaveSystem>
{

    public readonly string CharacterKey = "Character";

    public Dictionary<int, List<SaveInstance>> SaveDic = new Dictionary<int, List<SaveInstance>>();

    
    public void SaveData(List<SaveInstance> saves)
    {
        if (saves[0].Savetype == SaveType.Character)
        {
            SaveDataToEnum(1, saves, CharacterKey);
        }
      
    }

    public void SaveDataToEnum(int index, List<SaveInstance> saves, string Key)
    {
        SaveDic[index] = saves;
        string json = JsonUtility.ToJson(new SaveDataWrapper { Saves = SaveDic[index] });
        PlayerPrefs.SetString(Key, json);
        PlayerPrefs.Save();
        
    }

    public List<SaveInstance> Load(int index, string Key)
    {
        if(PlayerPrefs.GetString(Key) == string.Empty)
        {
            Debug.LogError("Data Do not Exist");
        }

        List<SaveInstance> saves = new List<SaveInstance>();
        saves = JsonUtility.FromJson<List<SaveInstance>>(PlayerPrefs.GetString(Key));
        return saves;
    }


  


    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }

}
