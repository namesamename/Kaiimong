using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : Singleton<ChapterManager>
{
    public Chapter CurChapter;

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

        SceneLoader.Instance.RegisterSceneAction(SceneState.StageSelectScene, SetChapter);
    }

    private void SetChapter()
    {

    }
}
