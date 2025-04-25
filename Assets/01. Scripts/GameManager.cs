using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //매니저들 순서만 초기화
    //각 매니저들이 다른 매니저를 참조해야할 때 허브 역할
    private void Awake()
    {
        //각 매니저들 초기화
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
        LevelUpSystem.Init();
    }

}
