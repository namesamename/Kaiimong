using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;
    public async void Setup(Package package)
    {
        Sprite loadedSprite = null;

        // ItemID가 0일 때는 재화 아이콘
        if (package.ItemID == 0)
        {
            loadedSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 1);
            Debug.Log($"재화 아이콘 로드: ID {package.ItemID}");
            iconImage.sprite = loadedSprite;
            iconImage.color = Color.white;
            countText.text = $"x{package.Gold}";
        }
        else
        {
            string key = $"ItemIcon_{package.ItemID}";
            loadedSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.ItemIcon, package.ItemID);
            Debug.Log($" 아이템 아이콘 로드: {key}");
            iconImage.sprite = loadedSprite;
            iconImage.color = Color.white;
            countText.text = $"x{package.ItemCount}";
        }

        if (loadedSprite == null)
        {
            Debug.LogWarning($"아이콘 로드 실패 - ItemID: {package.ItemID}");
            iconImage.color = Color.red;
            return;
        }
    }

}
