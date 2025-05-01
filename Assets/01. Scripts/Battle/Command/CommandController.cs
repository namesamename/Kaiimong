using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private List<SkillCommand> skillCommands = new List<SkillCommand>();
    public int Index = 0;

    public Action EndCheck;

    public List<SkillCommand> SkillCommands { get { return skillCommands; } set { skillCommands = value; } }

    public void AddCommand(SkillCommand command)
    {
        skillCommands.Add(command);
        Debug.Log($"Command added: {command}, Total commands: {skillCommands.Count}");
    }

    public IEnumerator ExecuteCommandCoroutine()
    {
        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            if (StageManager.Instance.IsBattleEnd)
                yield break;
            EndCheck.Invoke();
            command.Execute();
            yield return new WaitForSeconds(Wait(command) + 3f);
            RemoveCommand(command);
            if (StageManager.Instance.IsBattleEnd)
                yield break;
            EndCheck.Invoke();
        }

        ClearList();

        if (!StageManager.Instance.IsBattleEnd)
            EndCheck?.Invoke();
    }


    public void Execute()
    {
        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            if (StageManager.Instance.IsBattleEnd)
                break;
            EndCheck.Invoke();
            command.Execute();
            StartCoroutine(Waits(command));
            RemoveCommand(command);
            if (StageManager.Instance.IsBattleEnd)
                break;
            EndCheck.Invoke();

        }
    }

    public IEnumerator Waits(SkillCommand skillCommand)
    {
        if (skillCommand.skillData.skillSO.ID % 3 == 1)
        {
            yield return new WaitForSeconds(skillCommand.unit.visual.GetAnimationLength(3));
                
        }
        else if (skillCommand.skillData.skillSO.ID % 3 == 2)
        {
            yield return new WaitForSeconds(skillCommand.unit.visual.GetAnimationLength(4));
        }
        else
        {
            yield return new WaitForSeconds(skillCommand.unit.visual.GetAnimationLength(5));
        }
    }


        public void ExecuteCommand()
    {
        StartCoroutine(ExecuteCommandCoroutines());
    }

    public IEnumerator ExecuteCommandCoroutines()
    {
        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            if (StageManager.Instance.IsBattleEnd)
                break;

            EndCheck?.Invoke();
            command.Execute();

            // 명령 실행 후 대기 (기존의 대기 시간 처리)
            yield return new WaitForSeconds(Wait(command) + 3f);

            // 명령 삭제
            RemoveCommand(command);

            // 전투 종료 체크
            if (StageManager.Instance.IsBattleEnd)
                break;

            EndCheck?.Invoke();
        }

        // 명령들이 모두 실행된 후 리스트를 비운다.
        ClearList();

        // 전투가 끝나지 않았으면 추가 처리
        if (!StageManager.Instance.IsBattleEnd)
            EndCheck?.Invoke();
    }


    public void ExecuteCommnad()
    {
        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            command.Execute();
            RemoveCommand(command);
        }
    }


    public float Wait(SkillCommand skillCommand)
    {
        if (skillCommand.skillData.skillSO.ID % 3 == 1)
        {
            return skillCommand.unit.visual.GetAnimationLength(3);
        }
        else if (skillCommand.skillData.skillSO.ID % 3 == 2)
        {
            return skillCommand.unit.visual.GetAnimationLength(4);
        }
        else
        {
            return skillCommand.unit.visual.GetAnimationLength(5);
        }

    }


    public void RemoveCommand(SkillCommand command)
    {
        skillCommands.Remove(command);
    }

    public void ClearList()
    {
        skillCommands.Clear();
    }
}
