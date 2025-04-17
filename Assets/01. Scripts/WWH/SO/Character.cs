
public enum Grade
{
    S,
    A,
    B
}

public class Character : SO
{
    public string Name;
    public Grade Grade;
    public int Health;
    public int Attack;
    public int Defense;
    public int Speed;
    public float CriticalPer;
    public float CriticalAttack;

    
    public bool IsEquiped = false;

}
