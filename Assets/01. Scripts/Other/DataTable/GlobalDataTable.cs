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
  
    public QuestDataTable Quest;
    public CharacterDialogueTable Dialogue;
    public ShopDataTable Shop;
    public PackageDataTable Package;



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

        Quest = new QuestDataTable();
        Dialogue = new CharacterDialogueTable();
        Shop = new ShopDataTable();
        Package = new PackageDataTable();

        Quest.Initialize();
        Dialogue.Initialize();

        Upgrade.Initialize();
        character.Initialize();
        skill.Initialize();
        currency.Initialize();
        Stage.Initialize();
        Chapter.Initialize();
        Item.Initialize();
        ChapterCategory.Initialize();
        EnemySpawn.Initialize();
        Shop.Initialize();
        Package.Initialize();
    }

}
