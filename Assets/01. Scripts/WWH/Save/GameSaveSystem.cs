using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

public enum SaveType
{
    Character,
    Item,
}
public static class GameSaveSystem
{
   
    public static Dictionary<SaveType, List<SaveInstance>> SaveDic = new Dictionary<SaveType, List<SaveInstance>>();

    public static void SaveData(SaveInstance instance)
    {
        if (instance.Savetype == SaveType.Character)
        {
            SaveDic[SaveType.Character].Add(instance);
        }   
    }
    public static void SaveDatas(List<SaveInstance> saves)
    {
        if (saves[0].Savetype == SaveType.Character)
        {
            SaveDataToEnum( SaveType.Character ,saves);
        }
      
    }

    public static void SaveDataToEnum( SaveType saveType ,List<SaveInstance> saves)
    {
        try
        {
            if (!SaveDic.TryGetValue(saveType, out var list) || list == null)
            {
                SaveDic[saveType] = new List<SaveInstance>();
            }
            SaveDic[saveType] = saves;

            //경로를 계산 /폴더 이름을 가져옴
            string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");
            string directory = Path.GetDirectoryName(path);
            
            //경로에 폴더가 존재하지않는 경우 생성을 해줌
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log($"폴더 생성됨: {directory}");
            }

            //저장
            string json = JsonConvert.SerializeObject(new SaveDataWrapper { Saves = SaveDic[saveType] }, Formatting.Indented
                , new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            //파일을 만든다.
            File.WriteAllText(path, json);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
        
    }

    public static List<SaveInstance> Load(SaveType saveType)
    {
        //경로를 찾아옴
        string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");

        //파일이 있다면
        if(File.Exists(path))
        {
            //모두 읽기
            string json =  File.ReadAllText(path);
            //새로운 세이브 인스턴스 리스트 만들기
            List<SaveInstance> saves = new List<SaveInstance>();
            //정보를 가져온다
            SaveDataWrapper saveData = JsonConvert.DeserializeObject<SaveDataWrapper>
            (json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});

            //정보를 덮어씌운다.
            saves = saveData.Saves;
            return saves;
        }
        else
        {
            //없으면 빈거 내보냄
           return new List<SaveInstance>();
        } 
    }

   
    public static void SaveAll()
    {
        //딕셔너리에 있는 것들 모두 저장
        foreach(List<SaveInstance> saves in SaveDic.Values)
        {
            SaveDatas(saves);
        }
    }


    public static List<List<SaveInstance>> LoadAll()
    {
        //세이브리스트를 담은 리스트를 만듬
        List<List<SaveInstance>> SaveList = new List<List<SaveInstance>>();

        //이넘 타입에 따라서 풀게
        foreach(SaveType saveType in Enum.GetValues(typeof(SaveType)))
        {
            //각 타입마다 먼저 로드를 함
            List<SaveInstance> saves = Load(saveType);

            //1개 이상일 경우 딕셔너리에 데이터를 추가함
            if(saves.Count > 0)
            {
                SaveDic[saveType] = saves;
            }
            //그리고 다시 묶음
            SaveList.Add(saves);
        }
        //리스트를 풀어요
        return SaveList;
    }

    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }




}
