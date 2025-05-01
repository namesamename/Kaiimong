using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemRewardSlot : MonoBehaviour
{
    [SerializeField] private Image gradeColor;
    [SerializeField] private Image Icon;
    [SerializeField] private TextMeshProUGUI itemCount;

    private ItemData item;
    private int ItemID = 0;

    public void SetSlot(int ID, int count)
    {
        ItemID = ID;
        gradeColor.enabled = true;
        itemCount.text = count.ToString();
        Icon.enabled = true;
        item = GlobalDataTable.Instance.Item.GetItemDataToID(ID);
        Icon.sprite = Resources.Load<Sprite>(item.IconPath);
        SetSlotColor(item);
    }

    public void SlotClear()
    {
        item = null;
        Icon.sprite = null;
        itemCount = null;
        gradeColor.color = Color.white;
        gradeColor.enabled = false;
        Icon.enabled = false;
    }

    public void SetSlotColor(ItemData item)
    {
        ERarity grade = item.Grade;
        switch (grade)
        {
            case ERarity.S:
                gradeColor.color = Color.yellow;
                break;
            case ERarity.A:
                gradeColor.color = Color.magenta;
                break;
            case ERarity.B:
                gradeColor.color = Color.blue;
                break;
            case ERarity.C:
                gradeColor.color = Color.green;
                break;
            case ERarity.D:
                gradeColor.color = Color.gray;
                break;
        }
    }
}
