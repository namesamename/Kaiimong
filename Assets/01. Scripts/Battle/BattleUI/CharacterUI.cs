using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    private List<DummyUnit> curUnits = new List<DummyUnit>();

    [SerializeField] private List<Image> Icons = new List<Image>();
    [SerializeField] private Button firstSkillButton;
    [SerializeField] private Button secondSkillButton;
    [SerializeField] private Button ultSkillButton;

    [SerializeField] private BattleSystem battleSystem;



    void Start()
    {
        battleSystem.onPlayerTurn += GetActivePlayerUnit;
        battleSystem.onEnemyTurn += GetActiveEnemyUnit;
    }

    void Update()
    {
        
    }

    public void GetActivePlayerUnit()
    {
        curUnits = battleSystem.GetActivePlayers();
    }

    public void GetActiveEnemyUnit()
    {
        curUnits = battleSystem.GetActiveEnemies();
    }

    public void SetUI()
    {
        for(int i = 0; i < curUnits.Count; i++)
        {
            Icons[i].sprite = curUnits[i].icon;
        }
    }
}
