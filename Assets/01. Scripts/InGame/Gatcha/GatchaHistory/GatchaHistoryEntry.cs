using System;
using static GatchaManager;

[System.Serializable]
public class GatchaHistoryEntry
{
    public string characterName;
    public Grade grade;
    public GatchaType gatchaType;
    public string timestamp;

    public GatchaHistoryEntry(string characterName, Grade grade, GatchaType gatchaType, string timestamp)
    {
        this.characterName = characterName;
        this.grade = grade;
        this.gatchaType = gatchaType;
        this.timestamp = timestamp;
    }
}