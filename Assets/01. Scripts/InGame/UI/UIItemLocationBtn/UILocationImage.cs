using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILocationImage : MonoBehaviour
{

    Image Images;

    private void Awake()
    {
        Images = GetComponent<Image>();
    }


    public void ImageSetting(int ID)
    {
        //Images.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.ItemSlot, ID);
    }
}
