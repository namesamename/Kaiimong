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
        battleSystem.GetActivePlayers().Sort((a,b) => b.Speed.CompareTo(a.Speed));        
        battleSystem.OnPlayerTurn?.Invoke();
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {

    }

    void PlayerCommand()
    {
        foreach (var player in battleSystem.GetActivePlayers())
        {
            bool pickSkill = false;
            while (!pickSkill)
            {

            }
        }
    }

    void PlayerTurn()
    {
        bool pickSKill =false;
        while (!pickSKill)
        {

        }
    }

}
