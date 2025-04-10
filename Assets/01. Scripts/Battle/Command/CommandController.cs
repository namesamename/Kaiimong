using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private List<DummySkill> skillCommands = new List<DummySkill>();
    public int Index = 0;

    public List<DummySkill> SkillCommands { get { return skillCommands; } }

    public Action newTurn;

    public void AddCommand(ICommand command)
    {
        skillCommands.Add(command as DummySkill);
        Debug.Log($"Command added: {command}, Total commands: {skillCommands.Count}");
    }

    public void ExecuteCommand()
    {
        foreach (ICommand command in skillCommands)
        {
            command.Execute(); 
        }
    }

    public void RemoveCommand(ICommand command)
    {
        skillCommands.Remove(command as DummySkill);
    }

    void ClearList()
    {
        skillCommands.Clear();
    }

    void Start()
    {        
        newTurn += ClearList;
    }

    void Update()
    {
        
    }
}
