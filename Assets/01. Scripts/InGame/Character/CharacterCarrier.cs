using UnityEngine;
public abstract class CharacterCarrier : MonoBehaviour 
{


    int ID = 0;
    [HideInInspector]
    public CharacterSkillBook skillBook;
    [HideInInspector]
    public CharacterStat stat;
    [HideInInspector]
    public CharacterVisual visual;
    public GameObject SelectEffect;


    private CharacterType type;

    private void Awake()
    {

        skillBook = GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();

    }


    public abstract void Initialize(int id, int level = -1);


    public  CharacterType GetCharacterType()
    {
        return type;
    }

    public void SetType(CharacterType type)
    {
        this.type = type;
    }

    public void SetID(int ID)
    {
        this.ID = ID; 
    }
    public int GetID()
    {
        return ID;
    }

    //    private void Start()
    //    {
    //        Initialize(1);
    //    }

    //    public void Initialize(int Id)
    //    {
    //        //ĳ���� �ʱ�ȭ
    //        CharacterSaveData.ID = Id;
    //        HaveData();
    //        SetstatToLevel(CharacterSaveData.ID, CharacterSaveData.Level);
    //        SetVisual();
    //        //��ų �ʱ�ȭ
    //        skillBook.SkillSet(CharacterSaveData.ID);
    //    }

    //    /// <summary>
    //    /// ĳ���� ���־� �ٲٱ�
    //    /// </summary>
    //    public void SetVisual()
    //    {
    //        visual.Initialize();
    //    }

    //    /// <summary>
    //    /// ���� �����Ͱ� ������ �� ������ �����
    //    /// </summary>
    //    /// <returns></returns>
    //    public void SaveData()
    //    {
    //        SaveDataBase.Instance.SaveSingleData(CharacterSaveData);
    //    }
    //    /// <summary>
    //    /// ���� �����͸� �ҷ�����
    //    /// </summary>
    //    /// <param name="saveData"></param>
    //    public void LoadData(CharacterSaveData saveData)
    //    {
    //        CharacterSaveData.ID = saveData.ID;
    //        CharacterSaveData.Level = saveData.Level;
    //        CharacterSaveData.Recognition = saveData.Recognition;
    //        CharacterSaveData.Necessity = saveData.Necessity;
    //        CharacterSaveData.IsEquiped = saveData.IsEquiped;
    //        CharacterSaveData.Love = saveData.Love;
    //        SetstatToLevel(saveData.ID, saveData.Level);
    //        SetVisual();
    //        skillBook.SkillSet(CharacterSaveData.ID);
    //    }

    //    /// <summary>
    //    /// �����Ͱ� �ִ��� �Ǵ�
    //    /// </summary>
    //    public void HaveData()
    //    {
    //        var foundData = SaveDataBase.Instance.GetSaveInstanceList<CharacterSaveData>(SaveType.Character);
    //        if (foundData != null)
    //        {
    //            if (foundData.Find(x => x.ID == CharacterSaveData.ID) != null)
    //            {
    //                LoadData(foundData.Find(x => x.ID == CharacterSaveData.ID));
    //            }
    //        }
    //        Save();

    //    }

    //    public void SetstatToLevel(int ID, int Level )
    //    {
    //        stat.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(ID), Level);
    //    }



    //    public void Save()
    //    {
    //        SaveDataBase.Instance.SaveSingleData(CharacterSaveData);  
    //    }
}
