
using UnityEngine;


public class GlobalDataTable : Singleton<GlobalDataTable>
{

    public CharacterDataTable character; //데이터베이스 이름 바꾸기 데이터/테이블
    public SkillDataTable skill;
    public CurrencyDataTable currency;

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

        character.Initialize();
        skill.Initialize();
        currency.Initialize();

    }


}
