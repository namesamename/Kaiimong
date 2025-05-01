using System.Collections.Generic;

[System.Serializable]
public class GatchaHistoryWrapper
{
    public List<GatchaHistoryEntry> entries;

    public GatchaHistoryWrapper(List<GatchaHistoryEntry> entries)
    {
        this.entries = entries;
    }
}