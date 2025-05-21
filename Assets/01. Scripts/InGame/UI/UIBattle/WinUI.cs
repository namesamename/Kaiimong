using DG.Tweening;
using System.Collections;
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
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI expPotionText;
    [SerializeField] private TextMeshProUGUI diaText;

    [SerializeField] private Image extraTargetOne;
    [SerializeField] private Image extraTargetTwo;

    public bool CanClick = false;
    private bool setComplete = false;

    private void Awake()
    {
        rewardPrefabs = Resources.Load<GameObject>("UI/Battle/ItemRewardSlot");
        StageManager.Instance.OnWin -= SetWinUI;
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

    public void SetWinUI()
    {
        if (!setComplete)
        {

            StageManager.Instance.WinUI = this;

            CanClick = true;
            int RandomCharacterID = StageManager.Instance.Players[Random.Range(0, StageManager.Instance.Players.Count)].ID;
            //Sprite sprite = GlobalDataTable.Instance.Sprite.GetSpriteToID(RandomCharacterID, SpriteType.Illustration);

            Sprite sp = AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, RandomCharacterID).Result;

            //Sprite sprite = Resources.Load<Sprite>($"CharacterSprite/{RandomCharacterID}");

            //characterImage.sprite = StageManager.Instance.Players[Random.Range(0,StageManager.Instance.Players.Count)].visual.SpriteRenderer.sprite;
            //characterImage.sprite = Resources.Load<Sprite>(StageManager.Instance.Players[Random.Range(0, StageManager.Instance.Players.Count)].SpritePath);

            bool targetOne = false;
            bool targetTwo = false;

            StageManager.Instance.CheckExtraTarget(targetOne, targetTwo);

            ExtraTargetColor(targetOne, targetTwo);

            characterImage.sprite = sp;
            characterImage.SetNativeSize();

            stageNameText.text = StageManager.Instance.CurrentStage.Name;
            playerLevelText.text = $"Lv {CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)}";
            playerExpText.text = $"{CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP)} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.UserEXP].MaxCount}";
            earnedExpText.text = $"Exp +{StageManager.Instance.userExp}";

            goldText.text = StageManager.Instance.RewardGold.ToString();
            expPotionText.text = StageManager.Instance.RewardExpPotion.ToString();
            diaText.text = StageManager.Instance.RewardDia.ToString();

            StartCoroutine(DelaySetExpBar());
        }
    }

    void ExtraTargetColor(bool targetOne, bool targetTwo)
    {
        if (targetOne)
        {
            extraTargetOne.DOColor(Color.magenta, 1f);
        }
        else
        {
            extraTargetOne.color = Color.white;
        }

        if (targetTwo)
        {
            extraTargetTwo.DOColor(Color.magenta, 1f);
        }
        else
        {
            extraTargetTwo.color = Color.white;
        }
    }

    private IEnumerator DelaySetExpBar()
    {
        yield return null;

        float userExp = CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP);
        float maxExp = GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.UserEXP].MaxCount;
        expBar.value = userExp / maxExp;
    }

    public void UnSubscribeWinUI()
    {
        CanClick = false;
        StageManager.Instance.OnWin -= SetWinUI;
    }

    private bool IsClickOnRewardTab()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Input.mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (var result in results)
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
