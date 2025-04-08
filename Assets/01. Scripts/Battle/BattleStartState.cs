using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : IBattleState
{
    private BattleSystem battleSystem;

    public BattleStartState(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    public void OnEnter()
    {
        battleSystem.CurBattleState = BattleState.Start;
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
