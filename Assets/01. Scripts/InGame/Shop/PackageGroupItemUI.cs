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

    [Header("������ ����")]
    [SerializeField] private Sprite goldIcon;
    [SerializeField] private Sprite crystalIcon;

    public void Setup(PackageGroup group, Shop shop)
    {
        titleText.text = $"��Ű�� {group.ShopID}";

        // ���� ���� ����
        foreach (Transform child in itemContainer)
        {
            Destroy(child.gameObject);
        }

        // ������ ���� ����
        foreach (var package in group.Packages)
        {
            var slotObj = Instantiate(itemSlotPrefab, itemContainer);
            var slotUI = slotObj.GetComponent<ItemSlotUI>();
            slotUI.Setup(package);
        }

        // ���� �� ȭ�� ������ ����
        priceText.text = shop.Price.ToString();
        priceIcon.sprite = (shop.MoneyID == 1 ? goldIcon : crystalIcon);

        // ���� ��ư ����
        buyButton.onClick.RemoveAllListeners();
        buyButton.interactable = true;
        buyButton.onClick.AddListener(() => OnClickBuy(group, shop));
    }

    private async void OnClickBuy(PackageGroup group, Shop shop)
    {
        var curManager = CurrencyManager.Instance;
        int current = 0;
        CurrencyType currencyType = CurrencyType.Gold;

        // ���� ��ȭ Ÿ�� ����
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

        // ��ȭ ���� ��
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

        // ���� ��ȭ Ÿ�� ����
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

        // ��ȭ ���� (���� ������ ó��)
        curManager.SetCurrency(currencyType, -shop.Price);
        Debug.Log($"��Ű�� {group.ShopID} ���� �Ϸ� - {shop.Price} {currencyType}");

        // ������ ����
        foreach (var package in group.Packages)
        {
            if (package.ItemID == 0)
            {
                curManager.SetCurrency(CurrencyType.Gold, package.Gold);
                Debug.Log($"��� ����: +{package.Gold}");
            }
            else
            {
                ItemManager.Instance.SetitemCount(package.ItemID, package.ItemCount);
                Debug.Log($"������ ����: ID={package.ItemID}, ����={package.ItemCount}");
            }
        }
        ShopUIManager.Instance.SettingCurrency();

        // TODO: UI ����, ����Ʈ, ���� �� �߰� ����
    }
}
