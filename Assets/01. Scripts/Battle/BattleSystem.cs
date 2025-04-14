using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<Character> Enemies; //스테이지 데이터에서 가져오고
    [SerializeField] private List<Character> activePlayers = new List<Character>();    //현재 배치중인 유닛들 정보
    [SerializeField] private List<Character> activeEnemies = new List<Character>();
    public List<Character> GetActivePlayers() => activePlayers;
    public List<Character> GetActiveEnemies() => activeEnemies;

    [Header("BattleInfo")]
    public int TurnIndex = 0;
    public SkillObject SelectedSkill;
    public List<Character> Targets;
    private float betweenPhaseTime;

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
    public BattleUI BattleUI;


    public Action OnPlayerTurn;
    public Action OnEnemyTurn;
    public Action SkillChanged;

    private void Awake()
    {
        CommandController = GetComponent<CommandController>();
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

    private void AttackPhase()
    {
        CommandController.ExecuteCommand();

        CheckGameOver();

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

    private void StartBattle()
    {
        activePlayers.Clear();
        activeEnemies.Clear();
        SetBattle();
    }

    private void EnemyTurnPhase()
    {
        PlayerTurn = false;
        Debug.Log("enemyturn");
        activeEnemies.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
        CommandController.ClearList();
        EnemyRandomCommand();
    }

    private void PlayerTurnPhase()
    {
        PlayerTurn = true;
        Debug.Log("playerturn");
        activePlayers.Sort((a, b) => b.stat.agilityStat.Value.CompareTo(a.stat.agilityStat.Value));
        OnPlayerTurn?.Invoke();
        CommandController.ClearList();
    }

    private void WinPhase()
    {

    }

    private void LosePhase()
    {

    }

    public void SetBattle()
    {
        SetPlayer();
        SetEnemy();
        StartCoroutine(AppearAnimTime(GetMaxAnimationTime()));
    }

    private void SetPlayer()
    {
        List<Character> playerCopy = new List<Character>(Players);
        if (playerCopy.Count > 0)
        {
            for (int i = 0; i < playerLocations.Count; i++)
            {
                Character player = playerCopy[i];
                //player.stat.agilityStat.Value = i + 1;

                if (playerLocations[i].isOccupied) continue;

                Character playerUnit = Instantiate(player, playerLocations[i].transform);
                playerLocations[i].isOccupied = true;
                playerUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(playerUnit);
                activePlayers.Add(playerUnit);
                Players.Remove(player);
            }
        }
    }

    private void SetEnemy()
    {
        List<Character> enemiesCopy = new List<Character>(Enemies);
        if (enemiesCopy.Count > 0)
        {
            for (int i = 0; i < enemyLocations.Count; i++)
            {
                Character enemy = enemiesCopy[i];
                //enemy.stat.agilityStat.Value = i + 1;

                if (enemyLocations[i].isOccupied) continue;

                Character enemyUnit = Instantiate(enemy, enemyLocations[i].transform);
                enemyLocations[i].isOccupied = true;
                enemyUnit.transform.rotation = Quaternion.Euler(0, 180, 0);
                enemyUnit.stat.OnDeath += () => EmptyPlateOnUnitDeath(enemyUnit);
                activeEnemies.Add(enemyUnit);
                Enemies.Remove(enemy);
            }
        }
    }

    public void EmptyPlateOnUnitDeath(Character unit) //Unit OnDeath Action에 추가하기
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

    private float CaluculateMaxAnimationTime(List<Character> unitList)
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

    void EnemyRandomCommand()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            int randomSkill = UnityEngine.Random.Range(0, activeEnemies[i].skillBook.SkillList.Length);
            SelectedSkill = activeEnemies[i].skillBook.SkillList[randomSkill];
            if (SelectedSkill.skillSO.IsBuff)
            {
                if (SelectedSkill.skillSO.isSingleAttack)
                {
                    int randomTarget = UnityEngine.Random.Range(0, activePlayers.Count);
                    Targets.Add(activePlayers[i]);
                }
                else
                {
                    foreach (Character units in activePlayers)
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
                    foreach (Character units in activeEnemies)
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
