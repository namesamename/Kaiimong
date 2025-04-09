    
public enum BattleState
{
    Start = 0,
    PlayerTurn = 1,
    EnemyTurn = 2,
    Action = 3,
    Win = 4,
    Lose = 5,
}

//상태 인터페이스
public interface IBattleState
{
    void OnEnter();
    void OnExit();
    void OnUpdate();
}

//상태 머신
public class BattleStateMachine 
{
    private BattleSystem battleSystem;
    public IBattleState CurrentState;

    //생성자
    public BattleStateMachine(BattleSystem battleSystem)
    {
        this.battleSystem = battleSystem;
    }

    public void ChangeState(IBattleState newState)
    {
        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState?.OnEnter();
    }
}
