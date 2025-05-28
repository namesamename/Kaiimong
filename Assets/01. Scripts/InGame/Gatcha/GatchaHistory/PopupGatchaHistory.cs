using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PopupGatchaHistory : UIPOPUP
{
    [SerializeField] private Transform contentRoot;
    [SerializeField] private GameObject itemPrefab;

    [SerializeField] GameObject NoticePanel;
    [SerializeField] GameObject HistoryPanel;
    [SerializeField] GameObject EntryPanel;

    private void Start()
    {
        ShowHistoryItems();
        NoticePanel.SetActive(true);
        HistoryPanel.SetActive(false);
    }
    private void ShowHistoryItems()
    {
        List<GatchaHistoryEntry> entries = GatchaHistoryManager.Instance.GetHistory();

        // ���� ������ ����
        foreach (Transform child in contentRoot)
        {
            Destroy(child.gameObject);
        }

        int count = Mathf.Min(entries.Count, 30);
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
        EntryPanel.SetActive(false);

    }
    public void PanelSettingHistory()
    {
        NoticePanel.SetActive(false);
        HistoryPanel.SetActive(true);
        EntryPanel.SetActive(false);
    }

    public void PanelSettingEntry()
    {
        NoticePanel.SetActive(false);
        HistoryPanel.SetActive(false);
        EntryPanel.SetActive(true);
    }

}
