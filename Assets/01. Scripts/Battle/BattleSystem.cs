using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    [Header("UnitLocation")]
    [SerializeField] private List<UnitPlate> playerLocations;
    [SerializeField] private List<UnitPlate> enemyLocations;
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform enemyParent;

    [Header("Units")]
    public List<DummyUnit> Players; //캐릭터 선택에서 가져오고
    public List<DummyUnit> Enemies; //스테이지 데이터에서 가져오고
    [SerializeField] private List<DummyUnit> activePlayers = new List<DummyUnit>();    //현재 배치중인 유닛들 정보
    [SerializeField] private List<DummyUnit> activeEnemies = new List<DummyUnit>();
    public List<DummyUnit> GetActivePlayers() => activePlayers;
    public List<DummyUnit> GetActiveEnemies() => activeEnemies;

    [Header("BattleInfo")]
    public int TurnIndex = 0;
    public SkillData SelectedSkill;
    public List<DummyUnit> Targets;
    private float betweenPhaseTime;

    [Header("Appear")]
    private bool appearAnimComplete = false;

    [Header("Skill&TargetSelect")]
    public bool CanSelectTarget = false;
    public bool SelectedTarget = false;
    public bool PlayerTurn = false;

    [Header("AttackPhase")]
    public bool CanAttack = false;
    public bool AttackEnded = false;

    public CommandController CommandController { get; private set; }
    public BattleUI BattleUI;


    public Action OnPlayerTurn;
    public Action OnEnemyTurn;
    public Action SkillChanged;

    private void Awake()
    {
        CommandController = GetComponent<CommandController>();
        BattleUI.BattleSystem = this;
        BattleUI.CharacterUI.BattleSystem = this;
    }

    void Start()
    {
        StartBattle();
    }

    void Update()
    {
        if (appearAnimComplete)
        {
            appearAnimComplete = false;
            StartCoroutine(ChangePhase(PlayerTurnPhase));
        }

        if (CanAttack)
        {
            CanAttack = false;
            StartCoroutine(ChangePhase(AttackPhase));
        }
    }

    private void AttackPhase()
    {
        CommandController.ExecuteCommand();

        CheckGameOver();

        if (PlayerTurn)
        {
            StartCoroutine(ChangePhase(EnemyTurnPhase));
        }
        else
        {
            StartCoroutine(ChangePhase(PlayerTurnPhase));
        }
    }

    private void CheckGameOver()
    {
        if(Players.Count == 0 && activePlayers.Count == 0) StartCoroutine(ChangePhase(WinPhase));
        if(Enemies.Count == 0 && activeEnemies.Count == 0) StartCoroutine(ChangePhase(LosePhase));
    }

    private void StartBattle()
    {
        SetBattle();
        StartCoroutine(AppearAnimTime(GetMaxAnimationTime()));
    }

    private void EnemyTurnPhase()
    {

    }

    private void PlayerTurnPhase()
    {
        activePlayers.Sort((a, b) => b.Speed.CompareTo(a.Speed));
        OnPlayerTurn?.Invoke();
        CommandController.ClearList();
    }

    private void WinPhase()
    {

    }

    private void LosePhase()
    {

    }

    public void SetBattle()
    {
        activePlayers.Clear();
        activeEnemies.Clear();
        SetPlayer();
        SetEnemy();
    }

    private void SetPlayer()
    {
        List<DummyUnit> playerCopy = new List<DummyUnit>(Players);
        for (int i = 0; i < playerLocations.Count; i++)
        {
            DummyUnit player = playerCopy[i];
            player.Speed = i + 1;

            if (playerLocations[i].isOccupied) continue;

            DummyUnit playerUnit = Instantiate(player, playerLocations[i].transform);
            playerLocations[i].isOccupied = true;
            playerUnit.OnDeath += () => EmptyPlateOnUnitDeath(playerUnit);
            activePlayers.Add(playerUnit);
            Players.Remove(player);
        }
    }

    private void SetEnemy()
    {
        List<DummyUnit> enemiesCopy = new List<DummyUnit>(Enemies);
        for (int i = 0; i < enemyLocations.Count; i++)
        {
            DummyUnit enemy = enemiesCopy[i];
            enemy.Speed = i + 1;

            if (enemyLocations[i].isOccupied) continue;

            DummyUnit enemyUnit = Instantiate(enemy, enemyLocations[i].transform);
            enemyLocations[i].isOccupied = true;
            enemyUnit.transform.rotation = Quaternion.Euler(0, 180, 0);
            enemyUnit.OnDeath += () => EmptyPlateOnUnitDeath(enemyUnit);
            activeEnemies.Add(enemyUnit);
            Enemies.Remove(enemy);
        }
    }

    public void EmptyPlateOnUnitDeath(DummyUnit unit) //Unit OnDeath Action에 추가하기
    {
        unit.GetComponentInParent<UnitPlate>().isOccupied = false;
    }

    private float GetMaxAnimationTime()
    {
        float enemyAnimation = CaluculateMaxAnimationTime(activeEnemies);
        float playerAnimation = CaluculateMaxAnimationTime(activePlayers);

        float maxAnimationTime = enemyAnimation > playerAnimation ? enemyAnimation : playerAnimation;
        return maxAnimationTime;
    }

    private float CaluculateMaxAnimationTime(List<DummyUnit> unitList)
    {
        float maxAnimationTime = 0f;

        for (int i = 0; i < unitList.Count; i++)
        {
            float animTime = unitList[i].AppearAnimationLength;
            maxAnimationTime = maxAnimationTime > animTime ? maxAnimationTime : animTime;
        }

        return maxAnimationTime;
    }

    private IEnumerator AppearAnimTime(float animationTime)
    {
        yield return null;
        yield return new WaitForSeconds(animationTime);
        appearAnimComplete = true;
    }

    public void SetTarget()
    {
        if (SelectedTarget)
        {
            CommandController.AddCommand(new DummySkill(activePlayers[TurnIndex], Targets, SelectedSkill));
            TurnIndex++;
            Targets.Clear();
        }

        if (TurnIndex == activePlayers.Count)
        {
            BattleUI.CharacterUI.SetActionButton();
        }
    }

    public void OnSkillSelected()
    {
        SelectedTarget = false;
        CanSelectTarget = true;
    }

    private IEnumerator ChangePhase(Action nextPhase)
    {
        yield return new WaitForSeconds(betweenPhaseTime);

        nextPhase();
    }
}
