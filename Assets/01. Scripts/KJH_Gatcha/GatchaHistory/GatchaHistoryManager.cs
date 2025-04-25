using System.Collections.Generic;
using UnityEngine;
using System.IO; // ���� ������� ���� using �߰�

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
            LoadHistoryFromFile(); //JSON ���Ͽ��� �ε� �õ�
            DontDestroyOnLoad(gameObject);
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

        SaveHistoryToFile(); //JSON ���Ϸ� ����
    }

    public List<GatchaHistoryEntry> GetHistory()
    {
        return new List<GatchaHistoryEntry>(historyList);
    }

    //JSON ����
    private void SaveHistoryToFile()
    {
        string json = JsonUtility.ToJson(new GatchaHistoryWrapper(historyList));
        string path = Path.Combine(Application.persistentDataPath, "gatcha_history.json");

        File.WriteAllText(path, json);
        Debug.Log($"[SAVE] Gatcha history saved to: {path}");
    }

    // JSON �ҷ�����
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
                Debug.Log($"[LOAD] Gatcha history loaded from: {path}");
            }
            else
            {
                Debug.LogWarning("[LOAD] JSON ������ �Ľ� ����!");
                historyList = new();
            }
        }
        else
        {
            Debug.LogWarning($"[LOAD] No history file found at: {path}");
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

