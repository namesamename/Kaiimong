using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private List<CharacterCarrier> curUnits = new List<CharacterCarrier>();

    [Header("Object for UIEnable")]
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject icons;

    [Header("Icons & Buttons")]
    [SerializeField] private List<Image> iconList = new List<Image>();
    [SerializeField] private List<Vector3> iconPos = new List<Vector3>();
    [SerializeField] private List<Vector3> iconSize = new List<Vector3>();
    [SerializeField] private List<Button> buttonList = new List<Button>();
    [SerializeField] private Button targetConfirmButton;
    //[SerializeField] private Button cancelButton;
    [SerializeField] private Button actionButton;
    public Action OnConfirmButton;

    private BattleSystem battleSystem;
    public BattleSystem BattleSystem { get { return battleSystem; } set { battleSystem = value; } }

    void Start()
    {
        battleSystem.OnPlayerTurn += GetActivePlayerUnit;
        //battleSystem.OnEnemyTurn += GetActiveEnemyUnit;
        AddListener();
        targetConfirmButton.enabled = false;
    }

    public void UnSubscribeCharacterUI()
    {
        battleSystem.OnPlayerTurn -= GetActivePlayerUnit;
        //battleSystem.OnEnemyTurn -= GetActiveEnemyUnit;
        RemoveListner();
    }

    void AddListener()
    {
        foreach (Button button in buttonList)
        {
            button.onClick.AddListener(() => OnClickSkillButton(button));
        }
        targetConfirmButton.onClick.AddListener(OnClickTargetConfirmButton);
        //cancelButton.onClick.AddListener(OnSkillCancelButton);
        actionButton.onClick.AddListener(OnActionButton);
    }

    void RemoveListner()
    {
        foreach (Button button in buttonList)
        {
            button.onClick.RemoveAllListeners();
        }
        targetConfirmButton.onClick.RemoveAllListeners();
        //cancelButton.onClick.RemoveAllListeners();
        actionButton.onClick.RemoveAllListeners();
    }

    public void SkillButtonDisable()
    {
        foreach (Button button in buttonList)
        {
            button.enabled = false;
        }
        targetConfirmButton.enabled = false;
    }

    public void SkillButtonAble()
    {
        foreach (Button button in buttonList)
        {
            button.enabled = true;
        }
    }

    public void ActionButtonAble()
    {
        targetConfirmButton.enabled = true;
    }

    void OnActionButton()
    {
        //DisableActionButton();
        //DIsableUI();
        //battleSystem.TurnIndex = 0;
        //PreviousCharacterIcon();
        battleSystem.CanAttack = true;
    }

    public void PlayerTurnEnd()
    {
        DisableActionButton();
        DIsableUI();
        battleSystem.TurnIndex = 0;
        PreviousCharacterIcon();
    }

    //void OnSkillCancelButton()
    //{
    //    if (battleSystem.CommandController.SkillCommands.Count == 0) return;
    //    if (battleSystem.TurnIndex == battleSystem.GetActivePlayers().Count)
    //    {
    //        DisableActionButton();
    //    }
    //    battleSystem.CommandController.RemoveCommand(battleSystem.CommandController.SkillCommands[battleSystem.CommandController.SkillCommands.Count - 1]);
    //    battleSystem.TurnIndex--;
    //    PreviousCharacterIcon();
    //    battleSystem.SkillChanged?.Invoke();
    //    battleSystem.SelectedSkill = null;
    //    battleSystem.CanSelectTarget = false;
    //    battleSystem.SelectedTarget = false;
    //}

    void OnClickSkillButton(Button button)
    {
        int skillNum = buttonList.IndexOf(button);
        battleSystem.SelectedSkill = curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[skillNum];
        battleSystem.SkillChanged?.Invoke();
        battleSystem.OnSkillSelected();
    }

    void OnClickTargetConfirmButton()
    {
        OnConfirmButton?.Invoke();
        SkillButtonDisable();
    }

    public void GetActivePlayerUnit()
    {
        curUnits.Clear();
        curUnits = battleSystem.GetActivePlayers();
        battleSystem.PlayerTurn = true;
        SetUI();
        EnableUI();
    }

    //public void GetActiveEnemyUnit()
    //{
    //    curUnits.Clear();
    //    curUnits = battleSystem.GetActiveEnemies();
    //    battleSystem.PlayerTurn = false;
    //}

    public void SetUI()
    {
        for (int i = 0; i < iconList.Count; i++)
        {
            if (!iconList[i].gameObject.activeSelf)
            {
                iconList[i].gameObject.SetActive(true);
            }
            if( i >= curUnits.Count)
            {
                iconList[i].gameObject.SetActive(false);
                continue;
            }
            iconList[i].sprite = curUnits[i].visual.Getincon();
            RectTransform iconRect = iconList[i].GetComponent<RectTransform>();
            iconPos.Add(iconRect.anchoredPosition);
            iconSize.Add(iconRect.localScale);
        }
        SetSkillButton();
    }

    public void NextCharacterIcon()
    {
        for (int i = 0; i < curUnits.Count; i++)
        {
            RectTransform curIconRect = iconList[i].GetComponent<RectTransform>();
            if (i <= battleSystem.TurnIndex)
            {
                curIconRect.DOAnchorPosX(-500, 1).SetEase(Ease.Linear);
            }
            if (i > battleSystem.TurnIndex)
            {
                curIconRect.DOAnchorPos(iconPos[i - battleSystem.TurnIndex - 1], 1).SetEase(Ease.Linear);
                curIconRect.DOScale(iconSize[i - battleSystem.TurnIndex - 1], 1).SetEase(Ease.Linear);
            }
        }
    }

    public void PreviousCharacterIcon()
    {
        for (int i = 0; i < curUnits.Count; i++)
        {
            RectTransform curIconRect = iconList[i].GetComponent<RectTransform>();
            if (i >= battleSystem.TurnIndex)
            {
                curIconRect.DOAnchorPos(iconPos[i - battleSystem.TurnIndex], 1).SetEase(Ease.Linear);
                curIconRect.DOScale(iconSize[i - battleSystem.TurnIndex], 1).SetEase(Ease.Linear);
            }
        }
    }

    void SetSkillButton()
    {
        for (int i = 0; i < curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList.Length; i++)
        {
            //buttonList[i].image.sprite = curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[i].skillSO.icon;
        }
    }

    void EnableUI()
    {
        skills.SetActive(true);
        icons.SetActive(true);
    }

    public void DIsableUI()
    {
        skills.SetActive(false);
        icons.SetActive(false);
    }

    public void SetActionButton()
    {
        targetConfirmButton.gameObject.SetActive(false);
        actionButton.gameObject.SetActive(true);
        foreach(Button button in buttonList)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void DisableActionButton()
    {
        foreach(Button button in buttonList)
        {
            button.gameObject.SetActive(true);
        }
        targetConfirmButton.gameObject.SetActive(true);
        actionButton.gameObject.SetActive(false);
    }
}
