using TMPro;
using UnityEngine;
using UnityEngine.UI;

//슬롯 하나 담당하는 스크립트

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image iconImage;                 // 슬롯 안의 아이콘 이미지
    [SerializeField] private TextMeshProUGUI countText;       //아이템 수량 텍스트
    [SerializeField] private Outline outlineEffect;           // 슬롯 외곽선 효과


    private ItemData item;                    // 현재 슬롯에 들어있는 아이템
    private int itemCount;                    // 해당 아이템 수량

    public void SetSlot(ItemData newItem, int amount = 1)     // 슬롯에 아이템 설정
    {
        item = newItem;
        itemCount = amount;

        if (item != null)
        {
            iconImage.sprite = item.Icon;    // 아이콘 설정
            iconImage.enabled = true;        // 아이콘 표시
            countText.text = itemCount > 1 ? itemCount.ToString() : ""; // 1개일 땐 수량 숨길 수도 있음
            outlineEffect.enabled = false;   // 슬롯 생성 시 외곽선 비활성화
        }
        else
        {
            ClearSlot();                    // 아이템이 null이면 슬롯 비우기
        }
    }

    public void ClearSlot()                 // 슬롯 비우기
    {
        item = null;                        // 아이템 제거
        itemCount = 0;                      // 수량 초기화

        iconImage.sprite = null;            // 이미지 제거
        iconImage.enabled = false;          // 이미지 숨김

    }

    public bool TryConsume(int amount)      //아이템 지정한 수만큼 소비
    {
        if (itemCount >= amount)
        {
            itemCount -= amount;                                        // 수량 감소
            countText.text = itemCount > 0 ? itemCount.ToString() : ""; // 수량 업데이트
            {
                if (itemCount == 0)                                         // 0이 되면 슬롯 비우기
                    ClearSlot();
            }
            return true;                      // 소비 성공
        }
        else
        {
            Debug.Log("재료 수량이 부족합니다.");
            return false;                         // 소비 실패
        }
    }
    public ItemData GetItem()               // 현재 아이템 정보 반환
    {
        return item;
    }

    public int GetCount()                   // 현재 아이템 수량 반환
    {
        return itemCount;
    }




    /* public void OnPointerClick(PointerEventData eventData)    // 슬롯 클릭 시 팝업 호출
     {
         if (item == null) return;

         ItemInfoPopup popup = FindObjectOfType<ItemInfoPopup>();
         if (popup != null)
             popup.Show(item, itemCount);
     }*/
}

