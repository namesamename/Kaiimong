using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
{
    StageClear = 0,
    UseCurrency = 1,
    LevelUp = 2,
    KillMonster = 3,
}

public enum RewardType
{
    Item = 0,
    Gold = 1,
    Ticket = 2,
}

[System.Serializable]
public class BaseQuest
{
    public string QuestName;
    public string QuestDescription;
    public QuestType QuestType;

    public int RequiredCount;
    public int CurrentCount;

    public bool IsComplete;
    public int Reward;
    public RewardType RewardType;
}
