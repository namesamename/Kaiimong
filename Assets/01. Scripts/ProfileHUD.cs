using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileHUD : MonoBehaviour
{

    Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }


    public void SetButton()
    {

    }

}
