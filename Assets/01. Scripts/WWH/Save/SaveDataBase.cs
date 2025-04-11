using System.Collections.Generic;


public class SaveDataBase : Singleton<SaveDataBase>
{
    public Dictionary<SaveType, List<SaveInstance>> SaveDatas = new Dictionary<SaveType, List<SaveInstance>>();

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
    }
    public List<T> GetSaveInstances<T>(SaveType type) where T : SaveInstance
    {
        if (!SaveDatas.ContainsKey(type))
        {
            return new List<T>();
        }

        return SaveDatas[type].ConvertAll(x => x as T);
    }

    public void SetSaveInstances(SaveInstance data, SaveType saveType) 
    {
        if(!SaveDatas.ContainsKey(saveType)) 
        {
            SaveDatas[saveType] = new List<SaveInstance>();
        }
        SaveDatas[saveType].Add(data);
    }

   

}
[System.Serializable]
public class CharacterSaveData : SaveInstance
{
    public string characterId;
    public int Level;
    public int Recognition;
    public int Necessity;

}