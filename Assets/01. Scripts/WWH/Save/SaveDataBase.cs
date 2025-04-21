using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class SaveDataBase : Singleton<SaveDataBase>
{

    //세이브 데이타 있는 거 보관
    public Dictionary<SaveType, List<SaveInstance>> SaveDic = new Dictionary<SaveType, List<SaveInstance>>();
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
        saveInstances = LoadAll();
        foreach (SaveType type in Enum.GetValues(typeof(SaveType)))
        {
            SaveDic[type] = new List<SaveInstance>();
        }
        foreach (List<SaveInstance> instanceList in saveInstances)
        {
            foreach (SaveInstance instance in instanceList)
            {
                SaveType type = instance.Savetype;

                if (!SaveDic.ContainsKey(type))
                {
                    SaveDic[type] = new List<SaveInstance>();
                }

                SaveDic[type].Add(instance);
            }
        }
    }
    public T GetSaveDataToID<T>(SaveType type, int Id ) where T :SaveInstance
    {
        if (SaveDic.TryGetValue(type, out List<SaveInstance> Sava)&& SaveDic[type].Count > 0 )
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
    public List<T> GetSaveInstanceList<T>(SaveType type) where T : SaveInstance
    {
        if (SaveDic.TryGetValue(type , out List<SaveInstance> Save))
        {
            return Save.OfType<T>().ToList();
        }

        return new List<T>();
    }

    ///// <summary>
    ///// 한 종류의 세이브 저장이 아님
    ///// </summary>
    ///// <param name="Data"></param>
    ///// <param name="SaveType"></param>
    //public void SetSingleSaveInstance(SaveInstance Data, SaveType SaveType) 
    //{
    //    if(SaveDic.TryGetValue(SaveType, out List<SaveInstance> SaveList))
    //    {
    //        int Index = SaveList.FindIndex(x => x.ID == Data.ID);
    //        if (Index != -1)
    //        {
    //            SaveList[Index] = Data;
    //        }
    //        else
    //        {
    //            SaveList.Add(Data);
    //        }
    //    }
    //    else
    //    {
    //        SaveDic[SaveType] = new List<SaveInstance>
    //        {Data};
    //    }
    //}
    public  void SavingList(List<SaveInstance> SaveList, SaveType SaveType)
    {
        RemoveDuplicates(SaveList);
        GameSaveSystem.Save(SaveType, SaveList);
    }
    public  void SaveAll()
    {
        if (SaveDic.TryGetValue(SaveType.Character, out List<SaveInstance> characterDatas))
        {
            SavingList(characterDatas,SaveType.Character);
        }
        if (SaveDic.TryGetValue(SaveType.Currency, out List<SaveInstance> CurrencyDatas))
        {
            SavingList(CurrencyDatas, SaveType.Currency);
        }
    }
    public List<List<SaveInstance>> LoadAll()
    {
        //세이브리스트를 담은 리스트를 만듬
        List<List<SaveInstance>> SaveList = new List<List<SaveInstance>>();
        //이넘 타입에 따라서 풀게
        foreach (SaveType saveType in Enum.GetValues(typeof(SaveType)))
        {
            //각 타입마다 먼저 로드를 함
            List<SaveInstance> saves = GameSaveSystem.Load(saveType);

            //1개 이상일 경우 딕셔너리에 데이터를 추가함
            if (saves.Count > 0)
            {
                SaveDic[saveType] = saves;
            }
            //그리고 다시 묶음
            SaveList.Add(saves);
        }
        //리스트를 풀어요
        return SaveList;
    }

    public void SaveSingleData(SaveInstance instance)
    {
        if (SaveDic.ContainsKey(instance.Savetype))
        {
            int Index = SaveDic[instance.Savetype].FindIndex(x => x.ID == instance.ID);
            if (Index != -1)
            {
                SaveDic[instance.Savetype][Index] = instance;
            }
            else
            {
                SaveDic[instance.Savetype].Add(instance);
            }
            GameSaveSystem.Save(instance.Savetype, SaveDic[instance.Savetype]);
        }
        else
        {
            SaveDic[instance.Savetype] = new List<SaveInstance>
            {
                instance
            };
            GameSaveSystem.Save(instance.Savetype, SaveDic[instance.Savetype]);
        }
    }

    private List<SaveInstance> RemoveDuplicates(List<SaveInstance> saveList)
    {
        Dictionary<int, SaveInstance> uniqueItems = new Dictionary<int, SaveInstance>();

        foreach (var item in saveList)
        {
            int key = item.ID; 
            uniqueItems[key] = item;
        }
        return uniqueItems.Values.ToList();
    }


    private void OnApplicationQuit()
    {
       
    }




}

