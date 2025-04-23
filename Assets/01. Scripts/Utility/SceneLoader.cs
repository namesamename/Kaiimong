using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �� �̸��� ���ƾ���
public enum SceneState
{
    MainScene,
    BattleScene,
    StageSelectScene,

}

public class SceneLoader : Singleton<SceneLoader>
{
    [Header("===Container===")]
    private Dictionary<SceneState, Action> sceneContainer;
    [SerializeField] private SceneState sceneState;

    private void Awake()
    {
        // DontDestory ���� 
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

        // �����̳� �ʱ�ȭ 
        sceneContainer = new Dictionary<SceneState, Action>();
        sceneContainer[SceneState.MainScene] = null;
        sceneContainer[SceneState.BattleScene] = null;
        sceneContainer[SceneState.StageSelectScene] = null;

        // �ʱ�� : ���� 
        sceneState = SceneState.StageSelectScene;
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

        // �̱��� LoadSceneAsync �� ����ǰ� �� �Ŀ� ���� 
        // ���� �� �̺�Ʈ ����
        if (sceneContainer.ContainsKey(sceneState))
        {
            sceneContainer[sceneState]?.Invoke();
        }
    }

    public void RegisterSceneAction(SceneState state, Action action)
    {
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
        if (!sceneContainer.ContainsKey(nextState))
        {
            Debug.LogError($"�� ���� {nextState}�� ���� ������ ��ųʸ��� �����ϴ�.");
            return;
        }

        // �� �ε�
        StartCoroutine(LoadSceneAsync(nextState));
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

        // ���� �� �̺�Ʈ ����
        // if (sceneContainer.ContainsKey(sceneState)) 
        // {
        //     sceneContainer[sceneState]?.Invoke();
        // }
    }
}
