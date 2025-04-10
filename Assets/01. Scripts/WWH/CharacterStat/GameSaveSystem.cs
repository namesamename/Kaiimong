using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public enum SaveType
{
    Character,
    Item,
}
public class GameSaveSystem : Singleton<GameSaveSystem>
{
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
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
        try
        {
            SaveDic[saveType] = saves;
            string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log($"Æú´õ »ý¼ºµÊ: {directory}");
            }

            string json = JsonConvert.SerializeObject(new SaveDataWrapper { Saves = SaveDic[saveType] }, Formatting.Indented
                , new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            File.WriteAllText(path, json);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
        
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

   
    public void SaveAll()
    {
        foreach(List<SaveInstance> saves in SaveDic.Values)
        {
            SaveData(saves);
        }
    }


    public List<List<SaveInstance>> LoadAll()
    {
        List<List<SaveInstance>> SaveList = new List<List<SaveInstance>>();


        foreach(SaveType saveType in Enum.GetValues(typeof(SaveType)))
        {
            List<SaveInstance> saves = Load(saveType);
            if(saves.Count > 0)
            {
                SaveDic[saveType] = saves;
            }
            SaveList.Add(saves);
        }
     
        return SaveList;
    }

    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }




}
