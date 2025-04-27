using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;

    [Header("Stage Info")] // 선택창에서 받아온 데이터
    public Stage CurrentStage;
    public List<CharacterCarrier> Players;
    public List<CharacterCarrier> Enemies;
    public int CurrentRound;
    public int CurrentRoundEnemyCount; //없애도 될수도

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


    private void SetBattleScene() //SceneLoader에서 로드 확인 후 setbattlescene
    {
        GameObject obj = Instantiate(Resources.Load("Battle/BattleSystem")) as GameObject;
        battleSystem = obj.GetComponent<BattleSystem>();
        SetStageInfo();
        StageStart();
    }
    private void SetStageInfo()
    {
        GameObject background = Instantiate(Resources.Load("Battle/Background")) as GameObject;
        background.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(CurrentStage.BackgroundPath);
        battleSystem.Players = new List<CharacterCarrier>(Players);
        CurrentRound = 1;
        //반환 행동력
        returnActivityPoints = CurrentStage.ActivityPoint * 0.9f;
        //플레이어 경험치
        userExp = CurrentStage.ActivityPoint * 20;
        playerLove = CurrentStage.ActivityPoint;
    }

    //CurrentStage.EnemiesID로 적 객체 생성 후 리스트업하기
    private void CreateEnemy()
    {
        List<EnemySpawn> curEnemyList = new List<EnemySpawn>(GlobalDataTable.Instance.EnemySpawn.EnemySpawnDic[CurrentStage.ID]);
        foreach (EnemySpawn enemy in curEnemyList)
        {
            if (enemy.Round == CurrentRound)
            {
                Enemy spawnEnemy = GlobalDataTable.Instance.character.enemyDic[enemy.EnemyID];
                for (int i = 0; i < enemy.Count; i++)
                {
                    GameObject newEnemy = GlobalDataTable.Instance.character.EnemyInstanceSummon(spawnEnemy, enemy.Level, Vector3.zero);
                    Enemies.Add(newEnemy.GetComponent<CharacterCarrier>());
                }
            }
        }
    }

    public void StageStart()
    {
        CreateEnemy();
        if (CurrentRound <= CurrentStage.Rounds)
        {
            battleSystem.Enemies.Clear();
            battleSystem.Enemies = new List<CharacterCarrier>(Enemies);
            battleSystem.SetBattle();
            //if (CurrentRound == 1)
            //{
            //    CurrentRoundEnemyCount = CurrentStage.EnemyCount[CurrentRound - 1];
            //    battleSystem.Enemies = new List<CharacterCarrier>(Enemies.GetRange(0, CurrentRoundEnemyCount));
            //    battleSystem.StartBattle();
            //}
            //else
            //{
            //    int nextRoundEnemyCount = CurrentStage.EnemyCount[CurrentRound - 1];
            //    battleSystem.Enemies = new List<CharacterCarrier>(Enemies.GetRange(CurrentRoundEnemyCount, nextRoundEnemyCount));
            //    CurrentRoundEnemyCount = nextRoundEnemyCount;
            //    battleSystem.SetBattle();
            //}
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
        OnWin?.Invoke();
        OnStageWin();
        StartCoroutine(BeforeWinChangeDelay());
    }

    private void OnStageWin()
    {
        //경험치, 골드,유료재화 지급 
        CurrencyManager.Instance.SetCurrency(CurrencyType.UserEXP, userExp);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, CurrentStage.Gold);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, CurrentStage.Dia);
        //아이템
        //for(int i = 0; i < CurrentStage.ItemID.Length; i++)
        //{
        //    int itemID = CurrentStage.ItemID[i];
        //    ItemSavaData itemSaveData = ItemManager.Instance.GetItemSaveData(itemID);


        //}
        //호감도 지급      
        foreach (CharacterCarrier player in Players)
        {
            CharacterManager.Instance.CharacterSaveDic[player.GetID()].Love += playerLove;
            //player.CharacterSaveData.Love += playerLove;
            //저장
            CharacterManager.Instance.SaveSingleData(player.GetID());
            //player.SaveData();
        }
        //스테이지정보 업데이트
        ChapterManager.Instance.GetStageSaveData(CurrentStage.ID).ClearedStage = true;
        for (int i = 0; i < CurrentStage.UnlockID.Length; i++)
        {
            ChapterManager.Instance.GetStageSaveData(CurrentStage.UnlockID[i]).StageOpen = true;
        }
        for (int i = 0; i < CurrentStage.UnlockChapterID.Length; i++)
        {
            ChapterManager.Instance.GetChapterSaveData(CurrentStage.UnlockChapterID[i]).ChapterOpen = true;
        }
        //저장
        ChapterManager.Instance.SaveAllData();
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
        //returnActivityPoints 반환하기
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
        foreach (CharacterCarrier carrier in Players)
        {
            battleSystem.UnSubscribeCharacterDeathAction(carrier);
        }
        foreach (CharacterCarrier carrier in Enemies)
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
}
