using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandController : MonoBehaviour
{
    [SerializeField] private List<DummySkill> skillCommands = new List<DummySkill>();
    public int index = 0;

    public Action newTurn;

    public void AddCommand(ICommand command)
    {
        skillCommands.Add(command as DummySkill);
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
