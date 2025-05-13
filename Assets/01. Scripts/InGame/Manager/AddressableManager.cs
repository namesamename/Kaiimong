using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableManager : Singleton<AddressableManager>
{
    Dictionary<(AddreassablesType, int), AsyncOperationHandle> Tracer = new Dictionary<(AddreassablesType, int), AsyncOperationHandle>();

    List<AsyncOperationHandle> PrefabTracer=  new List<AsyncOperationHandle>();

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

    public async Task<T> LoadAsset<T>(AddreassablesType type, int id) where T : class
    {
        var key = (type, id);
        if (Tracer.ContainsKey(key))
        {
            return Tracer[key].Result as T;
        }

        var handle = Addressables.LoadAssetAsync<T>(TypeChanger(type) + id);

    
        Tracer[(type, id)] = handle;


        await handle.Task;

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {

            return handle.Result;
        }
        else
        {
            return default;
        }
    }

    public async Task<GameObject> LoadPrefabs(AddreassablesType type, string name)
    {
        var handle = Addressables.LoadAssetAsync<GameObject>(TypeChanger(type)+name);

        await handle.Task;
        if(handle.Status == AsyncOperationStatus.Succeeded) 
        {
            PrefabTracer.Add(handle);
            return handle.Result;
        }
        else
        {
            return default;
        }

    }
    
    public string TypeChanger(AddreassablesType type)
    {
        switch (type)
        {
            
            case AddreassablesType.EnemyBattleSD:
                return "EnemySprite/Enemy_";
            case AddreassablesType.BattleSD:
                return "CharacterBattleSD/Char_";
            case AddreassablesType.Illustration:
                return "CharacterSprite/Char_";
            case AddreassablesType.RecognitionSD:
                return "RecognitionSD/Char_";
            case AddreassablesType.RecognitionIllustration:
                return "RecognitionSprite/Char_";
            case AddreassablesType.CharacterIcon:
                return "Icon/Char_";
            case AddreassablesType.POPUP:
                return "Popups/";
            case AddreassablesType.Passive:
                return "Popups/";
            case AddreassablesType.Quest:
                return "Quest/";
            case AddreassablesType.ItemSlot:
                return "ItemSlot/";
            case AddreassablesType.SkillEffect:
                return "SkillEffect/SkillEffect_";
            default:
                return string.Empty;

        }
    }

    public void UnLoad(AddreassablesType type, int ID)
    {
        if (Tracer.TryGetValue((type, ID), out var handle))
        {
            Addressables.Release(handle);
            Tracer.Remove((type, ID));
        }
    }

    public void UnLoad(string name)
    {
        foreach(AsyncOperationHandle handle in PrefabTracer)
        {
            if(handle.Result.GetType().ToString() .Equals(name))
            {
                Addressables.Release(handle);
            }
        }

    }

    public void UnloadType(AddreassablesType type)
    {
        var Remove = new List<(AddreassablesType, int)>();

        foreach (var TypeandID in Tracer)
        {
            if (TypeandID.Key.Item1 == type)
            {
                Addressables.Release(TypeandID.Value);
                Remove.Add(TypeandID.Key);
            }
        }

        foreach (var key in Remove)
        {
            Tracer.Remove(key);
        }
    }
}
