using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileBtn : MonoBehaviour
{
    Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }


    public void BtnSet()
    {
        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.OptionScene));
        buttons[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));


    }

}
