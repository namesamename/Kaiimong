
using UnityEngine;


public class CharacterCarrier : MonoBehaviour , ISavable
{

    [HideInInspector]
    public CharacterSkillBook skillBook;
    public CharacterStat stat;
    public CharacterVisual visual;

    public GameObject SelectEffect;
    public CharacterSaveData CharacterSaveData;

    private void Awake()
    {
        //�� ������ �� �� �ֵ���
        skillBook =GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();

        CharacterSaveData = new CharacterSaveData();

    }
    private void Start()
    {
        Initialize(1);
    }

    public void Initialize(int Id)
    {
        //ĳ���� �ʱ�ȭ
        CharacterSaveData.ID = Id;
        HaveData();
        SetStat();
        SetVisual();
        //��ų �ʱ�ȭ
        skillBook.SkillSet(CharacterSaveData.ID);
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
        SaveDataBase.Instance.SetSingleSaveInstance(CharacterSaveData, SaveType.Character);
        return CharacterSaveData;
    }
    /// <summary>
    /// ���� �����͸� �ҷ�����
    /// </summary>
    /// <param name="saveData"></param>
    public void LoadData(CharacterSaveData saveData)
    {
        CharacterSaveData.ID = saveData.ID;
        CharacterSaveData.Level = saveData.Level;
        CharacterSaveData.Recognition = saveData.Recognition;
        CharacterSaveData.Necessity = saveData.Necessity;
        CharacterSaveData.CurHp = stat.healthStat.Value;
        SetStat();
        SetVisual();
        skillBook.SkillSet(CharacterSaveData.ID);
    }

    /// <summary>
    /// �����Ͱ� �ִ��� �Ǵ�
    /// </summary>
    public void HaveData()
    {
        var foundData = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
        if (foundData != null)
        {
            if (foundData.Find(x => x.ID == CharacterSaveData.ID) != null)
            {
                LoadData(foundData.Find(x => x.ID == CharacterSaveData.ID));
            }
            else
            {
                CreatNewData();
            }
        }
        else
        {
            CreatNewData();
        }

    }

    public void SetstatToLevel(int Level, int ID)
    {
        stat.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(ID));
        stat.attackStat.Value += Level;
        stat.healthStat.Value += Level;
        stat.agilityStat.Value += Level;
        stat.criticalAttackStat.Value += (float)(Level * 0.01);
        stat.criticalPerStat.Value += (float)(Level * 0.01);
        stat.defenseStat.Value += Level;
    }

    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    public void SetStat()
    {
       stat.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(CharacterSaveData.ID));
    }

    public void Save()
    {
        SaveDataBase.Instance.SetSingleSaveInstance(CharacterSaveData, SaveType.Character);  
    }
}
