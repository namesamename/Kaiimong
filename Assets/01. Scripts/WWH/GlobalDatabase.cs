
public class GlobalDatabase : Singleton<GlobalDatabase>
{

    public CharacterDataBase character;
    public SkillDataBase skill;
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
        character = GetComponentInChildren<CharacterDataBase>();
        skill = GetComponentInChildren<SkillDataBase>();

        character.Initialize();
        skill.Initialize();

    }


}
