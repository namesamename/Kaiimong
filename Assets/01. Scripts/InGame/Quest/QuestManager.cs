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

        //    // 저장된 데이터가 있는지 확인
        //    var foundData = SaveDataBase.Instance.GetSaveInstanceList<ItemSavaData>(SaveType.Item);
        //    if (foundData != null && foundData.Find(x => x.ID == itemID) != null)
        //    {
        //        // 저장된 데이터 로드
        //        var savedData = foundData.Find(x => x.ID == itemID);
        //        newItemData.Value = savedData.Value;
        //        newItemData.Savetype = SaveType.Item;

        //    }
        //    else
        //    {
        //        // 새 데이터 생성
        //        newItemData.Value = 0;
        //        newItemData.Savetype = SaveType.Item;
        //        SaveDataBase.Instance.SaveSingleData(newItemData);
        //        Debug.Log("item data NO");
        //    }

        //    // 사전에 저장
        //    ItemDatasSaveDic[itemID] = newItemData;
        //}


    }
   
}
