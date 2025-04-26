using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class UIManager : Singleton<UIManager>
{


    List<GameObject> POPS = new List<GameObject>();
    public void Initialize()
    {
        GameObject[] POPs = Resources.LoadAll<GameObject>("Popups");

        for (int i = 0; i < POPs.Length; i++) 
        {
            POPS.Add(POPs[i]);
        }

    }

    public T ShowPopup<T>() where T : UIPopup
    {
        return ShowPopup(typeof(T).Name) as T;
    }

    public UIPopup ShowPopup(string popupName)
    {
        
        var obj = POPS.Find(x => x.name == popupName);
        //var obj = Resources.Load($"Popups/{popupName}", typeof(GameObject)) as GameObject;
        if (obj == null)
        {
            Debug.LogWarning($"Failed to ShowPopup({popupName})");
            return null;
        }
        return ShowPopupWithPrefab(obj);
    }

    private UIPopup ShowPopupWithPrefab(GameObject prefab)
    {
        var obj = Instantiate(prefab, GameObject.Find("Canvas").transform);
        return ShowPopup(obj);
    }

    private UIPopup ShowPopup(GameObject obj)
    {
        var popup = obj.GetComponent<UIPopup>();
        obj.SetActive(true);
        return popup;
    }

    public void CreatUI(string prefabPath)
    {
        var obj = Resources.Load(prefabPath, typeof(GameObject)) as GameObject;
        Instantiate(obj, GameObject.Find("Canvas").transform);


    }

}

