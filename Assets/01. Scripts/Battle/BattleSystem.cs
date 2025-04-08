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

    [Header("StateMachine")]
    public BattleState CurBattleState;
    private BattleStateMachine battleStateMachine;
    private IBattleState[] stateArray;

    [Header("Units")]
    public List<DummyUnit> players;
    public List<DummyUnit> enemies;


    void Start()
    {
        InitializeStateMachine();

        
        for(int i = 0; i < players.Count; i++)
        {
            players[i].Speed = i + 1;
            enemies[i].Speed = i + 1;
            Instantiate(players[i], playerLocations[i].position, Quaternion.identity, playerParent);
            Instantiate(enemies[i], enemyLocations[i].position, Quaternion.identity, enemyParent);
        }
    }

    void Update()
    {
        
    }

    //상태머신 초기 설정
    void InitializeStateMachine()
    {
        battleStateMachine = new BattleStateMachine(this);

        stateArray = new IBattleState[Enum.GetValues(typeof(BattleState)).Length];

        stateArray[(int)BattleState.Start] = new BattleStartState(this);

        CurBattleState = BattleState.Start;
        battleStateMachine.ChangeState(GetStateInterface(CurBattleState));
    }

    //상태머신 ChangeState 매개변수(IBattleState)로 return
    private IBattleState GetStateInterface(BattleState state)
    {
        return stateArray[(int)state];
    }
}
