using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngiBtn : MonoBehaviour, ISetPOPUp
{
    Button[] buttons;
    IngiPOPUP ingiPOPUP;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        ingiPOPUP = FindAnyObjectByType<IngiPOPUP>();
    }

    public void Initialize()
    {

        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        if (ingiPOPUP.slot.IsIngiBreakOK(ingiPOPUP.slot.NeedTabel))
        {
            buttons[0].interactable = true;

        }
        else
        {
            buttons[0].interactable = false;
        }

        buttons[1].onClick.AddListener(ingiPOPUP.Destroy);
    }


    public void UpGrade()
    {
        
    }
}
