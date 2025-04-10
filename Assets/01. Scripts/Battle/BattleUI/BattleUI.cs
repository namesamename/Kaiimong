using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private CharacterUI characterUI;
    private BattleSystem battleSystem;

    public BattleSystem BattleSystem { get { return battleSystem; } set { battleSystem = value; } }
    public CharacterUI CharacterUI { get { return characterUI; } }

    private void Start()
    {
        characterUI.BattleSystem = battleSystem;
    }

}
