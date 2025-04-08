using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    [Header("UnitLocation")]
    [SerializeField] private List<Transform> playerLocations;
    [SerializeField] private List<Transform> enemyLocations;
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform enemyParent;

    [Header("StateTiming")]
    [SerializeField] private float timeBetweenStates;

    [Header("StateMachine")]
    public BattleState CurBattleState;
    private BattleStateMachine battleStateMachine;
    private IBattleState[] stateArray;

    [Header("Units")]
    public List<DummyUnit> players; //캐릭터 선택에서 가져오고
    public List<DummyUnit> enemies; //스테이지 데이터에서 가져오고
    [SerializeField] private List<DummyUnit> activePlayers = new List<DummyUnit>();    //현재 배치중인 유닛들 정보
    [SerializeField] private List<DummyUnit> activeEnemies = new List<DummyUnit>();
    public List<DummyUnit> GetActivePlayers() => activePlayers;
    public List<DummyUnit> GetActiveEnemies() => activeEnemies;

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

        for (int i = 0; i < players.Count; i++)
        {
            players[i].Speed = i + 1;
            enemies[i].Speed = i + 1;
            
            DummyUnit playerUnit = Instantiate(players[i], playerLocations[i].position, Quaternion.identity, playerParent);
            DummyUnit enemyUnit = Instantiate(enemies[i], enemyLocations[i].position, Quaternion.identity, enemyParent);

            enemyUnit.transform.rotation = Quaternion.Euler(0, 180, 0);
            
            activePlayers.Add(playerUnit);
            activeEnemies.Add(enemyUnit);
        }
    }
}
