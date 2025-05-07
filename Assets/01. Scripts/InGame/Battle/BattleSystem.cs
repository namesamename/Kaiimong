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
    public List<Character> Players; //캐릭터 선택에서 가져오고
    public List<Enemy> Enemies; //스테이지 데이터에서 가져오고
    [SerializeField] private List<CharacterCarrier> activePlayers = new List<CharacterCarrier>();    //현재 배치중인 유닛들 정보
    [SerializeField] private List<CharacterCarrier> activeEnemies = new List<CharacterCarrier>();
    public List<CharacterCarrier> GetActivePlayers() => activePlayers;
    public List<CharacterCarrier> GetActiveEnemies() => activeEnemies;

    [Header("BattleInfo")]
    public int TurnIndex = 0;
    public ActiveSkillObject SelectedSkill;
    public List<CharacterCarrier> Targets;
    private float betweenPhaseTime = 1f;
    public bool winFlag = false;
    public bool loseFlag = false;
    public int CurrentSet = 1;
    private bool canStartNextRound = false;

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

    private bool isPhaseChanging = false;

    private void Awake()
    {
        CommandController = GetComponent<CommandController>();
        CharacterSelector = GetComponent<CharacterSelector>();
        CharacterSelector.battleSystem = this;
        //StageManager.Instance.BringBattleSystem(this);
        SetUI();
    }

    void Start()
    {
        //StartBattle();
        CommandController.EndCheck += CheckGameOver;
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
        //character.stat.OnDeath -= CheckGameOver;
        character.stat.OnDeath -= () => EmptyPlateOnUnitDeath(character);
        character.stat.OnDeath -= () => RemoveTarget(character);
        character.stat.OnDeath -= () => CharacterDeath(character);
    }

    public void SetUI()
    {
        GameObject canvas = GameObject.Find("Canvas");
        GameObject uiPrefab = Instantiate(Resources.Load("UI/Battle/BattleUI")) as GameObject;
        uiPrefab.transform.SetParent(canvas.transform, false);
        BattleUI = uiPrefab.GetComponent<BattleUI>();
        BattleUI.BattleSystem = this;
        BattleUI.CharacterUI.BattleSystem = this;
        StageManager.Instance.EndUISet();
    }

    private void AttackPhase()
    {
        StartCoroutine(AttackPhaseCoroutine());
    }

    private IEnumerator AttackPhaseCoroutine()
    {
        //CommandController.ExecuteCommnad();
        yield return StartCoroutine(CommandController.ExecuteCommandCoroutine());

        if (PlayerTurn)
        {
            CheckGameOver();
            if (!isPhaseChanging)
            {
                StartCoroutine(ChangePhase(EnemyTurnPhase));
            }
        }
        else
        {
            SetBattle();
            if (appearAnimComplete)
            {
                CheckGameOver();
                if (!isPhaseChanging)
                {
                    StartCoroutine(ChangePhase(PlayerTurnPhase));
                }
            }
        }

    }

    public void BattleEndCheck()
    {
        if (PlayerTurn)
        {
            CheckGameOver();
            if (!isPhaseChanging)
            {
                StartCoroutine(ChangePhase(EnemyTurnPhase));
            }
        }
        else
        {
            SetBattle();
            if (appearAnimComplete)
            {
                CheckGameOver();
                if (!isPhaseChanging)
                {
                    StartCoroutine(ChangePhase(PlayerTurnPhase));
                }
            }
        }
    }

    private void CheckGameOver()
    {
        if (isPhaseChanging) return;

        isPhaseChanging = true;

        bool isExecutingCommands = CommandController.IsExecutingCommands;

        if (Players.Count == 0 && activePlayers.Count == 0)
        {
            // 플레이어가 모두 죽었을 때 (패배)
            if (isExecutingCommands)
            {
                Debug.Log("모든 플레이어가 죽음: 현재 실행 중인 커맨드 중단");
                CommandController.StopAllCommands();
            }
            StartCoroutine(ChangePhase(() => { isPhaseChanging = false; LosePhase(); }));
            return;
        }

        if (Enemies.Count == 0 && activeEnemies.Count == 0)
        {
            // 적이 모두 죽었을 때
            if (isExecutingCommands)
            {
                Debug.Log("모든 적이 죽음: 현재 실행 중인 커맨드 중단");
                CommandController.StopAllCommands();
            }

            if (StageManager.Instance.CurrentRound < StageManager.Instance.CurrentStage.Rounds)
            {
                // 다음 라운드가 있는 경우
                PlayerTurn = false;
                StartCoroutine(ChangePhase(() => { isPhaseChanging = false; NextRoundPhase(); }));
            }
            else
            {
                // 마지막 라운드인 경우 (승리)
                StartCoroutine(ChangePhase(() => { isPhaseChanging = false; WinPhase(); }));
            }
            return;
        }
        isPhaseChanging = false;
    }

    public void StartBattle()
    {
        activePlayers.Clear();
        activeEnemies.Clear();
        SetBattle();
    }

    private void EnemyTurnPhase()
    {
        if (PlayerTurn)
        {
            Debug.Log("enemyturn");
            activeEnemies = activeEnemies.OrderByDescending(x => x.stat.agilityStat.Value).ThenBy(x => x.stat.attackStat.Value).ToList();
            //activeEnemies.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
            CommandController.ClearList();
            EnemyRandomCommand();
        }
        PlayerTurn = false;
    }

    private void PlayerTurnPhase()
    {
        if (!PlayerTurn)
        {
            Debug.Log("playerturn");
            activePlayers = activePlayers.OrderByDescending(x => x.stat.agilityStat.Value).ThenBy(x => x.stat.attackStat.Value).ToList();
            //activePlayers.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
            OnPlayerTurn?.Invoke();
            CommandController.ClearList();
        }
        PlayerTurn = true;
    }

    private void WinPhase()
    {

        winFlag = true;
        StageManager.Instance.WinStage();

    }

    private void NextRoundPhase()
    {
        StartCoroutine(NextRoundPhaseCoroutine());
    }

    private IEnumerator NextRoundPhaseCoroutine()
    {
        yield return new WaitForSeconds(1f);

        if (StageManager.Instance.CurrentRound < StageManager.Instance.CurrentStage.Rounds)
        {
            winFlag = false;
            if (canStartNextRound)
            {
                canStartNextRound = false;
                StageManager.Instance.CurrentRound++;
                if (StageManager.Instance.CurrentRound == StageManager.Instance.CurrentStage.Rounds) winFlag = true;
                StageManager.Instance.StageStart();
            }
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
        canStartNextRound = true;
        StartCoroutine(AppearAnimTime(GetMaxAnimationTime()));
    }

    private void SetPlayer()
    {
        List<Character> playerCopy = new List<Character>(Players);
        if (playerCopy.Count > 0)
        {
            for (int i = 0; i < playerLocations.Count; i++)
            {
                if (i < playerCopy.Count)
                {
                    Character player = playerCopy[0];
                    //player.stat.agilityStat.Value = i + 1;

                    if (playerLocations[i].isOccupied) continue;

                    GameObject playerObject = GlobalDataTable.Instance.character.CharacterInstanceSummon(player, Vector3.zero);
                    playerObject.transform.SetParent(playerLocations[i].transform, false);
                    CharacterCarrier playerUnit = playerObject.GetComponent<CharacterCarrier>();
                    playerUnit.visual.SetSprite(SpriteType.BattleSprite);
                    playerUnit.Initialize(GlobalDataTable.Instance.DataCarrier.GetCharacterIDToIndex(i));
                    playerLocations[i].isOccupied = true;
                    playerUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(playerUnit);
                    playerUnit.stat.OnDeath += () => RemoveTarget(playerUnit);
                    playerUnit.stat.OnDeath += () => CharacterDeath(playerUnit);
                    //playerUnit.stat.OnDeath += CheckGameOver;
                    activePlayers.Add(playerUnit);
                    Players.RemoveAt(0);
                }
                else continue;
            }
        }
    }

    private void SetEnemy()
    {
        List<Enemy> enemiesCopy = new List<Enemy>(Enemies);
        if (enemiesCopy.Count > 0)
        {
            for (int i = 0; i < enemyLocations.Count; i++)
            {
                if (i < enemiesCopy.Count)
                {
                    Enemy enemy = enemiesCopy[0];
                    //enemy.stat.agilityStat.Value = i + 1;

                    if (enemyLocations[i].isOccupied) continue;

                    List<EnemySpawn> curEnemySpawnList = GlobalDataTable.Instance.EnemySpawn.EnemySpawnDic[StageManager.Instance.CurrentStage.ID];
                    EnemySpawn curEnemySpawn = curEnemySpawnList.FirstOrDefault(x => x.Round == StageManager.Instance.CurrentRound && x.Set == CurrentSet);
                    int enemyLevel = curEnemySpawn.Level;
                    GameObject enemyObject = GlobalDataTable.Instance.character.EnemyInstanceSummon(enemy, enemyLevel, Vector3.zero);
                    enemyObject.transform.SetParent(enemyLocations[i].transform, false);
                    CharacterCarrier enemyUnit = enemyObject.GetComponent<CharacterCarrier>();
                    enemyLocations[i].isOccupied = true;
                    enemyUnit.transform.rotation = Quaternion.Euler(0, 180, 0);
                    enemyUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(enemyUnit);
                    enemyUnit.stat.OnDeath += () => RemoveTarget(enemyUnit);
                    enemyUnit.stat.OnDeath += () => EnemyDeath(enemyUnit);
                    //enemyUnit.stat.OnDeath += CheckGameOver;
                    activeEnemies.Add(enemyUnit);
                    Enemies.RemoveAt(0);
                }
                else continue;

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
        foreach (SkillCommand command in CommandController.SkillCommands.ToList())
        {
            command.targets.Remove(target);
            if (command.skillData.SkillSO.isSingleAttack)
            {
                if (command.skillData.SkillSO.IsBuff)
                {
                    var newBuffTarget = FindNewBuffTarget();
                    command.targets.Add(newBuffTarget);
                }
                else
                {
                    var newAttackTarget = FindNewAttackTarget();
                    command.targets.Add(newAttackTarget);
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
            if (SelectedSkill.SkillSO.IsBuff)
            {
                if (SelectedSkill.SkillSO.isSingleAttack)
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
            else
            {
                if (SelectedSkill.SkillSO.isSingleAttack)
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

        nextPhase?.Invoke();
    }

    private void CharacterDeath(CharacterCarrier character)
    {
        UnSubscribeCharacterDeathAction(character);
        activePlayers.Remove(character);
        Destroy(character.gameObject);
    }

    private void EnemyDeath(CharacterCarrier enemy)
    {
        UnSubscribeCharacterDeathAction(enemy);
        activeEnemies.Remove(enemy);
        Destroy(enemy.gameObject);


        if (activeEnemies.Count == 0 && Enemies.Count == 0)
        {
            Debug.Log("All enemies defeated, checking game state");
            CheckGameOver();
        }
    }
}
