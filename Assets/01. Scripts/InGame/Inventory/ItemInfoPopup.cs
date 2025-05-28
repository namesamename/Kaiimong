
using UnityEngine.UI;

public class ItemInfoPopup : UIPOPUP
{

    UILocationDes locationDes;
    UILocationImage locationImage;
    UILocationItemValue locationItemValue;

    Button button;

    private void Awake()
    {
        locationDes = GetComponentInChildren<UILocationDes>();
        locationImage = GetComponentInChildren<UILocationImage>();
        locationItemValue = GetComponentInChildren<UILocationItemValue>();
        button = GetComponentInChildren<Button>();

    }


    public void ShowItem(ItemData itemData)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(Destroy);
        locationImage.ImageSetting(itemData.ID);
        locationDes.InfoSetting(itemData.ID);
        locationItemValue.ValueSettimg(itemData.ItemType, itemData.ID);
    }
   

}
