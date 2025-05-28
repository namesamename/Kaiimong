using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private List<CharacterCarrier> curUnits = new List<CharacterCarrier>();

    [Header("Object for UIEnable")]
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject icons;
    [SerializeField] private GameObject stat;
    [SerializeField] private GameObject skillInfo;
    [SerializeField] private GameObject CommandBtn;


    [Header("Icons & Buttons")]
    [SerializeField] private List<Image> iconList = new List<Image>();
    [SerializeField] private List<Vector3> iconPos = new List<Vector3>();
    [SerializeField] private List<Vector3> iconSize = new List<Vector3>();
    [SerializeField] private List<Button> buttonList = new List<Button>();
    [SerializeField] private List<TextMeshProUGUI> skillTypeList = new List<TextMeshProUGUI>();
    [SerializeField] private Button targetConfirmButton;
    [SerializeField] private Image ultCover;
    [SerializeField] private Button ultButton;
    //[SerializeField] private Button cancelButton;
    [SerializeField] private Button actionButton;
    private List<Image> buttonIcon = new List<Image>();

    public Action OnConfirmButton;
    public Action ResetIconY;

    private BattleSystem battleSystem;
    public BattleSystem BattleSystem { get { return battleSystem; } set { battleSystem = value; } }




    void Start()
    {

        battleSystem.OnPlayerTurn += GetActivePlayerUnit;
        //battleSystem.OnEnemyTurn += GetActiveEnemyUnit;
        AddListener();
        targetConfirmButton.enabled = false;
        foreach (Button button in buttonList)
        {
            Image image = button.GetComponent<Image>();
            image.material = new Material(image.material);
            buttonIcon.Add(image);
        }
    }

    public void UltEnable()
    {
        if (curUnits[battleSystem.TurnIndex].skillBook.GetSkillGauge() == 7)
        {
            ultCover.gameObject.SetActive(false);
            ultButton.interactable = true;
        }
        else
        {
            ultCover.gameObject.SetActive(true);
            ultButton.interactable = false;
        }
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
            Button btn = button;
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(() => OnClickSkillButton(btn));
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
        SetSkillButton();
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
        ResetIconY?.Invoke();
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
        if (!CurrencyManager.Instance.GetIsTutorial())
        {
            TutorialManager.Instance.CurPreDelete();
            TutorialManager.Instance.TutorialAction();
        }



        battleSystem.SelectedSkill = curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[skillNum];
        SkillSelectEffect(skillNum);
        battleSystem.SkillChanged?.Invoke();
        battleSystem.CharacterSelector.initialTarget = false;
        targetConfirmButton.enabled = false;

        battleSystem.OnSkillSelected();
        button.GetComponent<UILongPressButton>().SetBattleSystem(curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[skillNum]);




    }

    public void SkillSelectEffect(int skillNum)
    {
        foreach (Image icon in buttonIcon)
        {
            Material mat = icon.material;

            mat.SetFloat("_OutlineEnabled", 0);
        }

        if (skillNum < buttonIcon.Count)
        {
            Image _image = buttonIcon[skillNum];
            Material _material = _image.material;

            _material.SetFloat("_OutlineEnabled", 1);
        }

        if (skillNum == 100)
        {
            foreach (Image icon in buttonIcon)
            {
                Material mat = icon.material;

                mat.SetFloat("_OutlineEnabled", 0);
            }
        }
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
            if (i >= curUnits.Count)
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
                curIconRect.DOAnchorPosX(iconPos[i - battleSystem.TurnIndex - 1].x, 1).SetEase(Ease.Linear);
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

    private async void SetSkillButton()
    {
        UltEnable();
        Sprite[] sprites = new Sprite[3];

        for (int i = 0; i < curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList.Length; i++)
        {
            int id = curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[i].SkillSO.ID;
            sprites[i] = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.SkillIcon, id);
            skillTypeList[i].text = curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[i].SkillSO.Type.ToString().ToUpper();
            //curUnits[battleSystem.TurnIndex].skillBook.ActiveSkillList[i].skillSO.icon;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            buttonList[i].image.sprite = sprites[i];
        }
        ultCover.sprite = buttonList[2].image.sprite;

        SkillSelectEffect(100);
    }

    void EnableUI()
    {
        skills.SetActive(true);
        skills.GetComponent<CharacterUIEffect>().BounceEffect();
        icons.SetActive(true);
        icons.GetComponent<CharacterUIEffect>().BounceEffect();

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
        foreach (Button button in buttonList)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void DisableActionButton()
    {
        foreach (Button button in buttonList)
        {
            button.gameObject.SetActive(true);
        }
        targetConfirmButton.gameObject.SetActive(true);
        actionButton.gameObject.SetActive(false);
    }






}
