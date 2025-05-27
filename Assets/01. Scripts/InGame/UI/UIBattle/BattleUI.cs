using DG.Tweening;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private RectTransform roundsRect;
    [SerializeField] private RectTransform[] buttonsRect;

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
        MoveUI();
        characterUI.BattleSystem = battleSystem;
        pauseButton.onClick.AddListener(OnPauseButtonAsync);
        speedDownButton.onClick.AddListener(OnSpeedDownButton);
        speedUpButton.onClick.AddListener(OnSpeedUpButton);


        if(!CurrencyManager.Instance.GetIsTutorial())
        {
            pauseButton.gameObject.SetActive(false);
            speedDownButton.gameObject.SetActive(false);
            speedUpButton.gameObject.SetActive(false);
        }

    }

    private void MoveUI()
    {
        roundsRect.DOLocalMoveX(-1000, 1f);
        for(int i = 0; i < buttonsRect.Length; i++)
        {
            buttonsRect[i].DOAnchorPosY(100, 1f);
        }
    }

    private void DisableButtons()
    {
        pauseButton.enabled = false;
        speedDownButton.enabled = false;
        speedUpButton.enabled = false;
    }

    private void EnableButtons()
    {
        pauseButton.enabled = true;
        speedDownButton.enabled = true;
        speedUpButton.enabled = true;
    }

    private async void OnPauseButtonAsync()
    {
 

        popupBattlePausePrefab = await UIManager.Instance.GetPOPUPPrefab("PopupBattlePause");
        GameObject instance = Instantiate(popupBattlePausePrefab, this.transform);
        popupBattlePause = instance.GetComponent<PopupBattlePause>();

        popupBattlePause.OnCancel -= OnCancelButton;
        popupBattlePause.OnConfirm -= StageManager.Instance.LoseStage;

        DisableButtons();
        characterUI.SkillButtonDisable();

        Time.timeScale = 0;

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
        EnableButtons();
        characterUI.SkillButtonAble();
    }

    public void SetUI()
    {
        roundsText.text = $"라운드 {StageManager.Instance.CurrentRound.ToString()} / {StageManager.Instance.CurrentStage.Rounds}";
        turnText.text = $"턴 {StageManager.Instance.CurrentTurn.ToString()} / 20";
    }
}
