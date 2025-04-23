using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBreakImage : MonoBehaviour, ISetPOPUp
{
    Image[] Images;
    TextMeshProUGUI textMesh;
    private void Awake()
    {
        Images =GetComponentsInChildren<Image>();
        textMesh = GetComponent<TextMeshProUGUI>();
    }
    public void Initialize()
    {
        //Images[0].sprite = Resources.Load<Sprite>(ImsiGameManager.Instance.GetCharacter().chracterpath);
        //Images[0].sprite = Resources.Load<Sprite>( GlobalDataTable.Instance.Item.GetItemDataToID(ImsiGameManager.Instance.GetCharacter().CharacterItem).IconPath);

        ItemSavaData itemSava = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, ImsiGameManager.Instance.GetCharacter().CharacterItem);

        if(itemSava == null)
        {
            textMesh.text = $"0/1";
        }
        else
        {
            textMesh.text = $"{itemSava.Value}/1";
        }
    }

}
