using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngiPOPUP : UIPopup
{
    [HideInInspector]
    public IngiBoard board;
    [HideInInspector]
    public IngiSlots slot;
    [HideInInspector]
    public IngiBtn btn;
   
    ISetPOPUp[] setPOPUps;

    

    private void Awake()
    {
        setPOPUps = GetComponentsInChildren<ISetPOPUp>();
        board = GetComponentInChildren<IngiBoard>();
        slot = GetComponentInChildren<IngiSlots>();
        btn = GetComponentInChildren<IngiBtn>();
    
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
