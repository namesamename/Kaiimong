using UnityEngine.UI;

public class UIBreakPOPUP : UIPOPUP
{

    ISetPOPUp[] setPOPUps;

    public Button button;

    private void Awake()
    {
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();

    }
    private void Start()
    {
        Initialize();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Destroy);
    }

    public void Initialize()
    {
        for (int i = 0; i < setPOPUps.Length; i++)
        {
            setPOPUps[i].Initialize();
        }
    }
}
