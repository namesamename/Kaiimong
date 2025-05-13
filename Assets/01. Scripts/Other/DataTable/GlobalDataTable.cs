public class GlobalDataTable : Singleton<GlobalDataTable>
{

    public CharacterDataTable character; 
    public SkillDataTable skill;
    public CurrencyDataTable currency;
    public DataCarrier DataCarrier;
    public StageDataTable Stage;
    public ChapterDataTable Chapter;
    public ItemDataTable Item;
    public UpGradeTable Upgrade;
    public EnemySpawnDataTable EnemySpawn;
    public ChapterCategoryDataTable ChapterCategory;
    public SpriteDataBase Sprite;
    public QuestDataTable Quest;
    public CharacterDialogueTable Dialogue;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        LevelUpSystem.Init();
        Initialize();
    }
    public void Initialize()
    {
        character = new CharacterDataTable();
        skill = new SkillDataTable();
        currency = new CurrencyDataTable();
        DataCarrier = new DataCarrier();
        Stage = new StageDataTable();
        Chapter = new ChapterDataTable();
        Item = new ItemDataTable();
        ChapterCategory = new ChapterCategoryDataTable();
        Upgrade = new UpGradeTable();
        EnemySpawn = new EnemySpawnDataTable();
        Sprite = new  SpriteDataBase();
        Quest = new QuestDataTable();
        Dialogue = new CharacterDialogueTable();

        Quest.Initialize();
        Dialogue.Initialize();
        Sprite.Initialize();
        Upgrade.Initialize();
        character.Initialize();
        skill.Initialize();
        currency.Initialize();
        Stage.Initialize();
        Chapter.Initialize();
        Item.Initialize();
        ChapterCategory.Initialize();
        EnemySpawn.Initialize();
    }

}
