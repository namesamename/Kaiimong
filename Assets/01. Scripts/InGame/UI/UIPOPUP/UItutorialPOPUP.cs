using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UItutorialPOPUP : MonoBehaviour
{
    Button[] buttons;
    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>(); 
    }

    private  void Start()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
            buttons[i].onClick.AddListener( () =>   TutorialManager.Instance.NextCharTutorialAsync());
        }
    }


    

}
