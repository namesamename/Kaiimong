

public class FriendCarrier : CharacterCarrier
{
    public override void Initialize(int id, int level =-1)
    {
        SetID(id);
        visual.Initialize(id, CharacterType.Friend);
        stat.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(id), CharacterManager.Instance.GetSaveData(id).Level);
        skillBook.SkillSet(id);
    }
}
