using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerExpText;
    [SerializeField] private TextMeshProUGUI earnedExpText;
    public bool CanClick = false;


    void Start()
    {
        StageManager.Instance.OnWin += SetWinUI;
    }

    private void Update()
    {
        if (CanClick)
        {
            if (IsClickOnRewardTab()) return;

            StageManager.Instance.ToStageSelectScene();
        }
    }

    void SetWinUI()
    {
        CanClick = true;
        characterImage.sprite = StageManager.Instance.Players[Random.Range(0,StageManager.Instance.Players.Count)].visual.SpriteRenderer.sprite;
        characterImage.SetNativeSize();
        stageNameText.text = StageManager.Instance.CurrentStage.Name;
        playerLevelText.text = $"Lv {CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)}";
        playerExpText.text = $"{CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP)} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.UserEXP].MaxCount}";
        earnedExpText.text = $"Exp +{StageManager.Instance.userExp}";
    }

    public void UnSubscribeWinUI()
    {
        CanClick = false;
        StageManager.Instance.OnWin += SetWinUI;
    }

    private bool IsClickOnRewardTab()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach(var result in results)
        {
            if (result.gameObject.CompareTag("Reward"))
            {
                return true;
            }
        }

        return false;
    }
}
