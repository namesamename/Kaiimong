using UnityEngine;
using UnityEngine.UI;

public class LobbyBTns : MonoBehaviour
{
    Button[] buttons;
    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[0].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ChapterScene));
        buttons[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.InventoryScene));
        buttons[2].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.CharacterSelectScene));
        buttons[3].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.PickupScene));
        buttons[4].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ShopScene));
        buttons[5].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.QuestScene));
        buttons[6].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ProfileScene));
        buttons[7].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.MessageScene));
        buttons[8].onClick.AddListener(() => UIManager.Instance.ShowPopup("StaminaChargePOPUP"));

    }
    
}
