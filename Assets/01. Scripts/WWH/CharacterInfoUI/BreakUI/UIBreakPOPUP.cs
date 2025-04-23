using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface ISetPOPUp
{
    public void Initialize();
}

public class UIBreakPOPUP : UIPopup
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
