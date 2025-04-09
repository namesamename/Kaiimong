using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    private List<DummyUnit> curUnits = new List<DummyUnit>();

    [Header("Object for UIEnable")]
    [SerializeField] private GameObject skills;
    [SerializeField] private GameObject icons;

    [Header("Icons & Buttons")]
    [SerializeField] private List<Image> Icons = new List<Image>();
    [SerializeField] private Button firstSkillButton;
    [SerializeField] private Button secondSkillButton;
    [SerializeField] private Button ultSkillButton;

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
        firstSkillButton.onClick.AddListener(battleSystem.SetTarget);
        secondSkillButton.onClick.AddListener(battleSystem.SetTarget);
        ultSkillButton.onClick.AddListener(battleSystem.SetTarget);
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
            Icons[i].sprite = curUnits[i].icon;
        }
        SetSkillButton();
    }

    void SetSkillButton()
    {
        int index = battleSystem.TurnIndex;
        firstSkillButton.image.sprite = curUnits[index].skillDatas[0].icon;
        secondSkillButton.image.sprite = curUnits[index].skillDatas[1].icon;
        ultSkillButton.image.sprite = curUnits[index].skillDatas[2].icon;
    }

    void EnableUI()
    {
        skills.SetActive(true);
        icons.SetActive(true);
    }

    void DIsableUI()
    {
        skills.SetActive(false);
        icons.SetActive(false);
    }
}
