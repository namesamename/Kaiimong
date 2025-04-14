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

            //��θ� ��� /���� �̸��� ������
            string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");
            string directory = Path.GetDirectoryName(path);
            
            //��ο� ������ ���������ʴ� ��� ������ ����
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log($"���� ������: {directory}");
            }

            //����
            string json = JsonConvert.SerializeObject(new SaveDataWrapper { Saves = SaveDic[saveType] }, Formatting.Indented
                , new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto });

            //������ �����.
            File.WriteAllText(path, json);
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
        
    }

    public static List<SaveInstance> Load(SaveType saveType)
    {
        //��θ� ã�ƿ�
        string path = Path.Combine(Application.dataPath, $"JsonData/{saveType}/{saveType}Database.json");

        //������ �ִٸ�
        if(File.Exists(path))
        {
            //��� �б�
            string json =  File.ReadAllText(path);
            //���ο� ���̺� �ν��Ͻ� ����Ʈ �����
            List<SaveInstance> saves = new List<SaveInstance>();
            //������ �����´�
            SaveDataWrapper saveData = JsonConvert.DeserializeObject<SaveDataWrapper>
            (json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto});

            //������ ������.
            saves = saveData.Saves;
            return saves;
        }
        else
        {
            //������ ��� ������
           return new List<SaveInstance>();
        } 
    }

   
    public static void SaveAll()
    {
        //��ųʸ��� �ִ� �͵� ��� ����
        foreach(List<SaveInstance> saves in SaveDic.Values)
        {
            SaveDatas(saves);
        }
    }


    public static List<List<SaveInstance>> LoadAll()
    {
        //���̺긮��Ʈ�� ���� ����Ʈ�� ����
        List<List<SaveInstance>> SaveList = new List<List<SaveInstance>>();

        //�̳� Ÿ�Կ� ���� Ǯ��
        foreach(SaveType saveType in Enum.GetValues(typeof(SaveType)))
        {
            //�� Ÿ�Ը��� ���� �ε带 ��
            List<SaveInstance> saves = Load(saveType);

            //1�� �̻��� ��� ��ųʸ��� �����͸� �߰���
            if(saves.Count > 0)
            {
                SaveDic[saveType] = saves;
            }
            //�׸��� �ٽ� ����
            SaveList.Add(saves);
        }
        //����Ʈ�� Ǯ���
        return SaveList;
    }

    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }




}
