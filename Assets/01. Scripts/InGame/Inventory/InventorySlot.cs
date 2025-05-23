using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

//슬롯 하나 담당하는 스크립트

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;                 // 슬롯 안의 아이콘 이미지
    [SerializeField] private TextMeshProUGUI countText;       //아이템 수량 텍스트
    [SerializeField] private Outline outlineEffect;           // 슬롯 외곽선 효과


    Image[] images;

    private ItemData item;                    // 현재 슬롯에 들어있는 아이템
    private int itemCount;                    // 해당 아이템 수량

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }
    public void SetSlot(ItemData newItem, int amount = 1)     // 슬롯에 새 아이템 슬롯 설정
    {
    
        item = newItem;
        itemCount = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, item.ID).Value;

        if (item != null)
        {
            SetSlotColor(newItem);
            iconImage.sprite = Resources.Load<Sprite>(item.IconPath);
            iconImage.enabled = true;        // 아이콘  활성화
            if (itemCount > 1)
            {
                countText.text = itemCount.ToString();
            }
            else
            {
                countText.text = string.Empty;
            }

            outlineEffect.enabled = false;   // 슬롯 생성 시 외곽선 비활성화
        }
        else
        {
            ClearSlot();                    // 아이템이 null이면 슬롯 비우기
        }
    }


    public void SetSlotColor(ItemData item)
    {
        switch (item.Grade)
        {
            case ERarity.S:
                images[0].color = Color.yellow;
                break;
            case ERarity.A:
                images[0].color = Color.magenta;
                break;
            case ERarity.B:
                images[0].color = Color.blue;
                break;
            case ERarity.C:
                images[0].color = Color.green;
                break;
            case ERarity.D:
                images[0].color = Color.gray;
                break;
        }

    }


    public  async void SetSlotCurrency(CurrencyType currency, int Count)
    {


        iconImage.enabled = true;        // 아이콘  활성화
        if (itemCount > 1)
        {
            countText.text = Count.ToString();
        }
        else
        {
            countText.text = string.Empty;
        }

        switch (currency) 
        {
            case CurrencyType.UserEXP:
                iconImage.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 2);
                break;
            case CurrencyType.Gold:
                iconImage.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 1);
                break;
            case CurrencyType.Gacha:
                break;
            case CurrencyType.Activity:
                break;
            case CurrencyType.Dia:
                break;
            case CurrencyType.CharacterEXP:
                break;


        }
    }

    public void ClearSlot()                 // 슬롯 비우기
    {
        item = null;                        // 아이템 제거
        itemCount = 0;                      // 수량 초기화

        iconImage.sprite = null;            // 이미지 제거
        iconImage.enabled = false;          // 이미지 숨김

    }

    public bool TryConsume(int amount)                // 아이템 지정한 수량만큼 소비
    {
        if (item.ItemType != EItemType.Consume)         // 아임템 타입이 소모품이 아니면
            return false;                             // 소비 실패 (소모품만 사용 가능)

        if (itemCount >= amount)                       // 현재 수량이 소비하려는 양 이상이면
        {
            itemCount -= amount;                      // 수량 차감
            countText.text = itemCount > 0 ? itemCount.ToString() : "";  // 수량 표시, 0이면 표시 제거

            if (itemCount == 0)
                ClearSlot();                              // 수량 0이면 슬롯 비우기

            return true;                              // 소비 성공
        }
        else
        {
            Debug.Log("재료 수량이 부족");
            return false;                             // 소비 실패
        }
    }
    public ItemData GetItem()                         // 현재 아이템 정보 반환
    {
        return item;
    }

    public int GetCount()                             // 현재 아이템 수량 반환
    {
        return itemCount;
    }




    public  async void OnPointerClick(PointerEventData eventData)    // 슬롯 클릭 시 팝업 호출
    {
        if (item == null) return;                             // 아이템이 비어있으면 무시

        var popupObj = await UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager의 ShowPopup 함수 호출

        if (popupObj != null)
        {
            popupObj.ShowItem(item);  // 아이템 데이터 전달
        }
    }

    /* public void OnPointerClick(PointerEventData eventData)      // 슬롯 클릭 시 팝업 호출
     {
         if (item == null) return;                                 // 슬롯에 아이템이 없으면 돌아가기

         ItemInfoPopup popup = FindObjectOfType<ItemInfoPopup>();  // 씬에서 ItemInfoPopup 오브젝트
         if (popup != null)
             popup.Show(item, itemCount);
     }*/
}

