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

    //저장목록들
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
    ChapterBackground,

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
    Pickup, //픽업뽑기를 위한 타입
    Standard // 상시뽑기를 위한 타입
}

public enum EItemType
{
    None,
    Item,            // 아이템
    Consume       // 소모품
}

public enum EConsumableType //소모품 타입
{
    None,
    EnergyItem,     // 활동력 충전 아이템
    Currency,       // 픽업 재화
    Box            // 아이템 박스
}
public enum ERarity
{
    S,  // S급
    A,  // A급
    B,  // B급
    C,  // C급
    D   // D급
}
// 씬 이름과 같아야함
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
    FriendField
}



