using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button speedDownButton;
    [SerializeField] private Button speedUpButton;
    public float currentSpeed = 1f;

    [SerializeField] private CharacterUI characterUI;
    [SerializeField] private GameObject popupBattlePausePrefab;
    [SerializeField] private PopupBattlePause popupBattlePause;
    private BattleSystem battleSystem;

    [SerializeField] private TextMeshProUGUI roundsText;
    [SerializeField] private TextMeshProUGUI turnText;

    public BattleSystem BattleSystem { get { return battleSystem; } set { battleSystem = value; } }
    public CharacterUI CharacterUI { get { return characterUI; } }


    private void Start()
    {
        characterUI.BattleSystem = battleSystem;
        pauseButton.onClick.AddListener(OnPauseButtonAsync);
        speedDownButton.onClick.AddListener(OnSpeedDownButton);
        speedUpButton.onClick.AddListener(OnSpeedUpButton);
    }

    private async void OnPauseButtonAsync()
    {
        Time.timeScale = 0;

        popupBattlePausePrefab = await UIManager.Instance.GetPOPUPPrefab("PopupBattlePause");
       

        GameObject instance = Instantiate(popupBattlePausePrefab, this.transform);
        popupBattlePause = instance.GetComponent<PopupBattlePause>();
        popupBattlePause.OnCancel += OnCancelButton;
        popupBattlePause.OnConfirm += StageManager.Instance.LoseStage;
    }

    private void OnSpeedDownButton()
    {
        currentSpeed = 1f;
        Time.timeScale = currentSpeed;
        speedUpButton.gameObject.SetActive(true);
        speedDownButton.gameObject.SetActive(false);
    }

    private void OnSpeedUpButton()
    {
        currentSpeed = 2.0f;
        Time.timeScale = currentSpeed;
        speedDownButton.gameObject.SetActive(true);
        speedUpButton.gameObject.SetActive(false);
    }

    private void OnCancelButton()
    {
        Time.timeScale = currentSpeed;
    }

    public void SetUI()
    {
        roundsText.text = $"라운드 {StageManager.Instance.CurrentRound.ToString()} / {StageManager.Instance.CurrentStage.Rounds}";
        turnText.text = $"턴 {StageManager.Instance.CurrentTurn.ToString()} / 20";
    }
}
