using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectSceneUI : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private Button mainButton;
    [SerializeField] private TextMeshProUGUI activityPointText;

    void Start()
    {
        backButton.onClick.AddListener(OnBackButton);
        mainButton.onClick.AddListener(OnMainButton);
        SetUI();
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
