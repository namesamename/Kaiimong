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
        //UI세팅 (속도순 정렬 이후 행동대기 유닛 아이콘, 스킬 아이콘 세팅)
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
}
