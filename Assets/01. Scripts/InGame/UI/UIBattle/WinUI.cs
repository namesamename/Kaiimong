using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    private GameObject rewardPrefabs;

    [SerializeField] private Image characterImage;
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI playerLevelText;
    [SerializeField] private TextMeshProUGUI playerExpText;
    [SerializeField] private TextMeshProUGUI earnedExpText;
    [SerializeField] private GameObject itemRewardSlots;

    public bool CanClick = false;

    private void Awake()
    {
        rewardPrefabs = Resources.Load<GameObject>("UI/Battle/ItemRewardSlot");
    }

    void Start()
    {
        StageManager.Instance.OnWin += SetWinUI;
    }

    private void Update()
    {
        if (CanClick)
        {
            if (IsClickOnRewardTab()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                StageManager.Instance.ToStageSelectScene();
            }
        }
    }

    void SetWinUI()
    {
        CanClick = true;
        //sprite¡ÿ∫Ò æ»µ 
        int RandomCharacterID = StageManager.Instance.Players[Random.Range(0, StageManager.Instance.Players.Count)].ID;
        //int RandomCharacter = GlobalDataTable.Instance.DataCarrier.GetCharacterIDList()[Random.Range(0, GlobalDataTable.Instance.DataCarrier.GetCharacterIDList().Count)];
        Sprite sprite = GlobalDataTable.Instance.Sprite.GetSpriteToID(RandomCharacterID, SpriteType.Illustration);
        
        characterImage.sprite = sprite;
        //characterImage.sprite = Resources.Load<Sprite>(StageManager.Instance.Players[Random.Range(0, StageManager.Instance.Players.Count)].SpritePath);
        //characterImage.sprite = StageManager.Instance.Players[Random.Range(0,StageManager.Instance.Players.Count)].visual.SpriteRenderer.sprite;
        //characterImage.SetNativeSize();

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

    public void SetRewardSlot(int itemID, int count)
    {
        GameObject slot;
        slot = Instantiate(rewardPrefabs, itemRewardSlots.transform);
        slot.GetComponent<ItemRewardSlot>().SetSlot(itemID, count);
    }
}
