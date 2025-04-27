using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button mainButton;

    void Start()
    {
        backButton.onClick.AddListener(OnBackButton);
        mainButton.onClick.AddListener(OnMainButton);
    }

    private void OnBackButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.ChapterScene);
    }

    private void OnMainButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
    }
}
