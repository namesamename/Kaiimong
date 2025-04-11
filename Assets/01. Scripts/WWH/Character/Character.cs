
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
        //�� ������ �� �� �ֵ���
        skillBook =GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();
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
        //visual.ChangeAnimation();
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

    /// <summary>
    /// ���� �ʱ�ȭ
    /// </summary>
    public void SetStat()
    {
       stat.SetCharacter(GlobalDatabase.Instance.character.GetCharSOToGUID(characterId));
    }

}
