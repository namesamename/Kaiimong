using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{


    Dictionary<int, QuestSaveData> SaveDic = new Dictionary<int, QuestSaveData>();


    public void Intialize()
    {


        //for (int i = 1; i < GlobalDataTable.Instance. + 1; i++)
        //{
        //    int itemID = GlobalDataTable.Instance.Item.ItemDic[i].ID;
        //    ItemSavaData newItemData = new ItemSavaData();
        //    newItemData.ID = itemID;

        //    // ����� �����Ͱ� �ִ��� Ȯ��
        //    var foundData = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);
        //    if (foundData != null && foundData.Find(x => x.ID == itemID) != null)
        //    {
        //        // ����� ������ �ε�
        //        var savedData = foundData.Find(x => x.ID == itemID);
        //        newItemData.Value = savedData.Value;
        //        newItemData.Savetype = SaveType.Item;

        //    }
        //    else
        //    {
        //        // �� ������ ����
        //        newItemData.Value = 0;
        //        newItemData.Savetype = SaveType.Item;
        //        SaveDataBase.Instance.SaveSingleData(newItemData);
        //        Debug.Log("item data NO");
        //    }

        //    // ������ ����
        //    ItemDatasSaveDic[itemID] = newItemData;
        //}


    }
   
}
