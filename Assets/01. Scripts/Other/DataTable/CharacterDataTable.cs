using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTable
{
    public Dictionary<int, Character> CharacterDic = new Dictionary<int, Character>();
    public Dictionary<int, Enemy> EnemyDic = new Dictionary<int, Enemy>();
    public GameObject CharacterPrefabs;
    public GameObject EnemyPrefabs;

    public void Initialize()
    {
        Character[] characters = Resources.LoadAll<Character>("CharacterSO");
        Enemy[] enemies = Resources.LoadAll<Enemy>("Enemy");
        foreach (Character character in characters)
        {
            CharacterDic[character.ID] = character;
        }
        foreach (Enemy enemy in enemies)
        {
            EnemyDic[enemy.ID] = enemy;
        }
    }

    public Character GetCharToID(int characterId)
    {
        if (CharacterDic.ContainsKey(characterId) && CharacterDic[characterId] != null)
        {
            return CharacterDic[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }


    public Enemy GetEnemyToID(int characterId)
    {
        if (EnemyDic.ContainsKey(characterId) && EnemyDic[characterId] != null)
        {
            return EnemyDic[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }
    //ĳ���� ���̵�� ����
    public GameObject CharacterInstanceSummon(Character character, Vector3 pos, Transform parent = null)
    {
        GameObject CharacterPrefabs = Resources.Load("Character/FriendPrefabs") as GameObject;
        GameObject CharacterObject = Object.Instantiate(CharacterPrefabs, pos, Quaternion.identity, parent);

        if (CharacterObject.GetComponent<FriendCarrier>() == null)
        { CharacterObject.AddComponent<FriendCarrier>(); }
        CharacterObject.GetComponent<FriendCarrier>().Initialize(character.ID);
        return CharacterObject;

    }
    //ĳ���� ���̺� ������ ����
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
        GameObject EnemyPrefabs = Resources.Load("Character/EnemyPrefabs") as GameObject;
        GameObject CharacterObject = Object.Instantiate(EnemyPrefabs, pos, Quaternion.identity, parent);

        if (CharacterObject.GetComponent<EnemyCarrier>() == null)
        { CharacterObject.AddComponent<EnemyCarrier>(); }
        CharacterObject.GetComponent<EnemyCarrier>().Initialize(character.ID, level);
        return CharacterObject;

    }




}
