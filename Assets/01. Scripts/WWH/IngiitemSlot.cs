using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

//���� �ϳ� ����ϴ� ��ũ��Ʈ

public class IngiitemSlot : MonoBehaviour, IPointerClickHandler
{
   private Image[] iconImage;                 // ���� ���� ������ �̹���
   private TextMeshProUGUI countText;       //������ ���� �ؽ�Ʈ
   private Outline outlineEffect;           // ���� �ܰ��� ȿ��

    private ItemData item;                    // ���� ���Կ� ����ִ� ������
    private int itemCount;                    // �ش� ������ ����

    private void Awake()
    {
        iconImage = GetComponentsInChildren<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
    }
    public void SetSlot(ItemData newItem, int NeedCount )     // ���Կ� �� ������ ���� ����
    {
        item = newItem;
        itemCount = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, item.ID).Value;

        if (item != null)
        {
            iconImage[1].sprite = Resources.Load<Sprite>(item.IconPath);
            iconImage[1].enabled = true;        // ������  Ȱ��ȭ
            countText.text = $"{itemCount}/{NeedCount}";
            if (itemCount < NeedCount)
            {
                countText.color = Color.red;
            }
            else
            {
                countText.color = Color.green;
            }
       

            outlineEffect.enabled = false;   // ���� ���� �� �ܰ��� ��Ȱ��ȭ
        }
        else
        {
            ClearSlot();                    // �������� null�̸� ���� ����
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

    public void ClearSlot()                 // ���� ����
    {
        item = null;                        // ������ ����
        itemCount = 0;                      // ���� �ʱ�ȭ

        iconImage[1].sprite = null;            // �̹��� ����
        iconImage[1].enabled = false;          // �̹��� ����

    }
    public ItemData GetItem()                         // ���� ������ ���� ��ȯ
    {
        return item;
    }

    public int GetCount()                             // ���� ������ ���� ��ȯ
    {
        return itemCount;
    }

    public void OnPointerClick(PointerEventData eventData)    // ���� Ŭ�� �� �˾� ȣ��
    {
        if (item == null) return;                             // �������� ��������� ����

        var popupObj = UIManager.Instance.ShowPopup<ItemInfoPopup>();    // UIManager�� ShowPopup �Լ� ȣ��

        if (popupObj != null)
        {
            popupObj.Show(item, itemCount);  // ������ ������ ����
        }
    }
}

