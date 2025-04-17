
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
        //각 하위에 들어갈 수 있도록
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
        //캐릭터 초기화
        CharacterSaveData.ID = Id;
        HaveData();
        SetStat();
        SetVisual();
        //스킬 초기화
        skillBook.SkillSet(CharacterSaveData.ID);
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
        SaveDataBase.Instance.SetSingleSaveInstance(CharacterSaveData, SaveType.Character);
        return CharacterSaveData;
    }
    /// <summary>
    /// 저장 데이터를 불러오기
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
    /// 데이터가 있는지 판단
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
    /// 스탯 초기화
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
