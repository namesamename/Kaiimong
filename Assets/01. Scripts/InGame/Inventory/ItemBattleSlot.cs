using UnityEngine;
using UnityEngine.UI;

public class ItemBattleSlot : MonoBehaviour
{
    [SerializeField] private Image gradeColor;
    [SerializeField] private Image Icon;

    private ItemData item;
    private ItemSavaData Save;

    ItemBattleSlots battleSlots;

    private int ItemID = 0;

    public void SetComponent()
    {
        battleSlots = GetComponentInParent<ItemBattleSlots>();
    }

    public void SetSlot(int ID)
    {
        ItemID = ID;
        gradeColor.enabled = true;
        Icon.enabled = true;
        item = GlobalDataTable.Instance.Item.GetItemDataToID(ID);
        Icon.sprite = Resources.Load<Sprite>(item.IconPath);
        Save = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, ID);

        SetSlotColor(item);
    }

    public void SlotClear()
    {
        item = null;
        Save = null;
        Icon.sprite = null;
        gradeColor.color = Color.white;
        gradeColor.enabled = false;
        Icon.enabled= false;
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
