using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    [Header("UnitLocation")]
    [SerializeField] private List<UnitPlate> playerLocations;
    [SerializeField] private List<UnitPlate> enemyLocations;
    [SerializeField] private Transform playerParent;
    [SerializeField] private Transform enemyParent;

    [Header("Units")]
    public List<CharacterCarrier> Players; //캐릭터 선택에서 가져오고
    public List<CharacterCarrier> Enemies; //스테이지 데이터에서 가져오고
    [SerializeField] private List<CharacterCarrier> activePlayers = new List<CharacterCarrier>();    //현재 배치중인 유닛들 정보
    [SerializeField] private List<CharacterCarrier> activeEnemies = new List<CharacterCarrier>();
    public List<CharacterCarrier> GetActivePlayers() => activePlayers;
    public List<CharacterCarrier> GetActiveEnemies() => activeEnemies;

    [Header("BattleInfo")]
    public int TurnIndex = 0;
    public ActiveSkillObject SelectedSkill;
    public List<CharacterCarrier> Targets;
    private float betweenPhaseTime;
    public bool winFlag = false;
    public bool loseFlag = false;

    [Header("Appear")]
    private bool appearAnimComplete = false;

    [Header("Skill&TargetSelect")]
    public bool CanSelectTarget = false;
    public bool SelectedTarget = false;
    public bool PlayerTurn = true;

    [Header("AttackPhase")]
    public bool CanAttack = false;
    public bool AttackEnded = false;

    public CommandController CommandController { get; private set; }
    public CharacterSelector CharacterSelector { get; private set; }
    public BattleUI BattleUI;


    public Action OnPlayerTurn;
    public Action OnEnemyTurn;
    public Action SkillChanged;

    private void Awake()
    {
        CommandController = GetComponent<CommandController>();
        CharacterSelector = GetComponent<CharacterSelector>();
        CharacterSelector.battleSystem = this;
        BattleUI.BattleSystem = this;
        BattleUI.CharacterUI.BattleSystem = this;
    }

    void Start()
    {
        StartBattle();
    }

    void Update()
    {
        if (appearAnimComplete)
        {
            appearAnimComplete = false;
            StartCoroutine(ChangePhase(PlayerTurnPhase));
        }

        if (CanAttack)
        {
            CanAttack = false;
            StartCoroutine(ChangePhase(AttackPhase));
        }
    }

    public void UnSubscribeBattleSystem()
    {
        CharacterSelector.UnSubscribeCharacterSelector();
        BattleUI.CharacterUI.UnSubscribeCharacterUI();
    }

    public void UnSubscribeCharacterDeathAction(CharacterCarrier character)
    {
        character.stat.OnDeath -= () => EmptyPlateOnUnitDeath(character);
        character.stat.OnDeath -= () => RemoveTarget(character);
        character.stat.OnDeath -= CheckGameOver;
    }

    public void SetUI()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject uiPrefab = Instantiate(Resources.Load("UI/Battle/BattleUI")) as GameObject;
        StageManager.Instance.EndUISet();
    }
    private void AttackPhase()
    {
        CommandController.ExecuteCommand();

        if (PlayerTurn)
        {
            StartCoroutine(ChangePhase(EnemyTurnPhase));
        }
        else
        {
            SetBattle();
            if (appearAnimComplete)
            {
                StartCoroutine(ChangePhase(PlayerTurnPhase));
            }
        }
    }

    private void CheckGameOver()
    {
        if (Players.Count == 0 && activePlayers.Count == 0) StartCoroutine(ChangePhase(WinPhase));
        if (Enemies.Count == 0 && activeEnemies.Count == 0) StartCoroutine(ChangePhase(LosePhase));
    }

    public void StartBattle()
    {
        activePlayers.Clear();
        activeEnemies.Clear();
        SetBattle();
    }

    private void EnemyTurnPhase()
    {
        PlayerTurn = false;
        Debug.Log("enemyturn");
        activeEnemies = activeEnemies.OrderByDescending(x => x.stat.agilityStat.Value).ThenBy(x => x.stat.attackStat.Value).ToList();
        //activeEnemies.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
        CommandController.ClearList();
        EnemyRandomCommand();
    }

    private void PlayerTurnPhase()
    {
        PlayerTurn = true;
        Debug.Log("playerturn");
        activePlayers = activePlayers.OrderByDescending(x => x.stat.agilityStat.Value).ThenBy(x => x.stat.attackStat.Value).ToList();
        //activePlayers.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
        OnPlayerTurn?.Invoke();
        CommandController.ClearList();
    }

    private void WinPhase()
    {
        winFlag = true;
        if(StageManager.Instance.CurrentRound < StageManager.Instance.CurrentStage.Rounds)
        {
            StageManager.Instance.CurrentRound++;
            StageManager.Instance.StageStart();
        }
        else
        {
            StageManager.Instance.WinStage();
        }
    }

    private void LosePhase()
    {
        loseFlag = true;
        StageManager.Instance.LoseStage();
    }

    public void SetBattle()
    {
        SetPlayer();
        SetEnemy();
        StartCoroutine(AppearAnimTime(GetMaxAnimationTime()));
    }

    private void SetPlayer()
    {
        List<CharacterCarrier> playerCopy = new List<CharacterCarrier>(Players);
        if (playerCopy.Count > 0)
        {
            for (int i = 0; i < playerLocations.Count; i++)
            {
                CharacterCarrier player = playerCopy[i];
                //player.stat.agilityStat.Value = i + 1;

                if (playerLocations[i].isOccupied) continue;

                CharacterCarrier playerUnit = Instantiate(player, playerLocations[i].transform);
                playerLocations[i].isOccupied = true;
                playerUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(playerUnit);
                playerUnit.stat.OnDeath += () => RemoveTarget(playerUnit);
                playerUnit.stat.OnDeath += CheckGameOver;
                activePlayers.Add(playerUnit);
                Players.Remove(player);
            }
        }
    }

    private void SetEnemy()
    {
        List<CharacterCarrier> enemiesCopy = new List<CharacterCarrier>(Enemies);
        if (enemiesCopy.Count > 0)
        {
            for (int i = 0; i < enemyLocations.Count; i++)
            {
                CharacterCarrier enemy = enemiesCopy[i];
                //enemy.stat.agilityStat.Value = i + 1;

                if (enemyLocations[i].isOccupied) continue;

                CharacterCarrier enemyUnit = Instantiate(enemy, enemyLocations[i].transform);
                enemyLocations[i].isOccupied = true;
                enemyUnit.transform.rotation = Quaternion.Euler(0, 180, 0);
                enemyUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(enemyUnit);
                enemyUnit.stat.OnDeath += () => RemoveTarget(enemyUnit);
                enemyUnit.stat.OnDeath += CheckGameOver;
                activeEnemies.Add(enemyUnit);
                Enemies.Remove(enemy);
            }
        }
    }

    public void EmptyPlateOnUnitDeath(CharacterCarrier unit) //Unit OnDeath Action에 추가하기
    {
        unit.GetComponentInParent<UnitPlate>().isOccupied = false;
    }

    private float GetMaxAnimationTime()
    {
        float enemyAnimation = CaluculateMaxAnimationTime(activeEnemies);
        float playerAnimation = CaluculateMaxAnimationTime(activePlayers);

        float maxAnimationTime = enemyAnimation > playerAnimation ? enemyAnimation : playerAnimation;
        return maxAnimationTime;
    }

    private float CaluculateMaxAnimationTime(List<CharacterCarrier> unitList)
    {
        float maxAnimationTime = 0f;

        for (int i = 0; i < unitList.Count; i++)
        {
            float animTime = unitList[i].visual.AppearAnimationLength;
            maxAnimationTime = maxAnimationTime > animTime ? maxAnimationTime : animTime;
        }

        return maxAnimationTime;
    }

    private IEnumerator AppearAnimTime(float animationTime)
    {
        yield return null;
        yield return new WaitForSeconds(animationTime);
        appearAnimComplete = true;
    }

    public void SetTarget()
    {
        if (SelectedTarget)
        {
            CommandController.AddCommand(new SkillCommand(activePlayers[TurnIndex], Targets, SelectedSkill));
            BattleUI.CharacterUI.NextCharacterIcon();
            TurnIndex++;
            Targets.Clear();
        }

        if (TurnIndex == activePlayers.Count)
        {
            BattleUI.CharacterUI.SetActionButton();
        }
    }

    public void RemoveTarget(CharacterCarrier target)
    {
        foreach (SkillCommand command in CommandController.SkillCommands)
        {
            command.targets.Remove(target);
            if (command.skillData.skillSO.isSingleAttack)
            {
                if (command.skillData.skillSO.IsBuff)
                {
                    command.targets.Add(FindNewBuffTarget());
                }
                else
                {
                    command.targets.Add(FindNewAttackTarget());
                }
            }
            if (command.targets.Count == 0)
            {
                CommandController.RemoveCommand(command);
            }
        }
    }

    private CharacterCarrier FindNewAttackTarget()
    {
        if (PlayerTurn)
        {
            if (activeEnemies.Count == 0) return null;
            return activeEnemies[activeEnemies.Count - 1];
        }
        else
        {
            if (activePlayers.Count == 0) return null;
            return activePlayers[activePlayers.Count - 1];
        }
    }

    private CharacterCarrier FindNewBuffTarget()
    {
        if (!PlayerTurn)
        {
            if (activeEnemies.Count == 0) return null;
            return activeEnemies[activeEnemies.Count - 1];
        }
        else
        {
            if (activePlayers.Count == 0) return null;
            return activePlayers[activePlayers.Count - 1];
        }
    }

    void EnemyRandomCommand()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            int randomSkill = UnityEngine.Random.Range(0, activeEnemies[i].skillBook.ActiveSkillList.Length);
            SelectedSkill = activeEnemies[i].skillBook.ActiveSkillList[randomSkill];
            if (SelectedSkill.skillSO.IsBuff)
            {
                if (SelectedSkill.skillSO.isSingleAttack)
                {
                    int randomTarget = UnityEngine.Random.Range(0, activePlayers.Count);
                    Targets.Add(activePlayers[i]);
                }
                else
                {
                    foreach (CharacterCarrier units in activePlayers)
                    {
                        Targets.Add(units);
                    }
                }
            }
            else
            {
                if (SelectedSkill.skillSO.isSingleAttack)
                {
                    int randomTarget = UnityEngine.Random.Range(0, activeEnemies.Count);
                    Targets.Add(activeEnemies[i]);
                }
                else
                {
                    foreach (CharacterCarrier units in activeEnemies)
                    {
                        Targets.Add(units);
                    }
                }
            }

            CommandController.AddCommand(new SkillCommand(activeEnemies[i], Targets, SelectedSkill));
            Targets.Clear();
        }

        CanAttack = true;
    }

    public void OnSkillSelected()
    {
        SelectedTarget = false;
        CanSelectTarget = true;
    }

    private IEnumerator ChangePhase(Action nextPhase)
    {
        yield return new WaitForSeconds(betweenPhaseTime);

        nextPhase();
    }

}
