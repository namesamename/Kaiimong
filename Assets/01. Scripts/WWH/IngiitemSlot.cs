using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

//슬롯 하나 담당하는 스크립트

public class IngiitemSlot : MonoBehaviour, IPointerClickHandler
{
   private Image[] iconImage;                 // 슬롯 안의 아이콘 이미지
   private TextMeshProUGUI countText;       //아이템 수량 텍스트
   private Outline outlineEffect;           // 슬롯 외곽선 효과

    private ItemData item;                    // 현재 슬롯에 들어있는 아이템
    private int itemCount;                    // 해당 아이템 수량

    private void Awake()
    {
        iconImage = GetComponentsInChildren<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetSlot(ItemData newItem, int NeedCount )     // 슬롯에 새 아이템 슬롯 설정
    {
        item = newItem;
        itemCount = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, item.ID).Value;

        if (item != null)
        {
            iconImage[1].sprite = Resources.Load<Sprite>(item.IconPath);
            iconImage[1].enabled = true;        // 아이콘  활성화
            countText.text = $"{itemCount}/{NeedCount}";
            if (itemCount < NeedCount)
            {
                countText.color = Color.red;
            }
            else
            {
                countText.color = Color.green;
            }
       

            outlineEffect.enabled = false;   // 슬롯 생성 시 외곽선 비활성화
        }
        else
        {
            ClearSlot();                    // 아이템이 null이면 슬롯 비우기
        }
    }


    public void SetGold(int NeedCount)
    {
        itemCount = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        iconImage[1].sprite = Resources.Load<Sprite>( GlobalDataTable.Instance.currency.GetCurrencySOToEnum<GoldCurrencySO>(CurrencyType.Gold).IconPath);
        countText.text = $"{itemCount}/{NeedCount}";
        if (itemCount < NeedCount)
        {
            countText.color = Color.red;
        }
        else
        {
            countText.color = Color.green;
        }



    }

    public void ClearSlot()                 // 슬롯 비우기
    {
        item = null;                        // 아이템 제거
        itemCount = 0;                      // 수량 초기화

        iconImage[1].sprite = null;            // 이미지 제거
        iconImage[1].enabled = false;          // 이미지 숨김

    }
    public ItemData GetItem()                         // 현재 아이템 정보 반환
    {
        return item;
    }

    public int GetCount()                             // 현재 아이템 수량 반환
    {
        return itemCount;
    }

    public void OnPointerClick(PointerEventData eventData)    // 슬롯 클릭 시 팝업 호출
    {
        if (item == null) return;                             // 아이템이 비어있으면 무시

        var popupObj = UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager의 ShowPopup 함수 호출

        if (popupObj != null)
        {
            popupObj.Show(item, itemCount);  // 아이템 데이터 전달
        }
    }
}

