using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActionState : IBattleState
{
    private BattleSystem battleSystem;

    public BattleActionState(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    public void OnEnter()
    {
        battleSystem.CurBattleState = BattleState.Action;
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

}
