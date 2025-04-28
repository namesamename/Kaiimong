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

    private void Start()
    {
        SetButton();
    }

    public void SetButton()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.YBK_Character));
        buttons[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.YBK_Character));
        buttons[2].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.YBK_Character));
        buttons[3].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.YBK_Character));




        buttons[5].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));


    }

}
