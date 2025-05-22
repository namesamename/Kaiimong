using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBattleMenu : MonoBehaviour
{
    Button[] buttons;

    public GameObject[] Menus;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }


    private void Start()
    {
        transform.DOLocalMoveY(-220, 2f);
        ButtonSet();

    }

    public void ButtonSet()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(SpeedSet);
        buttons[1].onClick.AddListener(StatSet);
        buttons[2].onClick.AddListener(SkillSet);


    }


    public void SpeedSet()
    {
        if (!Menus[0].activeSelf)
        {
            Menus[0].SetActive(true);
            Menus[1].SetActive(false);
            Menus[2].SetActive(false);
        }
    }
    public void StatSet()
    {
        if (!Menus[1].activeSelf)
        {
            Menus[1].SetActive(true);
            Menus[0].SetActive(false);
            Menus[2].SetActive(false);
        }
    }

    public void SkillSet()
    {
        if (!Menus[2].activeSelf)
        {
            Menus[2].SetActive(true);
            Menus[0].SetActive(false);
            Menus[1].SetActive(false);
        }
    }

}
