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
        if (Input.GetMouseButtonDown(0)) // ���� ���콺 ��ư Ŭ��
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
        //ù Ŭ��,����
        if (selectedCharacter == null)
        {
            selectedCharacter = character;
            SelectedEffect(character, true);
        }
        else if (selectedCharacter == character)
        {
            // ���� ĳ���� �� ��° Ŭ��
            OnConfirm();
        }
        else
        {
            // �ٸ� ĳ���� Ŭ��, ���� ����
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
        // ���õ� ĳ���͸� Targets ����Ʈ�� �߰�
        battleSystem.Targets.Add(selectedCharacter);
        //battleSystem.ProceedToNextStage();
    }

    //ĳ���� ���� ȿ��
    private void SelectedEffect(DummyUnit character, bool set)
    {
        character.SelectEffect.SetActive(set);
    }


}
