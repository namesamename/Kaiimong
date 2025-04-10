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
        battleSystem.CommandController.newTurn?.Invoke();
    }

    public void OnExit()
    {
        //UI ²ô±â
        battleSystem.BattleUI.CharacterUI.DIsableUI();
    }

    public void OnUpdate()
    {

    }
}
