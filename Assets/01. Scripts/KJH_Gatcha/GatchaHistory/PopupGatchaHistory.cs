using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupGatchaHistory : UIPopup
{
    [SerializeField] private Transform contentRoot;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] GameObject NoticePanel;
    [SerializeField] GameObject HistoryPanel;

    private void Start()
    {
        Debug.Log($"[DEBUG] itemPrefab 할당 여부: {(itemPrefab == null ? "NULL" : "OK")}");
        Debug.Log($"[DEBUG] contentRoot 할당 여부: {(contentRoot == null ? "NULL" : "OK")}");
        ShowHistoryItems();
        NoticePanel.SetActive(true);
        HistoryPanel.SetActive(false);
    }
    private void ShowHistoryItems()
    {
        List<GatchaHistoryEntry> entries = GatchaHistoryManager.Instance.GetHistory();

        // 기존 아이템 제거
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        int count = Mathf.Min(entries.Count, 30);
        Debug.Log("불러온 개수" + count);
        for (int i = count - 1; i >= 0; i--)
        {
            var entry = entries[i];
            GameObject go = Instantiate(itemPrefab, contentRoot);
            var ui = go.GetComponent<UIHistoryItem>();
            ui.Setup(entry);
        }
    }

    public void PanelSettingNotice()
    {
        NoticePanel.SetActive(true);
        HistoryPanel.SetActive(false);
    }
    public void PanelSettingHistory()
    {
        NoticePanel.SetActive(false);
        HistoryPanel.SetActive(true);
    }
}
