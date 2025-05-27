using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private List<SkillCommand> skillCommands = new List<SkillCommand>();
    public int Index = 0;
    public bool IsExecutingCommands = false;
    private bool commandsStopped = false;
    public bool CommandsStopped { get { return commandsStopped; } }
    private Coroutine currentExecuteCoroutine = null;

    public Action EndCheck;

    public List<SkillCommand> SkillCommands { get { return skillCommands; } set { skillCommands = value; } }

    public void AddCommand(SkillCommand command)
    {
        skillCommands.Add(command);
        Debug.Log($"Command added: {command}, Total commands: {skillCommands.Count}");
    }

    public IEnumerator ExecuteCommandCoroutine()
    {
        IsExecutingCommands = true;
        commandsStopped = false;


        if (!CurrencyManager.Instance.GetIsTutorial()) 
        {
            TutorialManager.Instance.CurPreDelete();
        }

        // 실행 전 다시 한번 상태 체크
        if (StageManager.Instance.BattleSystem.GetActivePlayers().Count == 0 ||
            StageManager.Instance.BattleSystem.GetActiveEnemies().Count == 0)
        {
            skillCommands.Clear();
            IsExecutingCommands = false;
            EndCheck?.Invoke();
            yield break;
        }

        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            // 커맨드가 중지되었는지 체크
            if (commandsStopped)
            {
                break;
            }

            // 각 명령 실행 전 다시 상태 체크
            if (StageManager.Instance.BattleSystem.GetActivePlayers().Count == 0 ||
                StageManager.Instance.BattleSystem.GetActiveEnemies().Count == 0)
            {
                skillCommands.Clear();
                commandsToExecute.Clear();
                break;
            }

            if (StageManager.Instance.BattleSystem.GetActiveEnemies().Contains(command.targets[0]))
            {
                StageManager.Instance.BattleCamera.ShowEnemy();
            }
            else
            {
                StageManager.Instance.BattleCamera.ShowCharacter();
            }
            yield return command.Execute();
            yield return new WaitForSeconds(1);

            StartCoroutine(StageManager.Instance.BattleSystem.TargetDeathCheck(command.targets));

            RemoveCommand(command);
        }

        IsExecutingCommands = false;
        commandsStopped = false;
    }

    public void StopAllCommands()
    {
        commandsStopped = true;
        ClearList();

        if (currentExecuteCoroutine != null)
        {
            StopCoroutine(currentExecuteCoroutine);
            currentExecuteCoroutine = null;
        }

        IsExecutingCommands = false;
    }

    //public void ExecuteCommnad()
    //{
    //    List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

    //    foreach (SkillCommand command in commandsToExecute)
    //    {
    //        command.Execute();
    //        RemoveCommand(command);
    //    }
    //}


    //public float Wait(SkillCommand skillCommand)
    //{
    //    int id = skillCommand.skillData.SkillSO.ID % 3;
    //    int animIndex = id == 1 ? 3 : id == 2 ? 4 : 5;
    //    return skillCommand.unit.visual.GetAnimationLength(animIndex);
    //}


    public void RemoveCommand(SkillCommand command)
    {
        skillCommands.Remove(command);
    }

    public void ClearList()
    {
        skillCommands.Clear();
    }
}
