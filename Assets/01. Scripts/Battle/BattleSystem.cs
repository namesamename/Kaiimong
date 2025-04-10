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

    [Header("StateTiming")]
    [SerializeField] private float timeBetweenStates;

    [Header("StateMachine")]
    public BattleState CurBattleState;
    private BattleStateMachine battleStateMachine;
    private IBattleState[] stateArray;

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
    public bool CanSelectTarget = false;
    public bool SelectedTarget = false;
    public bool CanPickSkill = false;

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
        InitializeStateMachine();   
    }

    void Update()
    {
        battleStateMachine.CurrentState.OnUpdate();
    }

    void InitializeStateMachine()
    {
        battleStateMachine = new BattleStateMachine(this);

        stateArray = new IBattleState[Enum.GetValues(typeof(BattleState)).Length];

        stateArray[(int)BattleState.Start] = new BattleStartState(this);
        stateArray[(int)BattleState.PlayerTurn] = new BattlePlayerState(this);
        stateArray[(int)BattleState.Action] = new BattleActionState(this);

        ChangeState(BattleState.Start);
    }

    private IBattleState GetStateInterface(BattleState state)
    {
        return stateArray[(int)state];
    }

    public void ChangeState(BattleState state)
    {
        battleStateMachine.ChangeState(GetStateInterface(state));
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
            enemy.Speed = i+ 1;

            if (enemyLocations[i].isOccupied) continue;

            DummyUnit enemyUnit = Instantiate(enemy, enemyLocations[i].transform);
            enemyLocations[i].isOccupied= true;
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

    public void SetTarget()
    {
        if(SelectedTarget)
        {
            CommandController.AddCommand(new DummySkill(activePlayers[TurnIndex], Targets, SelectedSkill));
            TurnIndex++;
            Targets.Clear();
        }

        if(TurnIndex == activePlayers.Count)
        {
            BattleUI.CharacterUI.SetActionButton();
        }
    }

    public void OnSkillSelected()
    {
        SelectedTarget = false;
        CanSelectTarget = true;
    }


}
