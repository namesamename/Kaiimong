
using UnityEngine;


public class GlobalDatabase : Singleton<GlobalDatabase>
{

    public CharacterDataBase character;
    public SkillDataBase skill;
    [HideInInspector]
    public SaveDataBase save;
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
        character = new CharacterDataBase();
        skill = new SkillDataBase();
        save = GetComponentInChildren<SaveDataBase>();

        character.Initialize();
        skill.Initialize();

    }


}
