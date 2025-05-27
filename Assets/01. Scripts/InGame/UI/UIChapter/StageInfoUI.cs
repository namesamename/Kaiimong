using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    [SerializeField] private Button enterButton;
    [SerializeField] private TextMeshProUGUI stageName;
    [SerializeField] private TextMeshProUGUI stageActivityPointText;
    [SerializeField] private ItemBattleSlots ItemBattleSlots;
    [SerializeField] private StageSelectSceneUI stageSelectSceneUI;

    private Stage stage;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rect.DOAnchorPos(new Vector3(0, rect.anchoredPosition.y), 1f).SetEase(Ease.Linear);
    }

    public void DisableUI()
    {
        ItemBattleSlots.ClearSlots();
        RemoveListner();
        stage = null;
        rect.DOAnchorPos(new Vector3(960, rect.anchoredPosition.y), 1f).SetEase(Ease.Linear);
        StartCoroutine(DisableDelay());
    }

    private IEnumerator DisableDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void SetUI(StageSlot slot)
    {
        stage = slot.Stage;
        stageName.text = stage.Name;
        stageActivityPointText.text = $"-{stage.ActivityPoint}";
        ItemBattleSlots.SetItemList(stage.ID);
        enterButton.onClick.RemoveAllListeners();
        enterButton.onClick.AddListener(OnEnterButton);
    }

    private async void OnEnterButton()
    {
       


        int curActPoint = CurrencyManager.Instance.GetCurrency(CurrencyType.Activity);

        if (GlobalDataTable.Instance.DataCarrier.GetCharacterIDList().Count > 0)
        {
            if (curActPoint < stage.ActivityPoint)
            {
                PopupCurrencyLack popup = await UIManager.Instance.ShowPopup<PopupCurrencyLack>();
                popup.currencyType = CurrencyType.Activity;
                popup.SetDelegate(stageSelectSceneUI.SetUI);
            }
            else
            {
                CurrencyManager.Instance.SetCurrency(CurrencyType.Activity, -stage.ActivityPoint);
                stageSelectSceneUI.SetUI();

                StageManager.Instance.CurrentStage = stage;
                List<int> playerID = new List<int>(GlobalDataTable.Instance.DataCarrier.GetCharacterIDList());
                foreach (int id in playerID)
                {
                    Character newCharacter = GlobalDataTable.Instance.character.GetCharToID(id);
                    StageManager.Instance.Players.Add(newCharacter);
                }
                RemoveListner();
                if(!CurrencyManager.Instance.GetIsTutorial())
                {
                    TutorialManager.Instance.CurPreDelete();
                    SceneLoader.Instance.RegisterSceneAction(SceneState.BattleScene, TutorialManager.Instance.TutorialAction);
                    SceneLoader.Instance.RegisterSceneAction(SceneState.BattleScene, () => SceneLoader.Instance.DisRegistarerAction(SceneState.BattleScene, TutorialManager.Instance.TutorialAction));
                }

                SceneLoader.Instance.ChangeScene(SceneState.BattleScene);
            }
        }
    }

    private void RemoveListner()
    {
        enterButton.onClick.RemoveListener(OnEnterButton);
    }

 

}
