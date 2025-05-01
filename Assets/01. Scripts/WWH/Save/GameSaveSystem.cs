using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

public static class GameSaveSystem
{
    public static void Save( SaveType saveType ,List<SaveInstance> saves)
    {
        try
        {
            //��θ� ��� /���� �̸��� ������
            string path = Path.Combine(Application.persistentDataPath, $"JsonData/{saveType}/{saveType}Database.json");
            string directory = Path.GetDirectoryName(path);

     
            //��ο� ������ ���������ʴ� ��� ������ ����
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                Debug.Log($"���� ������: {directory}");
            }
            //����
            string json = JsonConvert.SerializeObject(new SaveDataWrapper { Saves = saves }, Formatting.Indented
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
        string path = Path.Combine(Application.persistentDataPath, $"JsonData/{saveType}/{saveType}Database.json");


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

    public static void Delete(SaveType saveType)
    {
        try
        {
            string path = Path.Combine(Application.persistentDataPath, $"JsonData/{saveType}/{saveType}Database.json");
            if (File.Exists(path))
            {
                File.Delete(path);

            }
            else
            {
                Debug.Log($"������ ������ �������� �ʽ��ϴ�: {path}");
            }
        }
        catch (Exception exception)
        {
            Debug.LogException(exception);
        }
    }


    [System.Serializable]
    public class SaveDataWrapper
    {
        public List<SaveInstance> Saves = new List<SaveInstance>();
    }




}
