
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

        buttons[0].onClick.AddListener(UnloadAndChangeScene);
        buttons[1].onClick.AddListener(UnloadAndChangePreScene);
    }

    public void UnloadAndChangeScene()
    {
        AddressableManager.Instance.UnLoad("Quest");
        SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
    }

    public void UnloadAndChangePreScene()
    {
        AddressableManager.Instance.UnLoad("Quest");
        SceneLoader.Instance.ChanagePreScene();
    }


}
