
using UnityEngine;


public class Character : MonoBehaviour
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
        SaveDataBase.Instance.SetSaveInstances(data, SaveType.Character);
        return data;
    }
    /// <summary>
    /// ���� �����͸� �ҷ�����
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadData(CharacterSaveData saveData)
    {
        characterId = saveData.characterId;
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
        var foundData = SaveDataBase.Instance.GetSaveInstances<CharacterSaveData>(SaveType.Character);


        if (foundData != null)
        {
            if (foundData.Find(x => x.characterId == characterId) != null)
            {
                Debug.Log(foundData.ToString());
                LoadData(foundData.Find(x => x.characterId == characterId));
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

}
