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
            command.Execute();
            yield return new WaitForSeconds(Wait(command) + 3f);
            EndCheck.Invoke();
            RemoveCommand(command);
        }
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


    public float Wait(SkillCommand skillCommand)
    {
        int id = skillCommand.skillData.SkillSO.ID % 3;
        int animIndex = id == 1 ? 3 : id == 2 ? 4 : 5;
        return skillCommand.unit.visual.GetAnimationLength(animIndex);
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
