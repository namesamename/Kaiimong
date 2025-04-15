
using UnityEngine;


public class Character : MonoBehaviour , ISavable
{

    [HideInInspector]
    public CharacterSkillBook skillBook;
    public CharacterStat stat;
    public CharacterVisual visual;

    public GameObject SelectEffect;
    public string characterId;
    public int Level;
    public int Recognition;
    public int Necessity;

    private void Awake()
    {
        //각 하위에 들어갈 수 있도록
        skillBook =GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();

    }
    private void Start()
    {
        Initialize("001");
    }

    public void Initialize(string Id)
    {
        //캐릭터 초기화
        characterId = Id;
        HaveData();
        SetStat();
        SetVisual();
        //스킬 초기화
        skillBook.SkillSet(characterId);
    }

    /// <summary>
    /// 캐릭터 비주얼 바꾸기
    /// </summary>
    public void SetVisual()
    {
        visual.Initialize();
    }

    /// <summary>
    /// 저장 데이터가 없으면 새 데이터 만들기
    /// </summary>
    /// <returns></returns>
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
        SaveDataBase.Instance.SetSingleSaveInstance(data, SaveType.Character);
        return data;
    }
    /// <summary>
    /// 저장 데이터를 불러오기
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadData(CharacterSaveData saveData)
    {
        characterId = saveData.ID;
        Level = saveData.Level;
        Recognition = saveData.Recognition;
        Necessity = saveData.Necessity;
        SetStat();
        SetVisual();
        skillBook.SkillSet(characterId);
    }

    /// <summary>
    /// 데이터가 있는지 판단
    /// </summary>
    public void HaveData()
    {
        var foundData = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);


        if (foundData != null)
        {
            if (foundData.Find(x => x.ID == characterId) != null)
            {
                LoadData(foundData.Find(x => x.ID == characterId));
            }else
            {
                CreatNewData();
            }
        }
        else
        {
            CreatNewData();
        }

    }

    /// <summary>
    /// 스탯 초기화
    /// </summary>
    public void SetStat()
    {
       stat.SetCharacter(GlobalDatabase.Instance.character.GetCharSOToID(characterId));
    }

    public void Save()
    {
        CharacterSaveData saveData = new CharacterSaveData()
        {
            ID = characterId,
            Recognition = this.Recognition,
            Necessity = this.Necessity,
            Savetype = SaveType.Character,
            Level = this.Level,
        };
        SaveDataBase.Instance.SetSingleSaveInstance(saveData, SaveType.Character);
       
    }
}
