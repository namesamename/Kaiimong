using UnityEngine;
public abstract class CharacterCarrier : MonoBehaviour 
{
    public float longPressThreshold = 1.0f;
    private float touchStartTime;
    private bool longPressTriggered = false;
    private bool popupShown = false;
    private bool isTouchingThis = false;
    public GameObject StatPOPUP;
    int ID = 0;
    [HideInInspector]
    public CharacterSkillBook skillBook;
    [HideInInspector]
    public CharacterStat stat;
    [HideInInspector]
    public CharacterVisual visual;
    public GameObject SelectEffect;


    protected CharacterType type;

    private void Awake()
    {

        skillBook = GetComponentInChildren<CharacterSkillBook>();
        stat = GetComponentInChildren<CharacterStat>();
        visual = GetComponentInChildren<CharacterVisual>();

    }

    private  void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartTime = Time.time;
                longPressTriggered = false;

         
                if (IsTouchedMe(touch))
                {
                    isTouchingThis = true;
                }
                else
                {
                    isTouchingThis = false;
                }
            }
            else if ((touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) && !longPressTriggered && isTouchingThis)
            {
                if (Time.time - touchStartTime >= longPressThreshold)
                {
             
                    longPressTriggered = true;

                    if(SceneLoader.Instance.GetCur() == SceneState.BattleScene && !popupShown)
                    {
                        popupShown  = true;

                        GameObject game = Instantiate(StatPOPUP, FindAnyObjectByType<Canvas>().transform);
                        game.GetComponent<UIBattleStatPOPUP>().Initialize(this);
                    }
                
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                touchStartTime = 0f;
                longPressTriggered = false;
            }
        }
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

    public void SetPopupShonwFalse()
    {
        popupShown = false;
    }

    private bool IsTouchedMe(Touch touch)
    {
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(touch.position);
        Collider2D col = Physics2D.OverlapPoint(worldPoint);
        return col != null && col.gameObject == this.gameObject;
    }

    //    private void Start()
    //    {
    //        Initialize(1);
    //    }

    //    public void Initialize(int Id)
    //    {
    //        //캐릭터 초기화
    //        CharacterSaveData.ID = Id;
    //        HaveData();
    //        SetstatToLevel(CharacterSaveData.ID, CharacterSaveData.Level);
    //        SetVisual();
    //        //스킬 초기화
    //        skillBook.SkillSet(CharacterSaveData.ID);
    //    }

    //    /// <summary>
    //    /// 캐릭터 비주얼 바꾸기
    //    /// </summary>
    //    public void SetVisual()
    //    {
    //        visual.Initialize();
    //    }

    //    /// <summary>
    //    /// 저장 데이터가 없으면 새 데이터 만들기
    //    /// </summary>
    //    /// <returns></returns>
    //    public void SaveData()
    //    {
    //        SaveDataBase.Instance.SaveSingleData(CharacterSaveData);
    //    }
    //    /// <summary>
    //    /// 저장 데이터를 불러오기
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
    //    /// 데이터가 있는지 판단
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
