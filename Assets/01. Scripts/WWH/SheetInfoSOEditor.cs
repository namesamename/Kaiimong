using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CustomEditor(typeof(SheetInfoSO))]
public class SheetInfoSOEditor : Editor
{
    string Directory;

    SheetInfoSO instance;
    public override void OnInspectorGUI()
    {
        instance = (SheetInfoSO)target;

        base.OnInspectorGUI();
     
        if (GUILayout.Button("Download Datas"))
        {
            
            DownloadData(instance.datas);
        }
    }


    public async void DownloadData(List<SheetData> sheets)
    {
        foreach (var sheet in sheets)
        {
            var url = $"{instance.url}/export?format=tsv&gid={sheet.sheetId}";
            var req = UnityWebRequest.Get(url);
            var op = req.SendWebRequest();
            await op;
            var res = req.downloadHandler.text; //얘를 Json으로 저장을 해
            Debug.Log(res);
            sheet.datas = TSVParser.TsvToDic(res);
        }
        ImportDatas(sheets);
    }

    protected void ImportDatas(List<SheetData> sheets)
    {
        foreach (var sheet in sheets)
        {
            ImportData(sheet);
        }
    }

    protected void ImportData(SheetData sheet)
    {
        Assembly assembly = typeof(SO).Assembly;
        var type = assembly.GetType(sheet.className);
        Pathcheck(sheet.className);
        GetDatas(type, sheet.datas);
    }

    public void GetDatas(Type type, List<Dictionary<string, string>> datas)
    {
            foreach (var data in datas)
            {
                var path = instance.OutPath + "/" + Directory + data["ID"] + ".asset";
                var dt = (ScriptableObject)AssetDatabase.LoadAssetAtPath(path, type);
                if (dt == null)
                {
                    dt = DicToClass(type, data);
                }
                else
                {
                    dt = TSVParser.DicToSOData(type, dt, data);
                }
                EditorUtility.SetDirty(dt);
                AssetDatabase.SaveAssets();
            }
    }

    public ScriptableObject DicToClass(Type type, Dictionary<string, string> data)
    {
        var dt = CreateInstance(type);
        AssetDatabase.CreateAsset(dt, instance.OutPath+"/"+ Directory+ data["ID"] + ".asset");
        return TSVParser.DicToSOData(type, dt, data);
    }


    public void Pathcheck(string SheetId)
    {
  
        if (SheetId == "Character")
        {
            Directory = "Char/";
        }
        else if (SheetId == "ActiveSkill")
        {
            Directory = "Skil/";
        }
        else if (SheetId == "Debuff")
        {
            Directory = "Debu/";
        }
        else if (SheetId == "Buff")
        {
            Directory = "Buff/";
        }
 
    }


}
