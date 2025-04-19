
using UnityEngine;


public class CharacterCarrier : MonoBehaviour , ISavable
{

    [HideInInspector]
    public CharacterSkillBook skillBook;
    [HideInInspector]
    public CharacterStat stat;
    [HideInInspector]
    public CharacterVisual visual;
    [HideInInspector]
    public CharacterSaveData CharacterSaveData;
    public GameObject SelectEffect;


    private void Awake()
    {
        //각 하위에 들어갈 수 있도록

        Debug.Log("ASd");
        skillBook =GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();
        CharacterSaveData = new CharacterSaveData()
        {
            ID = 0,
            Level = 0,
            Love = 0,
            CumExp = 0,
            Necessity = 0,
            Recognition = 0,
            Savetype = SaveType.Character,
            IsEquiped = false,

        };
        CharacterSaveData.Savetype = SaveType.Character;

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
        SetstatToLevel(CharacterSaveData.ID, CharacterSaveData.Level);
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
        SaveDataBase.Instance.SaveSingleData(CharacterSaveData);
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
        CharacterSaveData.CumExp = saveData.CumExp;
        CharacterSaveData.IsEquiped = saveData.IsEquiped;
        CharacterSaveData.Love = saveData.Love;
        SetstatToLevel(saveData.ID, saveData.Level);
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

    public void SetstatToLevel(int ID, int Level )
    {
        stat.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(ID), Level);
    }



    public void Save()
    {
        SaveDataBase.Instance.SaveSingleData(CharacterSaveData);  
    }
}
