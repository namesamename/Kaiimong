using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using static CharacterDataBase;

public enum SaveType
{
    Character,
    Item,
}
public class GameSaveSystem : Singleton<GameSaveSystem>
{

    

    public Dictionary<SaveType, List<SaveInstance>> SaveDic = new Dictionary<SaveType, List<SaveInstance>>();

    
    public void SaveData(List<SaveInstance> saves)
    {
        if (saves[0].Savetype == SaveType.Character)
        {
            SaveDataToEnum( SaveType.Character ,saves);
        }
      
    }

    public void SaveDataToEnum( SaveType saveType ,List<SaveInstance> saves)
    {
        SaveDic[saveType] = saves;

        string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");

        string json = JsonConvert.SerializeObject(new SaveDataWrapper { Saves = SaveDic[SaveType.Character] }, Formatting.Indented
            , new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

        File.WriteAllText(path, json);
        
    }

    public List<SaveInstance> Load(SaveType saveType)
    {
        string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");
        if(File.Exists(path))
        {
            string json =  File.ReadAllText(path);
            List<SaveInstance> saves = new List<SaveInstance>();
            SaveDataWrapper saveData = JsonConvert.DeserializeObject<SaveDataWrapper>
            (json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});
            saves = saveData.Saves;
            return saves;
        }
        else
        {
           return new List<SaveInstance>();
        } 
    }
    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }




}
