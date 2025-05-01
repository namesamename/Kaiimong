using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{

    Button[] buttons;
    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
        buttons[1].onClick.AddListener(OnApplicationQuit);
    }

    public void OnApplicationQuit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

}
