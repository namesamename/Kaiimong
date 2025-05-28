using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterSelector : MonoBehaviour
{
    private CharacterCarrier selectedCharacter = null;
    public BattleSystem battleSystem;
    private Camera _camera;

    public bool initialTarget = false;

    private void Awake()
    {
        _camera = Camera.main;
        initialTarget = false;
    }

    private void Start()
    {
        battleSystem.BattleUI.CharacterUI.OnConfirmButton += OnConfirmButtonClicked;
        battleSystem.SkillChanged += ResetEffect;
        battleSystem.SkillChanged += ResetSelectedCharacter;
    }

    public void UnSubscribeCharacterSelector()
    {
        battleSystem.BattleUI.CharacterUI.OnConfirmButton -= OnConfirmButtonClicked;
        battleSystem.SkillChanged -= ResetEffect;
        battleSystem.SkillChanged -= ResetSelectedCharacter;
    }

    private void Update()
    {
        if (battleSystem.CanSelectTarget)
        {
            if (!battleSystem.SelectedSkill.SkillSO.IsBuff)
            {
                if (!battleSystem.SelectedSkill.SkillSO.isSingleAttack)
                {
                    SelectAll(battleSystem.GetActiveEnemies());
                }
                else
                {
                    MouseClick(battleSystem.GetActiveEnemies());
                }
            }
            else
            {
                Debug.Log("이거 힐인데");

                foreach (CharacterCarrier character in battleSystem.GetActivePlayers())
                {
                    Debug.Log(character);
                }

                if (!battleSystem.SelectedSkill.SkillSO.isSingleAttack)
                {
                    SelectAll(battleSystem.GetActivePlayers());
                }
                else
                {
                    MouseClick(battleSystem.GetActivePlayers());
                }
            }
        }
    }

    private void SelectAll(List<CharacterCarrier> units)
    {
        if (!battleSystem.SelectedSkill.SkillSO.isSingleAttack)
        {
            foreach (CharacterCarrier unit in units)
            {
                SelectedEffect(unit, true);
                selectedCharacter = unit;
            }
            battleSystem.BattleUI.CharacterUI.ActionButtonAble();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                CharacterCarrier unit = hit.collider.GetComponent<CharacterCarrier>();
                if (unit != null && battleSystem.GetActiveEnemies().Contains(unit))
                {
                    OnConfirm();
                }
            }
        }
    }

    private void MouseClick(List<CharacterCarrier> units)
    {
        if (!initialTarget)
        {
            initialTarget = true;
            CharacterCarrier character = units[0];
            selectedCharacter = character;
            SelectedEffect(character, true);
            battleSystem.BattleUI.CharacterUI.ActionButtonAble();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                CharacterCarrier unit = hit.collider.GetComponent<CharacterCarrier>();
                if (unit != null && units.Contains(unit))
                {
                    battleSystem.BattleUI.CharacterUI.ActionButtonAble();
                    OnCharacterClicked(unit);
                }
            }
        }
    }

    public void OnCharacterClicked(CharacterCarrier character)
    {
        //첫 클릭,선택
        if (selectedCharacter == null)
        {
            selectedCharacter = character;
            SelectedEffect(character, true);
        }
        else if (selectedCharacter == character)
        {
            // 같은 캐릭터 두 번째 클릭
            OnConfirm();
        }
        else
        {
            // 다른 캐릭터 클릭, 선택 변경
            SelectedEffect(selectedCharacter, false);
            selectedCharacter = character;
            SelectedEffect(character, true);
        }
    }

    public void OnConfirmButtonClicked()
    {
        if (selectedCharacter != null)
        {
            OnConfirm();
        }
    }

    private void OnConfirm()
    {
        battleSystem.SelectedTarget = true;
        battleSystem.CanSelectTarget = false;
        // 선택된 캐릭터를 Targets 리스트에 추가
        if (!battleSystem.SelectedSkill.SkillSO.isSingleAttack)
        {
            if (battleSystem.SelectedSkill.SkillSO.IsBuff)
            {
                foreach (CharacterCarrier units in battleSystem.GetActivePlayers())
                {
                    battleSystem.Targets.Add(units);
                    SelectedEffect(units, false);
                }
            }
            else
            {
                foreach (CharacterCarrier units in battleSystem.GetActiveEnemies())
                {
                    battleSystem.Targets.Add(units);
                    SelectedEffect(units, false);
                }
            }
        }
        if (battleSystem.SelectedSkill.SkillSO.isSingleAttack)
        {
            battleSystem.Targets.Add(selectedCharacter);
            SelectedEffect(selectedCharacter, false);
        }
        selectedCharacter = null;
        initialTarget = false;
        battleSystem.SetTarget();
    }

    private void ResetSelectedCharacter()
    {
        selectedCharacter = null;
    }

    //캐릭터 선택 효과
    private void SelectedEffect(CharacterCarrier character, bool set)
    {
        character.SelectEffect.SetActive(set);
    }

    private void ResetEffect()
    {
        foreach (CharacterCarrier units in battleSystem.GetActiveEnemies())
        {
            SelectedEffect(units, false);
        }
        foreach (CharacterCarrier units in battleSystem.GetActivePlayers())
        {
            SelectedEffect(units, false);
        }
    }

}
