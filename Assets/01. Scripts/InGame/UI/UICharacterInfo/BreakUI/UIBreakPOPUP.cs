public class UIBreakPOPUP : UIPOPUP
{

    ISetPOPUp[] setPOPUps;

    private void Awake()
    {
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();

    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        for (int i = 0; i < setPOPUps.Length; i++)
        {
            setPOPUps[i].Initialize();
        }
    }
}
