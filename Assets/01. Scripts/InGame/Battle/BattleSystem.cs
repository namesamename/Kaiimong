using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;


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
    [SerializeField] private float betweenPhaseTime = 0.5f;
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
    private bool sortedOnce = false;
    private bool settingBattle = false;

    public BattleCamera BattleCamera;

    public CommandController CommandController { get; private set; }
    public CharacterSelector CharacterSelector { get; private set; }
    public BattleUI BattleUI;

    List<UIBattleHealthBar> BattleHealthBars = new List<UIBattleHealthBar> { };

    public Transform CanvasTrans;

    public GameObject HealthPrefab;
    public Action OnPlayerTurn;
    public Action OnEnemyTurn;
    public Action SkillChanged;



    private bool isPhaseChanging = false;

    private void Awake()
    {
        CommandController = GetComponent<CommandController>();
        CharacterSelector = GetComponent<CharacterSelector>();
        CharacterSelector.battleSystem = this;
        HealthPrefab = Resources.Load<GameObject>("UI/Battle/HealthBar");
        CanvasTrans = FindAnyObjectByType<Canvas>().transform;
        //StageManager.Instance.BringBattleSystem(this);
        SetUI();
    }

    

    void Start()
    {
        //StartBattle();
        CommandController.EndCheck += CheckGameOver;
        BattleCamera = StageManager.Instance.BattleCamera;

    }

    void Update()
    {

        if (appearAnimComplete)
        {

            appearAnimComplete = false;
            PlayerTurn = true;
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
        // 기존 BattleUI 제거
        BattleUI[] existingBattleUIs = FindObjectsOfType<BattleUI>();
        foreach (BattleUI existingUI in existingBattleUIs)
        {
            if (existingUI != null && existingUI != BattleUI)
            {
                Destroy(existingUI.gameObject);
            }
        }

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
        UpdateHealthUI();

        BattleCamera.ShowMainView();

        if (PlayerTurn)
        {
            CheckGameOver();
            if (!isPhaseChanging)
            {
                StartCoroutine(ChangePhase(PlayerTurnPhase));
                if (!CurrencyManager.Instance.GetIsTutorial())
                {
                    if(TutorialManager.Instance.GetIndex() != 11)
                    {
                        TutorialManager.Instance.NextTutorial();
                    }
                  
                }
            }
        }
        else
        {

            if (settingBattle)
            {
                SetBattle();
                if (appearAnimComplete)
                {
                    CheckGameOver();
                    if (!isPhaseChanging)
                    {
                        StartCoroutine(ChangePhase(EnemyTurnPhase));
                    }
                }
            }
            else
            {
                CheckGameOver();
                if (!isPhaseChanging)
                {
                    StartCoroutine(ChangePhase(EnemyTurnPhase));
                }
            }
        }

    }

    //public void BattleEndCheck()
    //{
    //    if (PlayerTurn)
    //    {
    //        CheckGameOver();
    //        if (!isPhaseChanging)
    //        {
    //            StartCoroutine(ChangePhase(PlayerTurnPhase));
    //        }
    //    }
    //    else
    //    {
    //        SetBattle();
    //        if (appearAnimComplete)
    //        {
    //            CheckGameOver();
    //            if (!isPhaseChanging)
    //            {
    //                StartCoroutine(ChangePhase(EnemyTurnPhase));
    //            }
    //        }
    //    }
    //}

    private void CheckGameOver()
    {
        if (isPhaseChanging) return;

        isPhaseChanging = true;
        Debug.Log(isPhaseChanging);
        bool isExecutingCommands = CommandController.IsExecutingCommands;

        if (Players.Count == 0 && activePlayers.Count == 0)
        {
            // 플레이어가 모두 죽었을 때 (패배)
            if (isExecutingCommands)
            {
                Debug.Log("모든 플레이어가 죽음: 현재 실행 중인 커맨드 중단");
                CommandController.StopAllCommands();
            }
            DestroyHealthBar();
            StartCoroutine(ChangePhase(() => { isPhaseChanging = false; LosePhase(); }));
            return;
        }

        if (Enemies.Count == 0 && activeEnemies.Count == 0)
        {
            PlayerTurn = false;
            sortedOnce = false;
            Debug.Log("wjrl");
            BattleUI.CharacterUI.PlayerTurnEnd();
            // 적이 모두 죽었을 때
            if (isExecutingCommands)
            {
                Debug.Log("모든 적이 죽음: 현재 실행 중인 커맨드 중단");
                CommandController.StopAllCommands();
            }

            if (StageManager.Instance.CurrentRound < StageManager.Instance.CurrentStage.Rounds)
            {
                // 다음 라운드가 있는 경우
                PlayerTurn = true;
                StartCoroutine(ChangePhase(() => { isPhaseChanging = false; NextRoundPhase(); }));
            }
            else
            {
                // 마지막 라운드인 경우 (승리)
                StartCoroutine(ChangePhase(() => { isPhaseChanging = false; WinPhase(); }));
                DestroyHealthBar();


                QuestManager.Instance.QuestTypeValueUP(1, QuestType.StageClear);
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
        if (StageManager.Instance.CurrentTurn == 20)
        {
            StartCoroutine(ChangePhase(() => { isPhaseChanging = false; LosePhase(); }));
        }
        if (!PlayerTurn)
        {
            PlayerTurn = true;
            Debug.Log("enemyturn");
            activeEnemies = activeEnemies.OrderByDescending(x => x.stat.agilityStat.Value).ThenBy(x => x.stat.attackStat.Value).ToList();
            if( activeEnemies.Count > 0 && activeEnemies.Count >0)
            {
                foreach (CharacterCarrier asd in activePlayers)
                {
                    asd.stat.OnTurnEnd();
                }
                foreach (CharacterCarrier asd in activeEnemies)
                {
                    asd.stat.OnTurnEnd();
                }

            }
     
            //activeEnemies.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
            CommandController.ClearList();
            EnemyRandomCommand();
        }
    }

    private void PlayerTurnPhase()
    {
        if (PlayerTurn)
        {
            if (!sortedOnce)
            {
                sortedOnce = true;
                activePlayers = activePlayers.OrderByDescending(x => x.stat.agilityStat.Value).ThenBy(x => x.stat.attackStat.Value).ToList();
                StageManager.Instance.CurrentTurn++;
                BattleUI.SetUI();
                if (activePlayers.Count > 0 && activeEnemies.Count > 0)
                {
   
                    foreach (CharacterCarrier asd in activePlayers)
                    {
                        asd.stat.OnTurnEnd();
                    }
                    foreach (CharacterCarrier asd in activeEnemies)
                    {
                        asd.stat.OnTurnEnd();
                    }
                }
         


                //activePlayers.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
                OnPlayerTurn?.Invoke();
                CommandController.ClearList();
                //BattleCamera.ShowCharacter(activePlayers[0].gameObject);
            }
            BattleUI.CharacterUI.SkillButtonAble();
        }
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
                BattleUI.SetUI();
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
        settingBattle = false;
        StartCoroutine(AppearAnimTime(GetMaxAnimationTime()));
    }

    private void SetPlayer()
    {
        

        for (int i = BattleHealthBars.Count - 1; i >= 0; i--)
        {
            var bar = BattleHealthBars[i];
            if (bar != null && bar.carrier != null && bar.carrier.stat.healthStat.CurHealth <= 0)
            {
                Destroy(bar.gameObject);
                BattleHealthBars.RemoveAt(i);
            }
            else if (bar == null)
            {
                BattleHealthBars.RemoveAt(i);
            }
        }
        BattleHealthBars.Clear();
        List<Character> playerCopy = new List<Character>(Players);
        if (playerCopy.Count > 0)
        {
            for (int i = 0; i < playerLocations.Count; i++)
            {
                if (i < playerCopy.Count)
                {
                    Character player = playerCopy[i];
                    //player.stat.agilityStat.Value = i + 1;

                    if (playerLocations[i].isOccupied) continue;

                    GameObject playerObject = GlobalDataTable.Instance.character.CharacterInstanceSummon(player, Vector3.zero);
                    playerObject.transform.SetParent(playerLocations[i].transform, false);
                    CharacterCarrier playerUnit = playerObject.GetComponent<CharacterCarrier>();

                    GameObject gameObject = Instantiate(HealthPrefab, CanvasTrans, false);
                    gameObject.GetComponent<UIBattleHealthBar>().SetUI(playerUnit, CharacterType.Friend);
                    BattleHealthBars.Add(gameObject.GetComponent<UIBattleHealthBar>());

                    playerUnit.visual.SetSprite(SpriteType.BattleSprite);
                    playerUnit.Initialize(GlobalDataTable.Instance.DataCarrier.GetCharacterIDToIndex(i));
                    playerLocations[i].isOccupied = true;
                    playerUnit.stat.OnDeath += () => OnPlayerDeath(playerUnit, gameObject);
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
                    Enemy enemy = enemiesCopy[i];
                    //enemy.stat.agilityStat.Value = i + 1;

                    if (enemyLocations[i].isOccupied) continue;

                    List<EnemySpawn> curEnemySpawnList = GlobalDataTable.Instance.EnemySpawn.EnemySpawnDic[StageManager.Instance.CurrentStage.ID];
                    EnemySpawn curEnemySpawn = curEnemySpawnList.FirstOrDefault(x => x.Round == StageManager.Instance.CurrentRound && x.Set == CurrentSet);
                    int enemyLevel = curEnemySpawn.Level;
                    GameObject enemyObject = GlobalDataTable.Instance.character.EnemyInstanceSummon(enemy, enemyLevel, Vector3.zero);
                    enemyObject.transform.SetParent(enemyLocations[i].transform, false);
                    CharacterCarrier enemyUnit = enemyObject.GetComponent<CharacterCarrier>();

                    GameObject gameObject = Instantiate(HealthPrefab, CanvasTrans, false);
                    gameObject.GetComponent<UIBattleHealthBar>().SetUI(enemyUnit, CharacterType.Enemy);
                    BattleHealthBars.Add(gameObject.GetComponent<UIBattleHealthBar>());
                    enemyLocations[i].isOccupied = true;
                    enemyUnit.transform.rotation = Quaternion.Euler(0, 180, 0);
                    enemyUnit.stat.OnDeath += () => OnPlayerDeath(enemyUnit, gameObject);
                    enemyUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(enemyUnit);
                    enemyUnit.stat.OnDeath += () => RemoveTarget(enemyUnit);
                    enemyUnit.stat.OnDeath += () => EnemyDeath(enemyUnit);
        
                    enemyUnit.stat.OnDeath += () => QuestManager.Instance.QuestTypeValueUP(1, QuestType.KillMonster);
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
            if(TurnIndex < activePlayers.Count)
  
            Targets.Clear();
            //BattleUI.CharacterUI.SetActionButton();
        }

        if (TurnIndex == activePlayers.Count)
        {
  
            PlayerTurn = false;
            sortedOnce = false;
            BattleUI.CharacterUI.PlayerTurnEnd();
        }
        //BattleCamera.ShowCharacter(activePlayers[TurnIndex].gameObject);
        CanAttack = true;
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
            Targets.Clear();
  
            if (SelectedSkill.SkillSO.IsBuff)
            {
           
                if (SelectedSkill.SkillSO.isSingleAttack)
                {
                    if (activeEnemies.Count > 0)
                    {
                        int randomTarget = UnityEngine.Random.Range(0, activeEnemies.Count);
                        Targets.Add(activeEnemies[randomTarget]);
                    }
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
          
                if (activePlayers.Count == 0)
                {
               
                    continue;
                }

                if (SelectedSkill.SkillSO.isSingleAttack)
                {
                    int randomTarget = UnityEngine.Random.Range(0, activePlayers.Count);
                    Targets.Add(activePlayers[randomTarget]);
                }
                else
                {
                    foreach (CharacterCarrier units in activePlayers)
                    {
                        Targets.Add(units);
               
                    }
                }
            }
            List<CharacterCarrier> targetsCopy = new List<CharacterCarrier>(Targets);
            CommandController.AddCommand(new SkillCommand(activeEnemies[i], targetsCopy, SelectedSkill));
        }
        UpdateHealthUI();
        CanAttack = true;
    }


    public void OnDestroy()
    {
        foreach (var bar in BattleHealthBars)
        {
            if (bar != null)
                Destroy(bar.gameObject);
        }
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
        if (Enemies.Count > 0)
        {
            settingBattle = true;
        }
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

    public IEnumerator TargetDeathCheck(List<CharacterCarrier> targets)
    {
        float maxWaitTime = 0f;
        List<CharacterCarrier> deadCharacter = new List<CharacterCarrier>();

        foreach (CharacterCarrier target in targets)
        {
            if (target.stat.healthStat.CurHealth <= 0)
            {
                deadCharacter.Add(target);
            }
            float waitTime = target.visual.GetAnimationLength(6);
            maxWaitTime = Mathf.Max(maxWaitTime, waitTime);
        }

        foreach (CharacterCarrier dead in deadCharacter)
        {
            dead.stat.OnDie();
        }

        yield return new WaitForSeconds(maxWaitTime + 0.5f);
    }

    public void UpdateHealthUI()
    {
        foreach(UIBattleHealthBar character in BattleHealthBars) 
        {
            character.UpdataHealthBar();
        }
    }

    public void DestroyHealthBar()
    {
        foreach (UIBattleHealthBar character in BattleHealthBars)
        {
            character.DestroyThis();
        }
    }

    public void OnPlayerDeath(CharacterCarrier unit, GameObject hpBar)
    {
        if (hpBar != null)
        {
            hpBar.GetComponent<UIBattleHealthBar>().DestroyThis();
        }

    }


}
