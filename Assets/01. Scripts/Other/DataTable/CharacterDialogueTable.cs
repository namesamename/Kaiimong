using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDialogueTable 
{
    Dictionary<int, CharacterDialogue> DialogueDic = new Dictionary<int, CharacterDialogue>();



    public void Initialize()
    {
        CharacterDialogue[] characters = Resources.LoadAll<CharacterDialogue>("CharacterDialogue");
        for (int i = 0; i < characters.Length; i++) 
        {
            DialogueDic[characters[i].ID] = characters[i];
        }
    }



    public CharacterDialogue GetDialogue(int ID)
    {
        if(DialogueDic.ContainsKey(ID) && DialogueDic[ID] != null)
        {
            return DialogueDic[ID];
        }
        else
        {
            return null;
        }
    }
}
