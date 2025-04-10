using System;
using System.IO;
using UnityEngine;


public class CharacterManager :Singleton<CharacterManager>
{



    public CharacterInstance instance;

    string path = Path.Combine(Application.dataPath, "database.json");

    private void Awake()
    {
        Save();
    }
    private void Start()
    {

   
    }
    public void Initalize()
    {       
       


    }


    public void Save()
    {
        string json = JsonUtility.ToJson(instance);
 
        try
        {
            File.WriteAllText(path, json);
            Debug.Log("파일 저장 완료!");
        }
        catch (Exception e)
        {
            Debug.LogError("파일 저장 실패: " + e.Message);
        }
    }


  






}
