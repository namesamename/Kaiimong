
[System.Serializable]
public class Quest : SO
{
    public string QuestName;
    public string QuestDescription;
    public QuestType QuestType;

    public int RequiredCount;
    //public int CurrentCount;

    public bool IsComplete;

    public int RewardTableID;
}
