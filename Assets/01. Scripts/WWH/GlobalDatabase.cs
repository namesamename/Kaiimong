using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDatabase : Singleton<GlobalDatabase>
{

    public CharacterDataBase character;
    public SkillDataBase skill;
    private void Awake()
    {
        character = GetComponentInChildren<CharacterDataBase>();
        skill = GetComponentInChildren<SkillDataBase>();
    }


}
