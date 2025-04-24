using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBreakUPBtn : MonoBehaviour, ISetPOPUp
{
    UIBreakPOPUP breakPOPUP;
    Button[] buttons;
    private void Awake()
    {
        breakPOPUP = GetComponentInParent<UIBreakPOPUP>();
        buttons = GetComponentsInChildren<Button>();
    }

    public void Initialize()
    {

        ItemSavaData itemSavaData = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, ImsiGameManager.Instance.GetCharacter().CharacterItem);
        CharacterSaveData characterSave = ImsiGameManager.Instance.GetCharacterSaveData();


        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();



        if (itemSavaData != null && itemSavaData.Value > 0 && characterSave.Recognition != 5)
        {
            buttons[0].onClick.AddListener(() => UPBreak(itemSavaData, characterSave));
        }
        else
        {
            buttons[0].interactable = false;
        }

        buttons[1].onClick.AddListener(breakPOPUP.Destroy);
    }


    public void UPBreak(ItemSavaData itemSava , CharacterSaveData saveData)
    {
        itemSava.Value--;
        saveData.Recognition += 1;
        //레벨에 따라서 그 효과 열기 진행
        SaveDataBase.Instance.SaveSingleData(itemSava);
        SaveDataBase.Instance.SaveSingleData(saveData);
        breakPOPUP.Initialize();
    }

}
