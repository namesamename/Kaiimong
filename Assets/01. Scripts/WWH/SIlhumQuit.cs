using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SIlhumQuit : MonoBehaviour
{

    public Button Button;
    private void Awake()
    {
        Button = GetComponentInChildren<Button>();
        Button.onClick.RemoveAllListeners();

    }
    private void Start()
    {
        Button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
    }

}
