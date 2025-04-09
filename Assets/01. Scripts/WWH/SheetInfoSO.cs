using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static System.Net.WebRequestMethods;

[System.Serializable]
public class SheetData
{
    public string className;
    public string sheetId;
    public List<Dictionary<string, string>> datas;
}

[CreateAssetMenu(fileName = "New DataLoad", menuName ="DataLoad")]
public class SheetInfoSO : SOSingleton<SheetInfoSO>
{
    public Object outPath;
    public string OutPath { get => AssetDatabase.GetAssetPath(outPath); }
    public string url;
    public List<SheetData> datas;
}