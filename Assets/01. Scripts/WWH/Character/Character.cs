
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
        //�� ������ �� �� �ֵ���
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
        //ĳ���� �ʱ�ȭ
        characterId = Id;
        HaveData();
        SetStat();
        SetVisual();
        //��ų �ʱ�ȭ
        skillBook.SkillSet(characterId);
    }

    /// <summary>
    /// ĳ���� ���־� �ٲٱ�
    /// </summary>
    public void SetVisual()
    {
        visual.Initialize();
    }

    /// <summary>
    /// ���� �����Ͱ� ������ �� ������ �����
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
    /// ���� �����͸� �ҷ�����
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
    /// �����Ͱ� �ִ��� �Ǵ�
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
    /// ���� �ʱ�ȭ
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
