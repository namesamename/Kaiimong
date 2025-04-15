using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;
using System.Linq;

public enum SaveType
{

    //저장목록들
    Character,
    Currency,
}
public static class GameSaveSystem
{
    public static void Save( SaveType saveType ,List<SaveInstance> saves)
    {
        try
        {
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
            string json = JsonConvert.SerializeObject(new SaveDataWrapper { Saves = saves }, Formatting.Indented
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

   
  

    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }




}
