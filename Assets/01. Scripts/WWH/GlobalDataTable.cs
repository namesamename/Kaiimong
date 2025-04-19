
public class GlobalDataTable : Singleton<GlobalDataTable>
{

    public CharacterDataTable character; 
    public SkillDataTable skill;
    public CurrencyDataTable currency;
    public DataCarrier PartyID;
    public StageDataTable Stage;
    public ChapterDataTable Chapter;

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
        character = new CharacterDataTable();
        skill = new SkillDataTable();
        currency = new CurrencyDataTable();
        PartyID = new DataCarrier();
        Stage = new StageDataTable();
        Chapter = new ChapterDataTable();
        character.Initialize();
        skill.Initialize();
        currency.Initialize();
        Stage.Initialize();

    }


}
