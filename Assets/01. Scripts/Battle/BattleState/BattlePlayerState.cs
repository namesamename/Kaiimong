using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerState : IBattleState
{
    private BattleSystem battleSystem;

    public BattlePlayerState(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    public void OnEnter()
    {
        battleSystem.CurBattleState = BattleState.PlayerTurn;
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
