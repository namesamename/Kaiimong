using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private Button addButton;
    [SerializeField] private TextMeshProUGUI activityPointText;

    [SerializeField] private RectTransform[] buttonsRect;
    [SerializeField] private float buttonPosY;

    void Start()
    {
        MoveUI();
        backButton.onClick.AddListener(OnBackButton);
        mainButton.onClick.AddListener(OnMainButton);
        addButton.onClick.AddListener(OnAddButton);
        SetUI();
    }

    private void MoveUI()
    {
        for (int i = 0; i < buttonsRect.Length; i++)
        {
            buttonsRect[i].DOLocalMoveY(buttonPosY, 1f);
        }
    }

    private async void OnAddButton()
    {
        UIStaminaChargePOPUP popup = await UIManager.Instance.ShowPopup("StaminaChargePOPUP") as UIStaminaChargePOPUP;
        popup.ClearAction();
        popup.OnChargeStamina += SetUI;
    }

    private void OnBackButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.ChapterScene);
    }

    private void OnMainButton()
    {
        SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
    }

    public void SetUI()
    {
        activityPointText.text = $"{CurrencyManager.Instance.GetCurrency(CurrencyType.Activity)} / {CurrencyManager.Instance.GetCurrency(CurrencyType.CurMaxStamina)}";
    }
}
