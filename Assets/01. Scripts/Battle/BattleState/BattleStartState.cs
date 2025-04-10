using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStartState : IBattleState
{
    private BattleSystem battleSystem;
    private bool isAnimationComplete = false;

    public BattleStartState(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    public void OnEnter()
    {
        battleSystem.CurBattleState = BattleState.Start;
        battleSystem.SetBattle();
        float maxAnimationTime = CalculateMaxAnimationTime();
        battleSystem.StartCoroutine(AppearAnimTime(maxAnimationTime));
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        if (isAnimationComplete)
        {
            battleSystem.ChangeState(BattleState.PlayerTurn);
        }
    }

    private float CalculateMaxAnimationTime()
    {
        float maxAnimationTime = 0f;
        var activePlayers = battleSystem.GetActivePlayers();
        var activeEnemies = battleSystem.GetActiveEnemies();

        for (int i = 0; i < activePlayers.Count; i++)
        {
            float playerAnimTime = activePlayers[i].AppearAnimationLength;
            float enemyAnimTime = activeEnemies[i].AppearAnimationLength;
            
            float animTime = playerAnimTime > enemyAnimTime ? playerAnimTime : enemyAnimTime;
            maxAnimationTime = maxAnimationTime > animTime ? maxAnimationTime : animTime;
        }

        return maxAnimationTime;
    }

    private IEnumerator AppearAnimTime(float animationTime)
    {
        yield return null;
        yield return new WaitForSeconds(animationTime);
        isAnimationComplete = true;
    }
}
