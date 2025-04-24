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

        buttons[1].onClick.AddListener(ingiPOPUP.Destroy);
    }
}
