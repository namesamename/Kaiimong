using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private List<SkillCommand> skillCommands = new List<SkillCommand>();
    public int Index = 0;

    public List<SkillCommand> SkillCommands { get { return skillCommands; } set { skillCommands = value; } }

    public void AddCommand(SkillCommand command)
    {
        skillCommands.Add(command);
        Debug.Log($"Command added: {command}, Total commands: {skillCommands.Count}");
    }

    public void ExecuteCommand()
    {
        List<SkillCommand> commandsToExecute = new List<SkillCommand>(skillCommands);

        foreach (SkillCommand command in commandsToExecute)
        {
            command.Execute(); 
            RemoveCommand(command);
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
