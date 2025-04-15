using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class SaveDataBase : Singleton<SaveDataBase>
{

    //세이브 데이타 있는 거 보관
    public Dictionary<SaveType, List<SaveInstance>> SaveDatas = new Dictionary<SaveType, List<SaveInstance>>();
    /// <summary>
    /// 특정 하나만 가져오기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <param name="Id"></param>
    /// <returns></returns>
    /// 

    private void Awake()
    {
        List<List<SaveInstance>> saveInstances = new List<List<SaveInstance>>();
        saveInstances = GameSaveSystem.LoadAll();
        foreach (List<SaveInstance> instance in saveInstances)
        {
            SaveDatas[instance[0].Savetype] = instance;
        }

    }


    public T GetSaveDataToID<T>(SaveType type, string Id ) where T :SaveInstance
    {
        if (SaveDatas[type].Count > 0 && SaveDatas.TryGetValue(type, out List<SaveInstance> Sava))
        {
            SaveInstance save = Sava.Find(x => x.ID == Id);
            if (save is T Instance)
            {return Instance;}
        }
        return null;
    }
    /// <summary>
    ///한 종류의 세이브 데이터 가져오기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="type"></param>
    /// <returns></returns>
    public List<T> GetSaveInstances<T>(SaveType type) where T : SaveInstance
    {
        if (SaveDatas.TryGetValue(type , out List<SaveInstance> Save))
        {
            return Save.OfType<T>().ToList();
        }

        return null;
    }

    /// <summary>
    /// 한 종류의 세이브 데이터 저장
    /// </summary>
    /// <param name="data"></param>
    /// <param name="saveType"></param>
    public void SetSaveInstances(SaveInstance data, SaveType saveType) 
    {

        if(SaveDatas.TryGetValue(saveType, out List<SaveInstance> Sava))
        {
            Sava.Add(data);
        }
        else
        {
            SaveDatas[saveType] = new List<SaveInstance>
            {
                data
            };
        }
    
    }



   

}

