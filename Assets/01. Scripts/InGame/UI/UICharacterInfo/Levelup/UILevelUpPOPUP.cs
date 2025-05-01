public class UILevelUpPOPUP : UIPOPUP
{

    public int UsingGlod;
    public int UsingAmulet;
    public int CurLevel;
    public int NextLevel;
    public int LevelInterval;

    public UILevelupBtn Btn;
    public UILevelUPEffect Effect;
    public UILevelupStatboard Stat;


    public ISetPOPUp[] setPOPUps;



    private void Awake()
    {
       
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();
        Btn = GetComponentInChildren<UILevelupBtn>();
        Effect = GetComponentInChildren<UILevelUPEffect>();
        Stat = GetComponentInChildren<UILevelupStatboard>();
 
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetDefault();
        for (int i = 0; i < setPOPUps.Length; i++)
        {
            setPOPUps[i].Initialize();
        }
    }

    public void SetDefault()
    {
        UsingGlod = 0;
        UsingAmulet = 0;
        LevelInterval = 0;
        CurLevel = GlobalDataTable.Instance.DataCarrier.GetSave().Level;
        NextLevel = CurLevel;
    }

    public void UISet()
    {
      CharacterInfoHUDManager.Instance.Initialize();
    }



}
