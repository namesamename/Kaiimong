
using UnityEngine;


public class Character : MonoBehaviour
{

    [HideInInspector]
    public CharacterSkillBook skillBook;
    public CharacterStat stat;
    public CharacterVisual visual;



    public string characterId;
    public int Level;
    public int Recognition;
    public int Necessity;

    private void Awake()
    {
        skillBook =GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();
    }

    public void Initialize(string Id)
    {
        characterId = Id;
        HaveData();
        SetStat();
        SetVisual();
        skillBook.SkillSet(characterId);
    }


    public void SetVisual()
    {
        //visual.ChangeAnimation();
    }


    public CharacterSaveData CreatNewData()
    {

        CharacterSaveData data = new CharacterSaveData()
        {
            ID = this.characterId,
            Level = this.Level,
            Recognition = this.Recognition,
            Necessity = this.Necessity,
            Savetype = SaveType.Character
        };
        SaveDataBase.Instance.SetSaveInstances(data, SaveType.Character);
        return data;



    }
    public void LoadData(CharacterSaveData saveData)
    {
        characterId = saveData.characterId;
        Level = saveData.Level;
        Recognition = saveData.Recognition;
        Necessity = saveData.Necessity;
        SetStat();
        skillBook.SkillSet(characterId);
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
       stat.SetCharacter(GlobalDatabase.Instance.character.GetCharSOToGUID(characterId));
    }

}
