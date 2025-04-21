
public enum Grade
{
    S,
    A,
    B
}

public enum CharacterType
{
    Spirit,
    Physics
}


public class Character : SO
{
    public string Name;
    public Grade Grade;
    public int Health;
    public int Attack;
    public int Defence;
    public int Speed;
    public float CriticalPer;
    public float CriticalAttack;
    public CharacterType CharacterType;
    public int CharacterItem;


}
