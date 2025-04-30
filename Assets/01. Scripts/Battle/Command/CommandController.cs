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

    //public IEnumerator ExecuteCommandCoroutine()
    //{
    //    List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

    //    foreach (SkillCommand command in commandsToExecute)
    //    {
    //        command.Execute();
    //        yield return new WaitForSeconds(Wait(command) + 3f);
    //        EndCheck.Invoke();
    //        RemoveCommand(command);
    //    }
    //}


    public void ExecuteCommnad()
    {
        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            command.Execute();
            RemoveCommand(command);
        }
    }
    

    //public float Wait(SkillCommand skillCommand)
    //{
    //    if (skillCommand.skillData.skillSO.ID % 3 == 1)
    //    {
    //        return skillCommand.unit.visual.GetAnimationLength(3);
    //    }
    //    else if (skillCommand.skillData.skillSO.ID % 3 == 2)
    //    {
    //        return skillCommand.unit.visual.GetAnimationLength(4);
    //    }
    //    else
    //    {
    //        return skillCommand.unit.visual.GetAnimationLength(5);
    //    }

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
