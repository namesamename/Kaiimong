using UnityEngine;

public class UIRecognitionPOPUP : UIPOPUP
{
    [HideInInspector]
    public UIRecognitionBoard board;
    [HideInInspector]
    public UIRecognitionSlotSpawner slot;
    [HideInInspector]
    public UIRecognitionBtn btn;
   
    ISetPOPUp[] setPOPUps;

    

    private void Awake()
    {
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();
        board = GetComponentInChildren<UIRecognitionBoard>();
        slot = GetComponentInChildren<UIRecognitionSlotSpawner>();
        btn = GetComponentInChildren<UIRecognitionBtn>();
    
    }

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {
        PlayShowAnimation();
        if (GlobalDataTable.Instance.DataCarrier.GetSave().Recognition <= 2)
        {
            for (int i = 0; i < setPOPUps.Length; i++)
            {
                setPOPUps[i].Initialize();
            }
        }
        else
        {
            Destroy();
        }
      
    }




}
