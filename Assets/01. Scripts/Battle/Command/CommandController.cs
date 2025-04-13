using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private List<DummySkill> skillCommands = new List<DummySkill>();
    public int Index = 0;

    public List<DummySkill> SkillCommands { get { return skillCommands; } }

    public void AddCommand(ICommand command)
    {
        skillCommands.Add(command as DummySkill);
        Debug.Log($"Command added: {command}, Total commands: {skillCommands.Count}");
    }

    public void ExecuteCommand()
    {
        List<DummySkill> commandsToExecute = new List<DummySkill>(skillCommands);

        foreach (ICommand command in commandsToExecute)
        {
            command.Execute(); 
            RemoveCommand(command);
        }
    }

    public void RemoveCommand(ICommand command)
    {
        skillCommands.Remove(command as DummySkill);
    }

    public void ClearList()
    {
        skillCommands.Clear();
    }
}
