using System.Collections.Generic;
using UnityEngine;

public class CharacterDataTable 
{
    public Dictionary<int , Character> characterDic = new Dictionary<int , Character>();  
    public Dictionary<int, Enemy> enemyDic = new Dictionary<int , Enemy>();
    public GameObject CharacterPrefabs;
    public GameObject EnemyPrefabs;

    public void Initialize()
    {
        Character[] characters = Resources.LoadAll<Character>("Char");
        Enemy[] enemies = Resources.LoadAll<Enemy>("Enem");
        foreach (Character character in characters)
        {
            characterDic[character.ID] = character;
        }
        foreach (Enemy enemy in enemies)
        {
            enemies[enemy.ID] = enemy;
        }
    }
   
    public Character GetCharToID(int characterId)
    {
        if (characterDic.ContainsKey(characterId) && characterDic[characterId] != null )
        {
            return characterDic[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }


    public Enemy GetEnemyToID(int characterId) 
    {
        if (enemyDic.ContainsKey(characterId) && enemyDic[characterId] != null)
        {
            return enemyDic[characterId];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }
    //캐릭터 아이디로 생성
    public GameObject CharacterInstanceSummon(Character character, Vector3 pos, Transform parent = null)
    {
        GameObject CharacterObject = Object.Instantiate(CharacterPrefabs, pos, Quaternion.identity , parent);

        if(CharacterObject.GetComponent<CharacterCarrier>() == null) 
        {   CharacterObject.AddComponent<CharacterCarrier>();}
        CharacterObject.GetComponent<CharacterCarrier>().Initialize(character.ID);
        return CharacterObject;

    }
    //캐릭터 세이브 정보로 생성
    public GameObject CharacterInstanceSummonFromSaveData(CharacterSaveData saveData , Vector3 pos, Transform parent = null)
    {
        GameObject Character = CharacterInstanceSummon(GetCharToID(saveData.ID), pos, parent);
        return Character;
    }

    public GameObject CharacterSummonToIDandLevel(int ID, int Level)
    {
        Character character = GetCharToID(ID);
        GameObject game = CharacterInstanceSummon(character, Vector3.zero);
        game.GetComponent<CharacterCarrier>().SetstatToLevel(ID, Level);
        return game;
    }

    public GameObject EnemyInstanceSummon(Enemy character, int level,Vector3 pos, Transform parent = null)
    {
        GameObject CharacterObject = Object.Instantiate(EnemyPrefabs, pos, Quaternion.identity, parent);

        if (CharacterObject.GetComponent<EnemyCarrier>() == null)
        { CharacterObject.AddComponent<EnemyCarrier>(); }
        CharacterObject.GetComponent<EnemyCarrier>().Initialize(character.ID, level);
        return CharacterObject;

    }




}
