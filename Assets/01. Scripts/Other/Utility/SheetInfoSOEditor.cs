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
                var path = instance.OutPath + "/" + Directory + FileName(type.ToString(), data["ID"]) + ".asset";
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
        AssetDatabase.CreateAsset(dt, instance.OutPath+"/"+ Directory+ FileName(type.ToString(),data["ID"]) + ".asset");
        return TSVParser.DicToSOData(type, dt, data);
    }
    public string FileName(string fileName, string ID)
    {
        if (fileName == "Character")
        {
            return "Character_" + ID;
        }
        else if (fileName == "CharacterDialogue")
        {
            return "CharacterDialogue_" + ID;
        }
        else if( fileName == "Quest")
        {
            return "Quest_" + ID;
        }
        else if(fileName == "QuestReward")
        {
            return "QuestReward_" + ID;
        }
        else if (fileName == "ActiveSkill")
        {
            return "ActiveSkill_" + ID;
        }
        else if (fileName == "Debuff")
        {
            return "Debuff_" + ID;
        }
        else if (fileName == "Buff")
        {
            return "Buff_" + ID;
        }
        else if (fileName == "ItemData")
        {
            return "Item_" + ID;
        }
        else if (fileName == "Consume")
        {
            return "ConsumeItem_" + ID;
        }
        else if (fileName == "Chapter")
        {
            return "Chapter_" + ID;
        }
        else if (fileName == "ChapterCategory")
        {
            return "ChapterCategory_" + ID;
        }
        else if (fileName == "Stage")
        {
            return "Stage_" + ID;
        }
        else if (fileName == "CharacterUpgradeTable")
        {
            return "CharacterUpgradeTable_" + ID;
        }
        else if (fileName == "Enemy")
        {
            return "Enemy_" + ID;
        }
        else if (fileName == "EnemySpawn")
        {
            return "EnemySpawn_" + ID;
        }
        else
        {
            return fileName;
        }
    }


    public void Pathcheck(string SheetId)
    {
  
        if (SheetId == "Character")
        {
            Directory = "CharacterSO/";
        }
        else if (SheetId == "ActiveSkill")
        {
            Directory = "ActiveSkill/";
        }
        else if (SheetId == "Debuff")
        {
            Directory = "Debuff/";
        }
        else if (SheetId == "Buff")
        {
            Directory = "Buff/";
        }
        else if (SheetId == "ItemData")
        {
            Directory = "Item/";
        }
        else if (SheetId == "CharacterDialogue")
        {
            Directory = "CharacterDialogue/";
        }
        else if (SheetId == "Consume")
        {
            Directory = "Consumable/";
        }
        else if (SheetId == "Chapter")
        {
            Directory = "Chapter/";
        }
        else if (SheetId == "ChapterCategory")
        {
            Directory = "ChapterCategory/";
        }
        else if (SheetId == "Stage")
        {
            Directory = "Stage/";
        }
        else if (SheetId == "CharacterUpgradeTable")
        {
            Directory = "RecongnitionSO/";
        }
        else if (SheetId == "Enemy")
        {
            Directory = "Enemy/";
        }
        else if (SheetId == "EnemySpawn")
        {
            Directory = "EnemySpawn/";
        }
        else if(SheetId == "Quest")
        {
            Directory = "Quest/";
        }
        else if (SheetId == "QuestReward")
        {
            Directory = "QuestReward/";
        }

    }


}
