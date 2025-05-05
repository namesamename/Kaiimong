using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class CharacterDataTable
{

    public Dictionary<int, Character> CharacterSODIc = new Dictionary<int, Character>();

    public Dictionary<int, Enemy> EnemySODIc = new Dictionary<int, Enemy>();



    public List<Character> CharacterList = new List<Character>();
    public List<Enemy> EnemyList = new List<Enemy>();
    public  async void Initialize()
    {
        await CharacterInitializeAsync();
        await EnemyInitializeAsync();

    }

    public async Task EnemyInitializeAsync()
    {
        var handle = Addressables.LoadResourceLocationsAsync("Character");
        await handle.Task;

        int count = handle.Result.Count;
        List<Task> CharacterLoding = new List<Task>();

        for (int i = 1; i <= count; i++) 
        {

            var handles = Addressables.LoadAssetsAsync<Enemy>($"Enemy/Enemy_{i}", Enemy =>
            {
                EnemyList.Add(Enemy);
            });
            CharacterLoding.Add(handle.Task);
        }
        try
        {

            await Task.WhenAll(CharacterLoding);
        }
        catch (AggregateException ex)
        {

            foreach (var innerEx in ex.InnerExceptions)
            {
                Debug.Log(innerEx.Message);
            }
        }

        foreach (Enemy enemy in EnemyList)
        {
            EnemySODIc[enemy.ID] = enemy;
        }

        Debug.Log("캐릭터 로딩 완료");

    }

    public async Task CharacterInitializeAsync()
    {
        var Savedata = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        List<int> ID = new List<int>();
        foreach (var i in Savedata)
        {
            if (i.IsEquiped)
                ID.Add(i.ID);
        }

        List<Task> CharacterLoding = new List<Task>();

        foreach (var i in ID)
        {
            var handle = Addressables.LoadAssetsAsync<Character>($"CharacterSO/Character_{i}", character =>
            {
                CharacterList.Add(character);
            });
            CharacterLoding.Add(handle.Task);
        }


        try
        {

            await Task.WhenAll(CharacterLoding);
        }
        catch (AggregateException ex)
        {

            foreach (var innerEx in ex.InnerExceptions)
            {
                Debug.Log(innerEx.Message);
            }
        }

        foreach (Character character in CharacterList)
        {
            CharacterSODIc[character.ID] = character;
        }

        Debug.Log("캐릭터 로딩 완료");



    }




    public Character GetCharToID(int characterId)
    {
        if (CharacterSODIc.ContainsKey(characterId) && CharacterSODIc[characterId] != null)
        {
            return CharacterSODIc[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }


    public Enemy GetEnemyToID(int characterId)
    {
        if (EnemySODIc.ContainsKey(characterId) && EnemySODIc[characterId] != null)
        {
            return EnemySODIc[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }
    //캐릭터 아이디로 생성


    public async Task<GameObject> GetPrefabs(bool IsEnemy)
    {
        AsyncOperationHandle  handle;


        if (IsEnemy)
        {
            handle = Addressables.LoadAssetAsync<GameObject>("Character/EnemyPrefabs");
        }
        else
        {
            handle = Addressables.LoadAssetAsync<GameObject>("Character/FriendPrefabs");
        }

        await handle.Task;

        if(handle.Status == AsyncOperationStatus.Succeeded) 
        {
            GameObject game = (GameObject)handle.Result;
            return game;
        }
        else
        {
            return null;
        }


    }
    public  GameObject CharacterInstanceSummon(Character character, Vector3 pos, Transform parent = null)
    {

        GameObject CharacterObject = null;
        var task = GetPrefabs(false);
        task.ContinueWith(t =>
        {
            if (t.Status == TaskStatus.RanToCompletion)
            {
                CharacterObject = t.Result;
                CharacterObject.transform.position = pos;
                if (parent != null)
                {
                    CharacterObject.transform.SetParent(parent);
                }

                if (CharacterObject.GetComponent<FriendCarrier>() == null)
                {
                    CharacterObject.AddComponent<FriendCarrier>();
                }
                CharacterObject.GetComponent<FriendCarrier>().Initialize(character.ID);
            }
            else
            {
                Debug.Log("Failed to load Friend prefab.");
            }
        });

        return CharacterObject; 

    }
    //캐릭터 세이브 정보로 생성
    public GameObject CharacterInstanceSummonFromSaveData(CharacterSaveData saveData, Vector3 pos, Transform parent = null)
    {
        GameObject Character = CharacterInstanceSummon(GetCharToID(saveData.ID), pos, parent);
        return Character;
    }

    //public GameObject CharacterSummonToIDandLevel(int ID, int Level)
    //{
    //    Character character = GetCharToID(ID);
    //    GameObject game = CharacterInstanceSummon(character, Vector3.zero);
    //    game.GetComponent<FriendCarrier>().SetstatToLevel(ID, Level);
    //    return game;
    //}

    public GameObject EnemyInstanceSummon(Enemy character, int level, Vector3 pos, Transform parent = null)
    {
        GameObject CharacterObject = null;
        var task = GetPrefabs(true);
        task.ContinueWith(t =>
        {
            if (t.Status == TaskStatus.RanToCompletion)
            {
                CharacterObject = t.Result;
                CharacterObject.transform.position = pos;
                if (parent != null)
                {
                    CharacterObject.transform.SetParent(parent);
                }

                if (CharacterObject.GetComponent<EnemyCarrier>() == null)
                {
                    CharacterObject.AddComponent<EnemyCarrier>();
                }
                CharacterObject.GetComponent<EnemyCarrier>().Initialize(character.ID,level);
            }
            else
            {
                Debug.Log("Failed to load Enemy prefab.");
            }
        });

        return CharacterObject;

    }




}
