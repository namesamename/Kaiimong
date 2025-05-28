using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecognitionitemSlot : MonoBehaviour, IPointerClickHandler
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
    public async void SetSlot(ItemData newItem, int NeedCount)     // 슬롯에 새 아이템 슬롯 설정
    {
        item = newItem;

        ItemSavaData itemSava = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, item.ID);
        SetColor(newItem);
        if (item != null)
        {
            iconImage[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.ItemIcon, item.ID);
            iconImage[1].enabled = true;        // 아이콘  활성화

            if (itemSava != null)
            {

                countText.text = $"{itemSava.Value}/{NeedCount}";
                if (itemSava.Value < NeedCount)
                {
                    countText.color = Color.red;
                }
                else
                {
                    countText.color = Color.black;
                }
            }
            else
            {
                countText.text = $"{0}/{NeedCount}";

                countText.color = Color.red;

            }


            //outlineEffect.enabled = false;   // 슬롯 생성 시 외곽선 비활성화
        }
        else
        {
            ClearSlot();                    // 아이템이 null이면 슬롯 비우기
        }
    }


    public  void SetLevel(int NeedCount)
    {
        itemCount = GlobalDataTable.Instance.DataCarrier.GetSave().Level;
        iconImage[0].color = Color.yellow;
       
        countText.text = $"{itemCount}/{NeedCount}";
        if (itemCount < NeedCount)
        {
            countText.color = Color.red;
        }
        else
        {
            countText.color = Color.black;
        }
    }

    public async void SetGold(int NeedCount)
    {
        itemCount = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        iconImage[0].color = Color.yellow;
        iconImage[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CurrencyIcon, 1);
        countText.text = $"{itemCount.ToKNumber()}/{NeedCount.ToKNumber()}";
        if (itemCount < NeedCount)
        {
            countText.color = Color.red;
        }
        else
        {
            countText.color = Color.black;
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

    public async  void OnPointerClick(PointerEventData eventData)    // 슬롯 클릭 시 팝업 호출
    {
        if (item == null) return;                             // 아이템이 비어있으면 무시

        var popupObj = await UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager의 ShowPopup 함수 호출

        if (popupObj != null)
        {
           popupObj.ShowItem(item);  // 아이템 데이터 전달
        }
    }


    public void SetColor(ItemData itemData)
    {
        switch (itemData.Grade)
        {
            case ERarity.S:
                iconImage[0].color = Color.yellow;
                break;
            case ERarity.A:
                iconImage[0].color = Color.magenta;
                break;
            case ERarity.B:
                iconImage[0].color = Color.blue;
                break;
            case ERarity.C:
                iconImage[0].color = Color.green;
                break;
            case ERarity.D:
                iconImage[0].color = Color.gray;
                break;
        }

    }
}

