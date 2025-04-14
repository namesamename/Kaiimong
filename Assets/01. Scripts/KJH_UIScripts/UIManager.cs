using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class UIManager : Singleton<UIManager>
{
    protected void Awake()
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

    public GameObject ShowPopup(string popupName)
    {
        var obj = Resources.Load($"Popups/{popupName}", typeof(GameObject)) as GameObject;
        if (!obj)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupName})");
            return null;
        }
        return Instantiate(obj, GameObject.Find("Canvas").transform);
    }
}
