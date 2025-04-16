using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;

    [Header("Stage Info")] // ����â���� �޾ƿ� ������
    public Stage CurrentStage;
    public List<Character> Players;


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
    }

    void Start()
    {

    }

    void SetBattleScene() //SceneLoader���� �ε� Ȯ�� �� setbattlescene
    {
        GameObject obj = Instantiate(Resources.Load("Battle/BattleSystem")) as GameObject;
        battleSystem = obj.GetComponent<BattleSystem>();
        SetStageInfo();

    }
    void SetStageInfo()
    {
        GameObject background = Instantiate(Resources.Load(CurrentStage.BackgroundPath)) as GameObject;
        background.GetComponent<SpriteRenderer>().sprite = null;
    }

    void Update()
    {

    }
}
