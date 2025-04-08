using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        _instance = this as UIManager;
        DontDestroyOnLoad(gameObject); // 선택 사항 (필요 시 유지)
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
