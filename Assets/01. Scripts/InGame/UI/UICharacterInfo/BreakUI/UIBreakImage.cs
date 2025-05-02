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
        images[1].sprite = GlobalDataTable.Instance.Sprite.GetSpriteToID(GlobalDataTable.Instance.DataCarrier.GetCharacter().ID, SpriteType.Illustration);

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
