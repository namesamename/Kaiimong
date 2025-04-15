using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;
    [SerializeField] private StageUI stageUI;

    [Header("Stage Info")]
    public int StageID;
    public int StageCount;
    public List<Character> Players;
    public List<Character> Enemies;

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
        SetBattleScene();
    }

    void SetBattleScene()
    {
        GameObject obj = Instantiate(Resources.Load("Battle/BattleSystem")) as GameObject;
        battleSystem = obj.GetComponent<BattleSystem>();
    }

    void SetStageInfo()
    {
        GameObject background = Instantiate(Resources.Load("Battle/BackGround")) as GameObject;
        background.GetComponent<SpriteRenderer>().sprite = null;
    }

    public void StageUISet()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject uiPrefab = Instantiate(Resources.Load("Battle/StageUI")) as GameObject;
    }


    void Update()
    {

    }
}
