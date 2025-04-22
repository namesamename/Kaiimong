using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelupBtn : MonoBehaviour
{
    Button[] buttons;

    LevelUpPopUP LevelUpPopUP;

    private void Start()
    {
        LevelUpPopUP =GetComponentInParent<LevelUpPopUP>();
        buttons = GetComponentsInChildren<Button>();
    }


    public void SetBtn()
    {
        //buttons[0].onClick.AddListener();
        //buttons[1].onClick.AddListener();
        //buttons[2].onClick.AddListener();
        //buttons[3].onClick.AddListener();
        //buttons[4].onClick.AddListener();
        //buttons[5].onClick.AddListener();
    }



    public void OnMax()
    {
        
    }

}
