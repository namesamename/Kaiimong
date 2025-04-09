using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    private DummyUnit selectedCharacter = null;
    private BattleSystem battleSystem;

    private Camera camera;

    private void Awake()
    {
        battleSystem = GetComponent<BattleSystem>();
        camera = Camera.main;
    }
    private void Update()
    {
        MouseClick();
    }

    private void MouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭
        {
            Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
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
        // 선택된 캐릭터를 Targets 리스트에 추가
        battleSystem.Targets.Add(selectedCharacter);
        //battleSystem.ProceedToNextStage();
    }

    //캐릭터 선택 효과
    private void SelectedEffect(DummyUnit character, bool set)
    {
        character.SelectEffect.SetActive(set);
    }


}
