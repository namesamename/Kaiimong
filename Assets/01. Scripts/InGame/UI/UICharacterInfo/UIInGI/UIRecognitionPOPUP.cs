using UnityEngine;

public class UIRecognitionPOPUP : UIPOPUP
{
    [HideInInspector]
    public UIRecognitionBoard board;
    [HideInInspector]
    public UIRecognitionSlots slot;
    [HideInInspector]
    public UIRecognitionBtn btn;
   
    ISetPOPUp[] setPOPUps;

    

    private void Awake()
    {
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();
        board = GetComponentInChildren<UIRecognitionBoard>();
        slot = GetComponentInChildren<UIRecognitionSlots>();
        btn = GetComponentInChildren<UIRecognitionBtn>();
    
    }

    private void Start()
    {
        Initialize();
    }
    public void Initialize()
    {

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
