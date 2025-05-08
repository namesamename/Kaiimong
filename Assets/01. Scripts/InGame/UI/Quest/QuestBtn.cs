
using UnityEngine;
using UnityEngine.UI;

public class QuestBtn : MonoBehaviour
{
    Button[] buttons;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
        buttons[1].onClick.AddListener(SceneLoader.Instance.ChanagePreScene);
    }
}
