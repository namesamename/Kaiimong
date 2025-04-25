using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    //�Ŵ����� ������ �ʱ�ȭ
    //�� �Ŵ������� �ٸ� �Ŵ����� �����ؾ��� �� ��� ����
    private void Awake()
    {
        //�� �Ŵ����� �ʱ�ȭ
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
