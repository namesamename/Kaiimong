using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
    }

}
