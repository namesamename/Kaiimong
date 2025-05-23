public enum CharacterInfoType
{
    Back,
    Main,
    Skin,
    CharacterImage,
    Name,
    Stat,
    Dolpa,
    Levelup,
    IngiUp,
    ActiveSkill,
    PassiveSkill,
    CEO,
    NoticePanel,

}
public enum SpriteType
{
    RecoSD,
    Recoilu,
    BattleSprite,
    Illustration,
    Icon,
}
public enum CharacterType
{
    Friend,
    Enemy
}
public enum CurrencyType
{
    UserLevel,
    UserEXP,
    Gold,
    Gacha,
    Activity,
    Dia,
    CharacterEXP,
    CurMaxStamina,
    purchaseCount,
}
public enum SaveType
{

    //�����ϵ�
    Character,
    Currency,
    Stage,
    Chapter,
    Item,
    Quest,
    Time,
    UICharacterID,
    Audio

}


public enum SkillType
{
    attk,
    debuff,
    buff,
    heal
}

public enum SkillTargetType
{
    enemy,
    our
}
public enum AddreassablesType
{
    CharacterIcon,
    BattleSD,
    Illustration,
    RecognitionSD,
    RecognitionIllustration,
    EnemyBattleSD,
    POPUP,
    Passive,
    Quest,
    ItemSlot,
    SkillEffect,
    SoundEffectFx,
    CharacterSlotSprite,

    GatchaBanner,

    ChapterBackground,
    BattleIcon,
    SkillIcon,


}
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
public enum StatGrade
{
    A, B, C, D,
}

public enum PassiveType
{
    Stat,
    Heal,
    Other
}

public enum GatchaType
{
    Pickup, //�Ⱦ��̱⸦ ���� Ÿ��
    Standard // ��û̱⸦ ���� Ÿ��
}

public enum EItemType
{
    None,
    Item,            // ������
    Consume       // �Ҹ�ǰ
}

public enum EConsumableType //�Ҹ�ǰ Ÿ��
{
    None,
    EnergyItem,     // Ȱ���� ���� ������
    Currency,       // �Ⱦ� ��ȭ
    Box            // ������ �ڽ�
}
public enum ERarity
{
    S,  // S��
    A,  // A��
    B,  // B��
    C,  // C��
    D   // D��
}
// �� �̸��� ���ƾ���
public enum SceneState
{
    StartScene,
    LobbyScene,
    PickupScene,
    ProfileScene,
    StageSelectScene,
    BattleScene,
    InventoryScene,
    CharacterInfoScene,
    CharacterSelectScene,
    ChapterScene,
    MessageScene,
    QuestScene,
    ShopScene,
    OptionScene,
}
public enum UpgradeType
{
    Item,
    Level,
    Gold

}

public enum QuestType
{
    StageClear,
    LevelUp,
    KillMonster,
    Recognition,
    CharacterInteraction,
    Gacha,
    UseSkill,
    UesItem,
    UseGold,
    UseStamina,
}

public enum RewardType
{
    Item,
    Gold,
    Ticket,
    Cystal,
    Stamina,
    UserEXP,
    CharacterEXP,
}

public enum TimeType
{
    Daily,
    Weekly,
    Monthly
}

public enum UICharacterIDType
{
    Lobby,
    First,
    Second,
    Thrid,
    Fourth,
    None,

}

public enum SkillEffectType
{
    Self,
    AllEnemy,
    AllFriend,
    SingleEnemy,
    SingleFriend,
    BattleField,
    EnemyField,
    FriendField,
    transformToTarget,
}


public enum DamageType
{
    CriAndWeek,
    Cri,
    Week,
    Basic
}




