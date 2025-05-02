using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;

    [Header("Stage Info")]
    public Stage CurrentStage;
    public List<Character> Players;
    public List<Enemy> Enemies;
    public int CurrentRound;
    public int CurrentSet;

    [Header("Reward and Returns")]
    public float returnActivityPoints;
    public int userExp;
    public int playerLove;

    [Header("StageUI")]
    public WinUI WinUI;
    public LoseUI LoseUI;

    public Action OnWin;
    public Action OnLose;

    public void Initialize()
    {
        SceneLoader.Instance.RegisterSceneAction(SceneState.BattleScene, SetBattleScene);
    }

    private void SetBattleScene() //SceneLoader���� �ε� Ȯ�� �� setbattlescene
    {
        // ���� battleSystem�� �ִ��� Ȯ��
        if (battleSystem != null)
        {
            // �̹� �����ϴ� battleSystem�� �ִٸ� ����
            Destroy(battleSystem.gameObject);
        }
        GameObject obj = Instantiate(Resources.Load("Battle/BattleSystem")) as GameObject;
        BattleSystem cursystem = obj.GetComponent<BattleSystem>();
        battleSystem = cursystem;
        battleSystem.Players = new List<Character>(Players);
        SetStageInfo();
        StageStart();
    }

    public void BringBattleSystem(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    private void SetStageInfo()
    {
        GameObject background = Instantiate(Resources.Load("Battle/Background")) as GameObject;
        //background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(CurrentStage.BackgroundPath);
        CurrentRound = 1;
        CurrentSet = 1;
        //��ȯ �ൿ��
        returnActivityPoints = CurrentStage.ActivityPoint * 0.9f;
        //�÷��̾� ����ġ
        userExp = CurrentStage.ActivityPoint * 20;
        playerLove = CurrentStage.ActivityPoint;
    }

    //CurrentStage.EnemiesID�� �� ��ü ���� �� ����Ʈ���ϱ�
    private void CreateEnemy()
    {
        List<EnemySpawn> curEnemyList = new List<EnemySpawn>(GlobalDataTable.Instance.EnemySpawn.EnemySpawnDic[CurrentStage.ID]);
        for (int i = 0; i < curEnemyList.Count; i++)
        {
            if (curEnemyList[i].Round == CurrentRound && curEnemyList[i].Set == CurrentSet)
            {
                for (int j = 0; j < curEnemyList[i].EnemyCount; j++)
                {
                    Enemy newEnemy = GlobalDataTable.Instance.character.GetEnemyToID(curEnemyList[i].EnemyID);
                    Enemies.Add(newEnemy);
                    Debug.Log(Enemies.Count);
                }
                CurrentSet++;
            }
        }
        CurrentSet = 1;
    }

    public void StageStart()
    {
        Enemies.Clear();
        CreateEnemy();
        if (CurrentRound <= CurrentStage.Rounds)
        {
            battleSystem.Enemies.Clear();
            battleSystem.Enemies = new List<Enemy>(Enemies);
            battleSystem.SetBattle();
        }
        else return;
    }
    public void EndUISet()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject uiwinPrefab = Instantiate(Resources.Load("UI/Battle/WinUI"), canvas.transform) as GameObject;
        GameObject uilosePrefab = Instantiate(Resources.Load("UI/Battle/LoseUI"), canvas.transform) as GameObject;
        WinUI = uiwinPrefab.GetComponent<WinUI>();
        LoseUI = uilosePrefab.GetComponent<LoseUI>();
    }

    public void WinStage()
    {
        WinUI.gameObject.SetActive(true);
        OnStageWin();
        OnWin?.Invoke();
        StartCoroutine(BeforeWinChangeDelay());
    }

    private void OnStageWin()
    {
        //����ġ, ���,������ȭ ���� 
        CurrencyManager.Instance.SetCurrency(CurrencyType.UserEXP, userExp);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, CurrentStage.Gold);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, CurrentStage.Dia);
        //������
        RewardListUp();
        //for(int i = 0; i < CurrentStage.ItemID.Length; i++)
        //{
        //    int itemID = CurrentStage.ItemID[i];
        //    ItemSavaData itemSaveData = ItemManager.Instance.GetItemSaveData(itemID);
        //}
        //ȣ���� ����      
        foreach (Character player in Players)
        {
            CharacterManager.Instance.CharacterSaveDic[player.ID].Love += playerLove;
            //player.CharacterSaveData.Love += playerLove;
            //����
            CharacterManager.Instance.SaveSingleData(player.ID);
            //player.SaveData();
        }
        //������������ ������Ʈ
        ChapterManager.Instance.GetStageSaveData(CurrentStage.ID).ClearedStage = true;
        ChapterManager.Instance.SaveStageSingleData(CurrentStage.ID);
        for (int i = 0; i < CurrentStage.UnlockStageID.Length; i++)
        {
            ChapterManager.Instance.GetStageSaveData(CurrentStage.UnlockStageID[i]).StageOpen = true;
            ChapterManager.Instance.SaveStageSingleData(CurrentStage.UnlockStageID[i]);
        }
        if (CurrentStage.UnlockChapterID != null)
        {
            for (int i = 0; i < CurrentStage.UnlockChapterID.Length; i++)
            {
                if (CurrentStage.UnlockChapterID[i] == -1) break;
                ChapterManager.Instance.GetChapterSaveData(CurrentStage.UnlockChapterID[i]).ChapterOpen = true;
                ChapterManager.Instance.SaveChapterSingleData(CurrentStage.UnlockChapterID[i]);
            }
        }
    }

    public void LoseStage()
    {
        LoseUI.gameObject.SetActive(true);
        OnLose?.Invoke();
        OnStageLose();
        StartCoroutine(BeforeLoseChangeDelay());
    }

    private void OnStageLose()
    {
        //returnActivityPoints ��ȯ�ϱ�
        CurrencyManager.Instance.SetCurrency(CurrencyType.Activity, (int)returnActivityPoints);
    }

    private IEnumerator BeforeLoseChangeDelay()
    {
        LoseUI.CanClick = false;
        yield return new WaitForSecondsRealtime(1f);
        LoseUI.CanClick = true;
    }

    private IEnumerator BeforeWinChangeDelay()
    {
        WinUI.CanClick = false;
        yield return new WaitForSecondsRealtime(1f);
        WinUI.CanClick = true;
    }

    private void UnSubscribeAllAction()
    {
        foreach (CharacterCarrier carrier in battleSystem.GetActivePlayers())
        {
            battleSystem.UnSubscribeCharacterDeathAction(carrier);
        }
        foreach (CharacterCarrier carrier in battleSystem.GetActiveEnemies())
        {
            battleSystem.UnSubscribeCharacterDeathAction(carrier);
        }
        battleSystem.UnSubscribeBattleSystem();
        WinUI.UnSubscribeWinUI();
        LoseUI.UnSubscribeLoseUI();
    }

    public void ToStageSelectScene()
    {
        UnSubscribeAllAction();
        ClearStageSetting();
        SceneLoader.Instance.ChangeScene(SceneState.StageSelectScene);
    }

    private void ClearStageSetting()
    {
        Players.Clear();
        Enemies.Clear();
        CurrentStage = null;
    }

    public void RewardListUp(int battleCount = 1)
    {
        if (ChapterManager.Instance.StageItemList.ContainsKey(CurrentStage.ID))
        {
            foreach (var item in ChapterManager.Instance.StageItemList[CurrentStage.ID])
            {
                int rewardCount = 0;
                for (int i = 0; i < battleCount; i++)
                {
                    int num = UnityEngine.Random.Range(0, 100);
                    if (num < item.Probability)
                    {
                        rewardCount++;
                    }

                }
                if(rewardCount > 0)
                {
                    WinUI.SetRewardSlot(item.ID, rewardCount);
                    ItemManager.Instance.SetitemCount(item.ID, rewardCount);
                }
            }
        }
    }
}
