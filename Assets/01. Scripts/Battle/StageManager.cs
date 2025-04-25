using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;

    [Header("Stage Info")] // ����â���� �޾ƿ� ������
    public Stage CurrentStage;
    public List<CharacterCarrier> Players;
    public List<CharacterCarrier> Enemies;
    public int CurrentRound;
    public int CurrentRoundEnemyCount;

    [Header("Reward and Returns")]
    public float returnActivityPoints;
    public int userExp;
    public int playerExp;

    [Header("StageUI")]
    public WinUI WinUI;
    public LoseUI LoseUI;

    public Action OnWin;
    public Action OnLose;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        SceneLoader.Instance.RegisterSceneAction(SceneState.BattleScene, SetBattleScene);
    }

    void Start()
    {
    }

    private void SetBattleScene() //SceneLoader���� �ε� Ȯ�� �� setbattlescene
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
        CreateEnemy();
        battleSystem.Players = new List<CharacterCarrier>(Players);
        CurrentRound = 1;
        //��ȯ �ൿ��
        returnActivityPoints = CurrentStage.ActivityPoint * 0.9f;
        //�÷��̾� ����ġ
        userExp = CurrentStage.ActivityPoint * 100;
        //ĳ���� ����ġ
        playerExp = CurrentStage.ActivityPoint * 100 / 4;
    }

    //CurrentStage.EnemiesID�� �� ��ü ���� �� ����Ʈ���ϱ�
    private void CreateEnemy()
    {
        for (int i = 0; i < CurrentStage.EnemiesID.Length; i++)
        {
            GameObject enemy = GlobalDataTable.Instance.character.CharacterSummonToIDandLevel(CurrentStage.EnemiesID[i], CurrentStage.EnemyLevel);
            Enemies.Add(enemy.GetComponent<CharacterCarrier>());
        }
    }

    public void StageStart()
    {
        if (CurrentRound <= CurrentStage.Rounds)
        {
            if (CurrentRound == 1)
            {
                CurrentRoundEnemyCount = CurrentStage.EnemyCount[CurrentRound - 1];
                battleSystem.Enemies = new List<CharacterCarrier>(Enemies.GetRange(0, CurrentRoundEnemyCount));
                battleSystem.StartBattle();
            }
            else
            {
                int nextRoundEnemyCount = CurrentStage.EnemyCount[CurrentRound - 1];
                battleSystem.Enemies = new List<CharacterCarrier>(Enemies.GetRange(CurrentRoundEnemyCount, nextRoundEnemyCount));
                CurrentRoundEnemyCount = nextRoundEnemyCount;
                battleSystem.SetBattle();
            }
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
        StartCoroutine(BeforeSceneChangeDelay(WinUI.CanClick));
    }

    private void OnStageWin()
    {
        //����ġ, ���,������ȭ ����
        CurrencyManager.Instance.SetCurrency(CurrencyType.UserEXP, userExp);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, CurrentStage.Gold);
        CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, CurrentStage.Dia);
        //������
        //ĳ���� ����ġ?
        //ȣ���� ����

        //������������ ������Ʈ
        SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, CurrentStage.ID).ClearedStage = true;
        //�������� ����
        SaveDataBase.Instance.SaveSingleData(SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, CurrentStage.ID));
        for (int i = 0; i < CurrentStage.UnlockID.Length; i++)
        {
            SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, CurrentStage.UnlockID[i]).StageOpen = true;
            //�رݵ� �������� ���� ����
            SaveDataBase.Instance.SaveSingleData(SaveDataBase.Instance.GetSaveDataToID<StageSaveData>(SaveType.Stage, CurrentStage.UnlockID[i]));
        }
        for (int i = 0; i < CurrentStage.UnlockChapterID.Length; i++)
        {
            SaveDataBase.Instance.GetSaveDataToID<ChapterSaveData>(SaveType.Chapter, CurrentStage.UnlockChapterID[i]).ChapterOpen = true;
            //�رݵ� é�� ����
            SaveDataBase.Instance.SaveSingleData(SaveDataBase.Instance.GetSaveDataToID<ChapterSaveData>(SaveType.Chapter, CurrentStage.UnlockChapterID[i]));
        }

    }

    public void LoseStage()
    {
        LoseUI.gameObject.SetActive(true);
        OnLose?.Invoke();
        OnStageLose();
        StartCoroutine(BeforeSceneChangeDelay(LoseUI.CanClick));
    }

    private void OnStageLose()
    {
        //returnActivityPoints ��ȯ�ϱ�
        CurrencyManager.Instance.SetCurrency(CurrencyType.Activity, (int)returnActivityPoints);
    }

    private IEnumerator BeforeSceneChangeDelay(bool canClick)
    {
        yield return new WaitForSeconds(1f);

        canClick = true;
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
