using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionBtn : MonoBehaviour
{
    Button[] buttons;


    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }


    private void Start()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChanagePreScene());
        buttons[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
    }




}
