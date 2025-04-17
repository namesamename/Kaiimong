
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
        //�� ������ �� �� �ֵ���
        skillBook =GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();
        CharacterSaveData = new CharacterSaveData();
        CharacterSaveData.Savetype = SaveType.Character;

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
        SetstatToLevel(CharacterSaveData.ID, CharacterSaveData.Level);
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
        SaveDataBase.Instance.SaveSingleData(CharacterSaveData);
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
        CharacterSaveData.CumExp = saveData.CumExp;
        CharacterSaveData.IsEquiped = saveData.IsEquiped;
        SetstatToLevel(saveData.ID, saveData.Level);
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

    public void SetstatToLevel(int ID, int Level )
    {
        stat.SetCharacter(GlobalDataTable.Instance.character.GetCharToID(ID), Level);

    }



    public void Save()
    {
        SaveDataBase.Instance.SetSingleSaveInstance(CharacterSaveData, SaveType.Character);  
    }
}
