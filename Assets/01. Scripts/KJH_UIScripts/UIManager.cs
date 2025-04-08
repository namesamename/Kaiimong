using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // �ߺ� ����
            return;
        }

        _instance = this as UIManager;
        DontDestroyOnLoad(gameObject); // ���� ���� (�ʿ� �� ����)
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
