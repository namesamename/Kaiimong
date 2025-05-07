
public class EnemyCarrier : CharacterCarrier
{
    public override void Initialize(int id, int level = -1)
    {
        SetID(id);
        visual.Initialize(id , CharacterType.Enemy);
        skillBook.SkillSet(GlobalDataTable.Instance.character.GetEnemyToID(id).SkillID, CharacterType.Enemy);
        stat.SetEnemy(GlobalDataTable.Instance.character.GetEnemyToID(id), level);
    }
}
