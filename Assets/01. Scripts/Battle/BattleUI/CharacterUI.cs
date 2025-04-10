using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    private List<DummyUnit> curUnits = new List<DummyUnit>();

    [Header("Object for UIEnable")]
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject icons;

    [Header("Icons & Buttons")]
    [SerializeField] private List<Image> iconList = new List<Image>();
    [SerializeField] private List<Button> buttonList = new List<Button>();
    [SerializeField] private Button targetConfirmButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button actionButton;
    public Action OnConfirmButton;
    //[SerializeField] private Button firstSkillButton;
    //[SerializeField] private Button secondSkillButton;
    //[SerializeField] private Button ultSkillButton;

    [Header("Icon transform & Scale")]
    [SerializeField] private float[] iconScale;
    [SerializeField] private Vector3[] iconPosition;

    private BattleSystem battleSystem;
    public BattleSystem BattleSystem { get { return battleSystem; } set { battleSystem = value; } }

    void Start()
    {
        battleSystem.OnPlayerTurn += GetActivePlayerUnit;
        battleSystem.OnEnemyTurn += GetActiveEnemyUnit;
        AddListener();
    }

    void Update()
    {
        
    }

    void AddListener()
    {

        foreach(Button button in buttonList)
        {
            button.onClick.AddListener(() => OnClickSkillButton(button));
        }
        targetConfirmButton.onClick.AddListener(OnClickTargetConfirmButton);
        cancelButton.onClick.AddListener(OnSkillCancelButton);
        actionButton.onClick.AddListener(OnActionButton);
    }

    void OnActionButton()
    {
        DisableActionButton();
        battleSystem.ChangeState(BattleState.Action);
    }

    void OnSkillCancelButton()
    {
        if(battleSystem.TurnIndex == battleSystem.GetActivePlayers().Count)
        {
            DisableActionButton();
        }
        battleSystem.CommandController.RemoveCommand(battleSystem.CommandController.SkillCommands[battleSystem.CommandController.SkillCommands.Count - 1]);
        battleSystem.TurnIndex--;
    }

    void OnClickSkillButton(Button button)
    {
        int skillNum = buttonList.IndexOf(button);
        battleSystem.SelectedSkill = curUnits[battleSystem.TurnIndex].skillDatas[skillNum];
        battleSystem.SkillChanged?.Invoke();
        battleSystem.OnSkillSelected();        
    }

    void OnClickTargetConfirmButton()
    {
        OnConfirmButton?.Invoke();
    }

    public void GetActivePlayerUnit()
    {
        curUnits = battleSystem.GetActivePlayers();
        SetUI();
        EnableUI();
    }

    public void GetActiveEnemyUnit()
    {
        curUnits = battleSystem.GetActiveEnemies();
    }

    public void SetUI()
    {
        for (int i = 0; i < curUnits.Count; i++)
        {
            iconList[i].sprite = curUnits[i].icon;
        }
        SetSkillButton();
    }

    void SetSkillButton()
    {
        for(int i = 0; i < curUnits[battleSystem.TurnIndex].skillDatas.Count; i++)
        {
            buttonList[i].image.sprite = curUnits[battleSystem.TurnIndex].skillDatas[i].icon;
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
    }

    public void DisableActionButton()
    {
        targetConfirmButton.gameObject.SetActive(true);
        actionButton.gameObject.SetActive(false);
    }
}
