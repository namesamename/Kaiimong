using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : Singleton<SceneLoader>
{
    [Header("===Container===")]
    private Dictionary<SceneState, Action> sceneContainer;
    [SerializeField] private SceneState sceneState;
    [SerializeField] private SceneState preSceneState;
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
    private void Start()
    {
        Initialize();
    }
    private void Update()
    {
        if(sceneContainer.ContainsKey(SceneState.BattleScene))
        {
            if(sceneContainer[SceneState.BattleScene] != null)
            {
                Debug.Log(sceneContainer[SceneState.BattleScene].ToString());
            }
            else
            {
                Debug.Log("no Contain Action");
            }
            
        }
    }
    public void Initialize()
    {
        SetDic();
        // 초기씬 : 메인 
        sceneState = SceneState.StartScene;
    }


    public SceneState GetCur()
    {
        return sceneState;
    }
    public SceneState GetPre()
    {
        return preSceneState;
    }

    public void SetDic()
    {
        Debug.Log("신로더 초기화 완료");
        sceneContainer = new Dictionary<SceneState, Action>();

        foreach (SceneState state in Enum.GetValues(typeof(SceneState)))
        {
            sceneContainer[state] = null;
        }
    }

    private void OnEnable()
    {

        // sceneLoaded는 awake -> sceneLoaded -> start 순서로 실행됨 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("씬이 로드될 때 호출되는 함수 현재 씬 이름 :" + scene.name + "\n / 현재 씬 state" + sceneState);
        if(sceneContainer == null)
        {
            SetDic();
        }

        
        // 싱글톤 LoadSceneAsync 이 실행되고 난 후에 실행 
        // 다음 씬 이벤트 시작
        if (sceneContainer.ContainsKey(sceneState))
        {
            sceneContainer[sceneState]?.Invoke();
        }
    }

    public void RegisterSceneAction(SceneState state, Action action)
    {
        if (sceneContainer == null)
        {
            SetDic();
        }
        if (sceneContainer.ContainsKey(state))
        {
            sceneContainer[state] += action;
        }
    }

    public void DisRegistarerAction(SceneState state, Action action)
    {
        if (sceneContainer.ContainsKey(state))
        {
            sceneContainer[state] -= action;
        }
    }

    // 씬 전환
    public void ChangeScene(SceneState nextState)
    {
       if(sceneContainer == null)
        {
            Debug.Log("신 컨테이너가 널");
        }

        if (!sceneContainer.ContainsKey(nextState))
        {
            Debug.LogError($"씬 상태 {nextState}에 대한 동작이 딕셔너리에 없습니다.");
            return;
        }

        preSceneState = sceneState;
        // 씬 로드
        StartCoroutine(LoadSceneAsync(nextState));
    }
    public void ChanagePreScene()
    {
        StartCoroutine(LoadSceneAsync(preSceneState));
    }
    IEnumerator LoadSceneAsync(SceneState nextState)
    {
        yield return new WaitForSeconds(0.02f);

        // 현재 씬 업데이트 
        sceneState = nextState;

        // 씬이름
        string sceneName = Enum.GetName(typeof(SceneState), sceneState);

        // 비동기화 로드 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        // 끝날때까지 기다리기
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //GameManager.Instance.Initialize();
        // 다음 씬 이벤트 시작
        if (sceneContainer.ContainsKey(sceneState))
        {
            sceneContainer[sceneState]?.Invoke();
        }
    }
}
