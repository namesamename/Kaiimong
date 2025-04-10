using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private DummyUnit selectedCharacter = null;
    private BattleSystem battleSystem;

    private Camera _camera;

    private void Awake()
    {
        battleSystem = GetComponent<BattleSystem>();
        _camera = Camera.main;
    }

    private void Start()
    {
        battleSystem.BattleUI.CharacterUI.OnConfirmButton += OnConfirmButtonClicked;
        battleSystem.SkillChanged += ResetEffect;
    }

    private void Update()
    {
        if (battleSystem.CanSelectTarget)
        {
            if (!battleSystem.SelectedSkill.isSingleAttack)
            {
                SelectAll();
            }
            else
            {
                MouseClick();
            }
        }
    }

    private void SelectAll()
    {
        if (!battleSystem.SelectedSkill.isSingleAttack)
        {
            foreach (DummyUnit units in battleSystem.GetActiveEnemies())
            {
                SelectedEffect(units, true);
                selectedCharacter = units;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                DummyUnit unit = hit.collider.GetComponent<DummyUnit>();
                if (unit != null && battleSystem.GetActiveEnemies().Contains(unit))
                {
                    OnConfirm();
                }
            }
        }
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                DummyUnit unit = hit.collider.GetComponent<DummyUnit>();
                if (unit != null && battleSystem.GetActiveEnemies().Contains(unit))
                {
                    OnCharacterClicked(unit);
                }
            }
        }
    }

    public void OnCharacterClicked(DummyUnit character)
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
        if (!battleSystem.SelectedSkill.isSingleAttack)
        {
            foreach (DummyUnit units in battleSystem.GetActiveEnemies())
            {
                battleSystem.Targets.Add(units);
                SelectedEffect(units, false);
            }
        }
        if (battleSystem.SelectedSkill.isSingleAttack)
        {
            battleSystem.Targets.Add(selectedCharacter);
            SelectedEffect(selectedCharacter, false);
        }
        selectedCharacter = null;
        battleSystem.SetTarget();
    }

    //캐릭터 선택 효과
    private void SelectedEffect(DummyUnit character, bool set)
    {
        character.SelectEffect.SetActive(set);
    }

    private void ResetEffect()
    {
        foreach (DummyUnit units in battleSystem.GetActiveEnemies())
        {
            SelectedEffect(units, false);
        }
    }

}
