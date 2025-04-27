
using UnityEngine;

public enum Grade
{
    S,
    A,
    B
}

public enum CharacterAttackType
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
    public CharacterAttackType CharacterType;
    public int CharacterItem;
    public Sprite AttributeIcon;


}
