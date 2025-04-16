using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [Header("Stage Base")]
    [SerializeField] private BattleSystem battleSystem;

    [Header("Stage Info")] // 선택창에서 받아온 데이터
    public Stage CurrentStage;
    public int Rounds;
    public List<CharacterCarrier> Players;
    public List<CharacterCarrier> Enemies;

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

        SceneLoader.Instance.RegisterSceneAction(SceneState.BattleScene, SetBattleScene);
    }

    void Start()
    {

    }

    void SetBattleScene() //SceneLoader에서 로드 확인 후 setbattlescene
    {
        GameObject obj = Instantiate(Resources.Load("Battle/BattleSystem")) as GameObject;
        battleSystem = obj.GetComponent<BattleSystem>();
        SetStageInfo();

    }
    void SetStageInfo()
    {
        GameObject background = Instantiate(Resources.Load(CurrentStage.BackgroundPath)) as GameObject;
        background.GetComponent<SpriteRenderer>().sprite = null;
        battleSystem.Players = new List<CharacterCarrier>(Players);
        //CurrentStage.EnemiesID 롤 적 객체 생성 후 리스트업하기
        Rounds = CurrentStage.Rounds;

    }

    void Update()
    {

    }
}
