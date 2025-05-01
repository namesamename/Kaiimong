using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBreakImage : MonoBehaviour, ISetPOPUp
{
    Image[] images;
    TextMeshProUGUI textMesh;
    private void Awake()
    {
        images =GetComponentsInChildren<Image>();
        textMesh = GetComponentInChildren<TextMeshProUGUI>();

    }
    public void Initialize()
    {
        //Images[0].sprite = Resources.Load<Sprite>(ImsiGameManager.Instance.GetCharacter().chracterpath);
        //Images[0].sprite = Resources.Load<Sprite>( GlobalDataTable.Instance.Item.GetItemDataToID(ImsiGameManager.Instance.GetCharacter().CharacterItem).IconPath);

        ItemSavaData itemSava = SaveDataBase.Instance.GetSaveDataToID<ItemSavaData>(SaveType.Item, GlobalDataTable.Instance.DataCarrier.GetCharacter().CharacterItem);

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
