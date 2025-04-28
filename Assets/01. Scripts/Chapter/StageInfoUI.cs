using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    [SerializeField] private Button enterButton;
    [SerializeField] private TextMeshProUGUI stageName;

    private Stage stage;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        rect.DOAnchorPos(new Vector3(-960, rect.anchoredPosition.y), 1f).SetEase(Ease.Linear);
    }

    public void DisableUI()
    {
        RemoveListner();
        stage = null;
        rect.DOAnchorPos(new Vector3(0, rect.anchoredPosition.y), 1f).SetEase(Ease.Linear);
        StartCoroutine(DisableDelay());
    }

    private IEnumerator DisableDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    public void SetUI(StageSlot slot)
    {
        stage = slot.Stage;
        stageName.text = stage.Name;

        enterButton.onClick.AddListener(OnEnterButton);
    }

    private void OnEnterButton()
    {
        if (GlobalDataTable.Instance.DataCarrier.GetCharacterIDList().Count > 0)
        {
            StageManager.Instance.CurrentStage = stage;
            List<int> playerID = new List<int>(GlobalDataTable.Instance.DataCarrier.GetCharacterIDList());
            foreach (int id in playerID)
            {
                Character newCharacter = GlobalDataTable.Instance.character.GetCharToID(id);
                //GameObject newGameObject = GlobalDataTable.Instance.character.CharacterInstanceSummon(newCharacter, Vector3.zero);
                //CharacterCarrier newCharacterCarrier = newGameObject.GetComponent<CharacterCarrier>();
                StageManager.Instance.Players.Add(newCharacter);
            }
            RemoveListner();
            SceneLoader.Instance.ChangeScene(SceneState.BattleScene);
        }
    }

    private void RemoveListner()
    {
        enterButton.onClick.RemoveListener(OnEnterButton);
    }

}
