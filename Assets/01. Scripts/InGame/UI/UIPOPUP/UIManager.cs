using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;


public class UIManager : Singleton<UIManager>
{


 

    public T ShowPopup<T>() where T : UIPOPUP
    {
        return ShowPopup(typeof(T).Name) as T;
    }

    public async Task<UIPOPUP> ShowPopup(string popupName)
    {
       var OBj =  Addressables.LoadAssetAsync<GameObject>($"Popups/{popupName}");

        await OBj.Task;
        if (OBj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            GameObject popupPrefab = OBj.Result;
            return ShowPopupWithPrefab(popupPrefab);
        }
        else
        {
            Debug.LogWarning($"Failed to ShowPopup({popupName})");
            return null;
        }
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


    public async Task<GameObject> CreatTransformPOPUP(string popupName, Transform transform)
    {
        var OBj = Addressables.LoadAssetAsync<GameObject>($"Popups/{popupName}");

        await OBj.Task;
        if (OBj.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
        {
            GameObject game = Instantiate(OBj.Result);

            int Ran = Random.Range(-4, 4);

            Vector3 vector3 = new Vector3(transform.position.x + Ran, transform.position.y + Ran + 3f, transform.position.z);
            game.transform.position = vector3;
            game.transform.rotation = Quaternion.identity;

            return game;
        }
        else
        {
            Debug.LogWarning($"Failed to ShowPopup({popupName})");
            return null;
        }

    
    }

}

