using System.Collections.Generic;
using UnityEngine;
using System.IO; // 파일 입출력을 위한 using 추가

public class GatchaHistoryManager : MonoBehaviour
{
    public static GatchaHistoryManager Instance;
    private const int MaxHistoryCount = 30;

    private List<GatchaHistoryEntry> historyList = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadHistoryFromFile(); //JSON 파일에서 로드 시도
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddEntry(GatchaHistoryEntry entry)
    {
        historyList.Add(entry);

        if (historyList.Count > MaxHistoryCount)
            historyList.RemoveAt(0);

        SaveHistoryToFile(); //JSON 파일로 저장
    }

    public List<GatchaHistoryEntry> GetHistory()
    {
        return new List<GatchaHistoryEntry>(historyList);
    }

    //JSON 저장
    private void SaveHistoryToFile()
    {
        string json = JsonUtility.ToJson(new GatchaHistoryWrapper(historyList));
        string path = Path.Combine(Application.persistentDataPath, "gatcha_history.json");

        File.WriteAllText(path, json);
    }

    // JSON 불러오기
    private void LoadHistoryFromFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "gatcha_history.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GatchaHistoryWrapper wrapper = JsonUtility.FromJson<GatchaHistoryWrapper>(json);
            if (wrapper != null && wrapper.entries != null)
            {
                historyList = wrapper.entries;
            }
            else
            {
                historyList = new();
            }
        }
        else
        {
            historyList = new();
        }
    }

    [System.Serializable]
    private class GatchaHistoryWrapper
    {
        public List<GatchaHistoryEntry> entries;

        public GatchaHistoryWrapper(List<GatchaHistoryEntry> entries)
        {
            this.entries = entries;
        }
    }
}

