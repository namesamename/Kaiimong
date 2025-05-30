using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PackageGroupItemUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform itemContainer;
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private Image priceIcon;
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private Button buyButton;

    [Header("아이콘 설정")]
    [SerializeField] private Sprite goldIcon;
    [SerializeField] private Sprite crystalIcon;

    public void Setup(PackageGroup group, Shop shop)
    {
        titleText.text = $"패키지 {group.ShopID}";

        // 기존 슬롯 제거
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // 아이템 슬롯 생성
        foreach (var package in group.Packages)
        {
            var slotObj = Instantiate(itemSlotPrefab, itemContainer);
            var slotUI = slotObj.GetComponent<ItemSlotUI>();
            slotUI.Setup(package);
        }

        // 가격 및 화폐 아이콘 설정
        priceText.text = shop.Price.ToString();
        priceIcon.sprite = (shop.MoneyID == 1 ? goldIcon : crystalIcon);

        // 구매 버튼 설정
        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = true;
        buyButton.onClick.AddListener(() => OnClickBuy(group, shop));
    }

    private async void OnClickBuy(PackageGroup group, Shop shop)
    {
        var curManager = CurrencyManager.Instance;
        int current = 0;
        CurrencyType currencyType = CurrencyType.Gold;

        // 구매 재화 타입 결정
        if (shop.MoneyID == 1)
        {
            current = curManager.GetCurrency(CurrencyType.Gold);
            currencyType = CurrencyType.Gold;
        }
        else if (shop.MoneyID == 3)
        {
            current = curManager.GetCurrency(CurrencyType.Dia);
            currencyType = CurrencyType.Dia;
        }

        // 재화 부족 시
        if (current < shop.Price)
        {
            await UIManager.Instance.ShowPopup("ShopCurrencyLackPopup");
            return;
        }
        var popup = await UIManager.Instance.ShowPopup("ShopBuyPopUp") as ShopBuyPopUp;

        if (popup != null)
        {
            popup.Setup(() =>
            {
                TryBuy(group, shop);
            });
        }
    }

    private async void TryBuy(PackageGroup group, Shop shop)
    {
        var curManager = CurrencyManager.Instance;
        int current = 0;
        CurrencyType currencyType = CurrencyType.Gold;

        // 구매 재화 타입 결정
        if (shop.MoneyID == 1)
        {
            current = curManager.GetCurrency(CurrencyType.Gold);
            currencyType = CurrencyType.Gold;
        }
        else if (shop.MoneyID == 3)
        {
            current = curManager.GetCurrency(CurrencyType.Dia);
            currencyType = CurrencyType.Dia;
        }

        // 재화 차감 (음수 값으로 처리)
        curManager.SetCurrency(currencyType, -shop.Price);
        Debug.Log($"패키지 {group.ShopID} 구매 완료 - {shop.Price} {currencyType}");

        // 아이템 지급
        foreach (var package in group.Packages)
        {
            if (package.ItemID == 0)
            {
                curManager.SetCurrency(CurrencyType.Gold, package.Gold);
                Debug.Log($"골드 지급: +{package.Gold}");
            }
            else
            {
                ItemManager.Instance.SetitemCount(package.ItemID, package.ItemCount);
                Debug.Log($"아이템 지급: ID={package.ItemID}, 수량={package.ItemCount}");
            }
        }
        ShopUIManager.Instance.SettingCurrency();

        // TODO: UI 갱신, 이펙트, 사운드 등 추가 가능
    }
}
