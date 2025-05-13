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
        // �ʱ�� : ���� 
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
        Debug.Log("�ŷδ� �ʱ�ȭ �Ϸ�");
        sceneContainer = new Dictionary<SceneState, Action>();

        foreach (SceneState state in Enum.GetValues(typeof(SceneState)))
        {
            sceneContainer[state] = null;
        }
    }

    private void OnEnable()
    {

        // sceneLoaded�� awake -> sceneLoaded -> start ������ ����� 
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("���� �ε�� �� ȣ��Ǵ� �Լ� ���� �� �̸� :" + scene.name + "\n / ���� �� state" + sceneState);
        if(sceneContainer == null)
        {
            SetDic();
        }

        
        // �̱��� LoadSceneAsync �� ����ǰ� �� �Ŀ� ���� 
        // ���� �� �̺�Ʈ ����
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

    // �� ��ȯ
    public void ChangeScene(SceneState nextState)
    {
       if(sceneContainer == null)
        {
            Debug.Log("�� �����̳ʰ� ��");
        }

        if (!sceneContainer.ContainsKey(nextState))
        {
            Debug.LogError($"�� ���� {nextState}�� ���� ������ ��ųʸ��� �����ϴ�.");
            return;
        }

        preSceneState = sceneState;
        // �� �ε�
        StartCoroutine(LoadSceneAsync(nextState));
    }
    public void ChanagePreScene()
    {
        StartCoroutine(LoadSceneAsync(preSceneState));
    }
    IEnumerator LoadSceneAsync(SceneState nextState)
    {
        yield return new WaitForSeconds(0.02f);

        // ���� �� ������Ʈ 
        sceneState = nextState;

        // ���̸�
        string sceneName = Enum.GetName(typeof(SceneState), sceneState);

        // �񵿱�ȭ �ε� 
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        
        // ���������� ��ٸ���
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        //GameManager.Instance.Initialize();
        // ���� �� �̺�Ʈ ����
        if (sceneContainer.ContainsKey(sceneState))
        {
            sceneContainer[sceneState]?.Invoke();
        }
    }
}
