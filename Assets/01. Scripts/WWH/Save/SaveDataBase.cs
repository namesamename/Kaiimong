using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class SaveDataBase : Singleton<SaveDataBase>
{

    //���̺� ����Ÿ �ִ� �� ����
    public Dictionary<SaveType, List<SaveInstance>> SaveDic = new Dictionary<SaveType, List<SaveInstance>>();
    /// <summary>
    /// Ư�� �ϳ��� ��������
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
    ///�� ������ ���̺� ������ ��������
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
    ///// �� ������ ���̺� ������ �ƴ�
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
        //���̺긮��Ʈ�� ���� ����Ʈ�� ����
        List<List<SaveInstance>> SaveList = new List<List<SaveInstance>>();
        //�̳� Ÿ�Կ� ���� Ǯ��
        foreach (SaveType saveType in Enum.GetValues(typeof(SaveType)))
        {
            //�� Ÿ�Ը��� ���� �ε带 ��
            List<SaveInstance> saves = GameSaveSystem.Load(saveType);

            //1�� �̻��� ��� ��ųʸ��� �����͸� �߰���
            if (saves.Count > 0)
            {
                SaveDic[saveType] = saves;
            }
            //�׸��� �ٽ� ����
            SaveList.Add(saves);
        }
        //����Ʈ�� Ǯ���
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

