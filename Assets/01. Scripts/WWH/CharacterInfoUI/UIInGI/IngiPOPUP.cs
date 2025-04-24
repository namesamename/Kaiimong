using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngiPOPUP : UIPopup
{

    public IngiBoard board;
    public IngiSlots slot;
    public IngiBtn btn;

    private void Awake()
    {
        board = GetComponentInChildren<IngiBoard>();
        slot = GetComponentInChildren<IngiSlots>();
        btn = GetComponentInChildren<IngiBtn>();
    }


}
