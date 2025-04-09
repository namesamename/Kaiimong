using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterDataBase;

public class Character : MonoBehaviour
{

    public string characterId;
    public int Level;
    public int Recognition;
    public int Necessity;

    public void Initailize(string Id)
    {
        characterId = Id;
        HaveData();
        SetStat();
        SetVisual();
    }


    public void SetVisual()
    {

    }


    public CharacterSaveData CreatNewData()
    {

        CharacterSaveData data = new CharacterSaveData()
        {
            characterId = this.characterId,
            Level = this.Level,
            Recognition = this.Recognition,
            Necessity = this.Necessity,
            Savetype = SaveType.Character
        };
        CharacterDataBase.Instance.SaveDatas.Add(data);
        return data;



    }
    public void LoadData(CharacterSaveData saveData)
    {
        characterId = saveData.characterId;
        Level = saveData.Level;
        Recognition = saveData.Recognition;
        Necessity = saveData.Necessity;
    }

    public void HaveData()
    {
        var foundData = CharacterDataBase.Instance.SaveDatas.Find(i => i.characterId == characterId);
        if (foundData != null) 
        {
            LoadData(foundData);
        }
        else
        {
            CreatNewData();
        }
    }

    public void SetStat()
    {
        GetComponent<CharacterStat>().SetCharacter(CharacterDataBase.Instance.GetCharSOToGUID(characterId));
    }

}
