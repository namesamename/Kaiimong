using UnityEngine;
using UnityEngine.UI;

public class LobbyBTns : MonoBehaviour
{
    Button[] buttons;
    private  void Awake()
    {
        buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[0].onClick.AddListener(() => TutorialChanger());
        buttons[1].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.InventoryScene));
        buttons[2].onClick.AddListener(() => { SceneLoader.Instance.ChangeScene(SceneState.CharacterSelectScene);  UIManager.Instance.characterIDType = UICharacterIDType.None; });
        buttons[3].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.PickupScene));

        buttons[4].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ShopScene));
        //buttons[4].onClick.AddListener(async () => await UIManager.Instance.ShowPopup("UpdateNoticePopUp"));

        buttons[5].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.QuestScene));

        //buttons[6].onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.MessageScene));
        buttons[6].onClick.AddListener(async () => await UIManager.Instance.ShowPopup("UpdateNoticePopUp"));

        buttons[7].onClick.AddListener(async () => await UIManager.Instance.ShowPopup("StaminaChargePOPUP"));

    }


    public void TutorialChanger()
    {
        if(!CurrencyManager.Instance.GetIsTutorial())
        {
            TutorialManager.Instance.CurPreDelete();
  

            SceneLoader.Instance.RegisterSceneAction(SceneState.ChapterScene, TutorialManager.Instance.TutorialAction);
            SceneLoader.Instance.RegisterSceneAction(SceneState.ChapterScene, () => SceneLoader.Instance.DisRegistarerAction(SceneState.ChapterScene, TutorialManager.Instance.TutorialAction));
            
        }
        SceneLoader.Instance.ChangeScene(SceneState.ChapterScene);

    }
    
}
