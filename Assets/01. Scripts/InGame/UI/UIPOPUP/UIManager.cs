using System.Collections.Generic;
using UnityEngine;


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

    public T ShowPopup<T>() where T : UIPOPUP
    {
        return ShowPopup(typeof(T).Name) as T;
    }

    public UIPOPUP ShowPopup(string popupName)
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

    private UIPOPUP ShowPopupWithPrefab(GameObject prefab)
    {
        var obj = Instantiate(prefab, GameObject.Find("Canvas").transform);
        return ShowPopup(obj);
    }

    private UIPOPUP ShowPopup(GameObject obj)
    {
        var popup = obj.GetComponent<UIPOPUP>();
        obj.SetActive(true);
        return popup;
    }

    public void CreatUI(string prefabPath)
    {
        var obj = Resources.Load(prefabPath, typeof(GameObject)) as GameObject;
        Instantiate(obj, GameObject.Find("Canvas").transform);


    }


    public GameObject CreatTransformPOPUP(string popupName, Transform transform)
    {
        var obj = POPS.Find(x => x.name == popupName);

        GameObject game = Instantiate(obj);

        int Ran = Random.Range(-4 , 4);

        Vector3 vector3 = new Vector3(transform.position.x + Ran, transform.position.y + Ran + 3f,  transform.position.z);
        game.transform.position = vector3;
        game.transform.rotation = Quaternion.identity;

        return game;
    }

}

