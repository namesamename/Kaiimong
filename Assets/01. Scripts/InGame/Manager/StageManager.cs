using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;
    public BattleCamera BattleCamera;

    public BattleSystem BattleSystem { get { return battleSystem; } }

    [Header("Stage Info")]
    public Stage CurrentStage;
    public List<Character> Players;
    public List<Enemy> Enemies;
    public int CurrentRound;
    public int CurrentTurn;
    public int CurrentSet;
    public int RewardGold;
    public int RewardExpPotion;
    public int RewardDia;

    [Header("Reward and Returns")]
    public float returnActivityPoints;
    public int userExp;
    public int playerLove;

    [Header("StageUI")]
    public WinUI WinUI;
    public LoseUI LoseUI;

    public Action OnWin;
    public Action OnLose;

    private bool finishedStage = false;

    public void Initialize()
    {
        SceneLoader.Instance.RegisterSceneAction(SceneState.BattleScene, SetBattleScene);
    }

    private void SetBattleScene() //SceneLoader에서 로드 확인 후 setbattlescene
    {
        // CurrentStage가 null이면 리턴
        if (CurrentStage == null)
        {
            Debug.LogWarning("CurrentStage is null. Cannot set battle scene.");
            return;
        }

        // 씬에 있는 모든 BattleSystem 찾기
        BattleSystem[] existingBattleSystems = FindObjectsOfType<BattleSystem>();
        foreach (BattleSystem existingSystem in existingBattleSystems)
        {
            Destroy(existingSystem.gameObject);
        }
        battleSystem = null;

        // Resources 폴더에서 BattleSystem 프리팹을 찾아서 생성
        GameObject obj = Instantiate(Resources.Load("Battle/BattleSystem")) as GameObject;
        BattleSystem cursystem = obj.GetComponent<BattleSystem>();
        battleSystem = cursystem;

        // 새로운 배틀시스템에 플레이어 데이터 설정
        if (Players != null && Players.Count > 0)
        {
            battleSystem.Players = new List<Character>(Players);
        }

        finishedStage = false;
        SetStageInfo();
        StageStart();
    }

    public void BringBattleSystem(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    private void SetStageInfo()
    {
        // CurrentStage가 null이면 리턴
        if (CurrentStage == null)
        {
            Debug.LogWarning("CurrentStage is null. Cannot set stage info.");
            return;
        }

        // 기존 배경이 있는지 확인하고 제거
        GameObject[] existingBackground = GameObject.FindGameObjectsWithTag("Background");
        foreach (GameObject backgrounds in existingBackground)
        {
            Destroy(backgrounds.gameObject);
        }

        // 새로운 배경 생성
        GameObject background = Instantiate(Resources.Load("Battle/Background")) as GameObject;
        background.name = "Background";

        CurrentRound = 1;
        CurrentTurn = 0;
        CurrentSet = 1;
        returnActivityPoints = CurrentStage.ActivityPoint * 1f;
        userExp = CurrentStage.ActivityPoint * 10;
        playerLove = CurrentStage.ActivityPoint;
        RewardExpPotion = CurrentStage.Potion;
        RewardGold = CurrentStage.Gold;
        RewardDia = CurrentStage.Dia;
    }

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
        if (WinUI != null)
        {
            Destroy(WinUI.gameObject);
            WinUI = null;
        }
        if (LoseUI != null)
        {
            Destroy(LoseUI.gameObject);
            LoseUI = null;
        }
        GameObject canvas = GameObject.Find("Canvas");
        GameObject uiwinPrefab = Instantiate(Resources.Load("UI/Battle/WinUI"), canvas.transform) as GameObject;
        GameObject uilosePrefab = Instantiate(Resources.Load("UI/Battle/LoseUI"), canvas.transform) as GameObject;
        WinUI = uiwinPrefab.GetComponent<WinUI>();
        LoseUI = uilosePrefab.GetComponent<LoseUI>();
    }

    public void WinStage()
    {
        if (!finishedStage)
        {
            finishedStage = true;
            WinUI.gameObject.SetActive(true);
            OnStageWin();
            OnWin?.Invoke();
            //StageWinExp();
            StartCoroutine(BeforeWinChangeDelay());
        }
    }

    private void OnStageWin()
    {
        bool targetOne = false;
        bool targetTwo = false;

        CheckExtraTarget(targetOne, targetTwo);

        RewardDia = CurrentStage.Dia;
        RewardGold = targetOne ? RewardGold : RewardGold / 2;
        RewardExpPotion = targetTwo ? RewardExpPotion : RewardExpPotion /2;

        if (RewardDia > 50)
        {
            CurrencyManager.Instance.SetCurrency(CurrencyType.Dia, RewardDia);
        }

        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, RewardGold);
        CurrencyManager.Instance.SetCurrency(CurrencyType.CharacterEXP, RewardExpPotion);
        RewardListUp();
        //for(int i = 0; i < CurrentStage.ItemID.Length; i++)
        //{
        //    int itemID = CurrentStage.ItemID[i];
        //    ItemSavaData itemSaveData = ItemManager.Instance.GetItemSaveData(itemID);
        //}
        foreach (Character player in Players)
        {
            CharacterSaveData save = SaveDataBase.Instance.GetSaveDataToID<CharacterSaveData>(SaveType.Character, player.ID);
            save.Love += playerLove;
            SaveDataBase.Instance.SaveSingleData(save);


            //CharacterManager.Instance.CharacterSaveDic[player.ID].Love += playerLove;
            ////player.CharacterSaveData.Love += playerLove;
            //CharacterManager.Instance.SaveSingleData(player.ID);
            ////player.SaveData();
        }
        if (!ChapterManager.Instance.GetStageSaveData(CurrentStage.ID).ClearedStage)
        {
            if (RewardDia == 50)
            {
                CurrencyManager.Instance.SetCurrency(CurrencyType.Dia, RewardDia);
            }
            ChapterManager.Instance.GetStageSaveData(CurrentStage.ID).ClearedStage = true;
        }
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

    public void CheckExtraTarget(bool targetOne, bool targetTwo)
    {
        targetOne = CurrentTurn <= 8 ? true : false;
        targetTwo = battleSystem.GetActivePlayers().Count == 4 ? true : false;
    }

    private void StageWinExp()
    {
        LevelUpSystem.GainPlayerEXP(userExp);
        //CurrencyManager.Instance.SetCurrency(CurrencyType.UserEXP, userExp);
    }

    public void LoseStage()
    {
        if (!finishedStage)
        {
            finishedStage = true;
            LoseUI.gameObject.SetActive(true);

            OnLose?.Invoke();
            OnStageLose();
            StartCoroutine(BeforeLoseChangeDelay());
        }
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
        WinUI = null;
        LoseUI = null;
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
                if (rewardCount > 0)
                {
                    WinUI.SetRewardSlot(item.ID, rewardCount);
                    ItemManager.Instance.SetitemCount(item.ID, rewardCount);
                }
            }
        }
    }
}
