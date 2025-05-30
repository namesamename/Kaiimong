using System.IO;
using UnityEngine;

public static class SaveGatchaSystem
{
    private static readonly string SavePath = Application.persistentDataPath + "/gatcha_save.json";

    public static void Save(GatchaSaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);
        Debug.Log($"[GatchaSaveSystem] 저장 완료: {SavePath}");
    }

    public static GatchaSaveData Load()
    {
        if (!File.Exists(SavePath))
            return new GatchaSaveData(); // 기본값 반환

        string json = File.ReadAllText(SavePath);
        return JsonUtility.FromJson<GatchaSaveData>(json);
    }
}
