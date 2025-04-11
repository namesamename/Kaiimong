
using UnityEngine;


public class Character : MonoBehaviour
{

    public string characterId;
    public int Level;
    public int Recognition;
    public int Necessity;

    public void Initialize(string Id)
    {
        characterId = Id;
        HaveData();
        SetStat();
        SetVisual();
    }


    public void SetVisual()
    {
        //나중에 스프라이트 추가되면
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
        SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character).Add(data);
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
        var foundData = SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character).Find(i => i.characterId == characterId);
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
        GetComponentInChildren<CharacterStat>().SetCharacter(GlobalDatabase.Instance.character.GetCharSOToGUID(characterId));
    }

}
