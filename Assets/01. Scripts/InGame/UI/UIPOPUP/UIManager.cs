using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.TextCore.Text;


public class UIManager : Singleton<UIManager>
{
    UICharacterIDSaveData characterID;
    public UICharacterIDType characterIDType;


    List<GameObject> POPS = new List<GameObject>();
    public void Initialize()
    {
        GameObject[] POPs = Resources.LoadAll<GameObject>("Popups");

        for (int i = 0; i < POPs.Length; i++) 
        {
            POPS.Add(POPs[i]);
        }

        characterID = new UICharacterIDSaveData()
        {
            ID = 0,
            Savetype = SaveType.UICharacterID,
            Lobby = 1,
            FirstProfile = 0,
            SecondProfile = 0,
            ThirdProfile = 0,
            FourthProfile = 0,
            IntroduceText = "Hi",
        };

        var CharacterID = SaveDataBase.Instance.GetSaveDataToID<UICharacterIDSaveData>(SaveType.UICharacterID, 0);
        if(CharacterID != null)
        {
            characterID.ID = CharacterID.ID;
            characterID.Savetype = SaveType.UICharacterID;
            characterID = CharacterID;
            SaveDataBase.Instance.SaveSingleData(characterID);
        }
        else
        {
        
            SaveDataBase.Instance.SaveSingleData(characterID);
        }



    }

    public int GetCharacterID(UICharacterIDType uI)
    {
        switch (uI)
        {
            case UICharacterIDType.Lobby:
                return characterID.Lobby;
            case UICharacterIDType.First:
                return characterID.FirstProfile;
            case UICharacterIDType.Second:
                return characterID.SecondProfile;
            case UICharacterIDType.Thrid:
                return characterID.ThirdProfile;
            case UICharacterIDType.Fourth:
                return characterID.FourthProfile;
            default:
                return 0;
              
        }
    }


    public void SetCharacterID( int ID)
    {
        switch (characterIDType)
        {
            case UICharacterIDType.Lobby:
                characterID.Lobby = ID;
                SaveDataBase.Instance.SaveSingleData(characterID);
                break;
            case UICharacterIDType.First:
                characterID.FirstProfile = ID;
                SaveDataBase.Instance.SaveSingleData(characterID);
                break;
            case UICharacterIDType.Second:
                characterID.SecondProfile = ID;
                SaveDataBase.Instance.SaveSingleData(characterID);
                break;
            case UICharacterIDType.Thrid:
                characterID.ThirdProfile = ID;
                SaveDataBase.Instance.SaveSingleData(characterID);
                break;
            case UICharacterIDType.Fourth:
                characterID.FourthProfile = ID;
                SaveDataBase.Instance.SaveSingleData(characterID);
                break;
            default:
                break;

        }
    }

    public void SetText(string Set)
    {
        characterID.IntroduceText = Set;
        SaveDataBase.Instance.SaveSingleData(characterID);
    }

    public string GetText()
    {
        return characterID.IntroduceText;
    }

    public async Task<T> ShowPopup<T>() where T : UIPOPUP
    {
        var popup = await ShowPopup(typeof(T).Name);
        return popup as T;
    }

    public async Task<UIPOPUP> ShowPopup(string popupName)
    {
       
        
        var obj = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.POPUP,popupName);
        //var obj = Resources.Load($"Popups/{popupName}", typeof(GameObject)) as GameObject;
        if (obj == null)
        {
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


    public async Task<GameObject> GetPOPUPPrefab(string popupName)
    {
         var obj = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.POPUP, popupName);
            
        return obj;
    }

    public async Task<GameObject> CreatTransformPOPUPAsync(string popupName, Transform transform)
    {
        var obj = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.POPUP, popupName);

        GameObject game = Instantiate(obj);

        int Ran = Random.Range(-4, 4);

        Vector3 vector3 = new Vector3(transform.position.x + Ran, transform.position.y + Ran + 3f, transform.position.z);
        game.transform.position = vector3;
        game.transform.rotation = Quaternion.identity;

        return game;
    }

}

