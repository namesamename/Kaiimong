
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileSlot : MonoBehaviour
{
    int index = 0;
    Button btn;
    Image[] image;



    private void Awake()
    {
        btn = GetComponentInChildren<Button>();
        image = GetComponentsInChildren<Image>();
    }



    public void SetIndex(int index)
    {
        this.index = index; 
    }

    public void SetSlot()
    {
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(BtnSet);
        ImageSet();
    }


    public void BtnSet()
    {
        switch(index) 
        {
            case 1:
                UIManager.Instance.characterIDType = UICharacterIDType.First; 
                break;
            case 2:
                UIManager.Instance.characterIDType = UICharacterIDType.Second;
                break;
            case 3:
                UIManager.Instance.characterIDType = UICharacterIDType.Thrid;
                break;
            case 4:
                UIManager.Instance.characterIDType = UICharacterIDType.Fourth;
                break;
        }

        SceneLoader.Instance.ChangeScene(SceneState.CharacterSelectScene);
    }

    public async void ImageSet()
    {
        if (index == (int)UICharacterIDType.First)
        {
            if (UIManager.Instance.GetCharacterID(UICharacterIDType.First) != 0)
            {
                image[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, UIManager.Instance.GetCharacterID(UICharacterIDType.First));
            }
        }
        else if (index == (int)UICharacterIDType.Second)
        {
            if (UIManager.Instance.GetCharacterID(UICharacterIDType.Second) != 0)
            {
                image[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, UIManager.Instance.GetCharacterID(UICharacterIDType.Second));
            }
        }
        else if (index == (int)UICharacterIDType.Thrid)
        {
            if (UIManager.Instance.GetCharacterID(UICharacterIDType.Thrid) != 0)
            {
                image[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, UIManager.Instance.GetCharacterID(UICharacterIDType.Thrid));
            }
        }
        else if (index == (int)UICharacterIDType.Fourth)
        {
            if (UIManager.Instance.GetCharacterID(UICharacterIDType.Fourth) != 0)
            {
                image[1].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, UIManager.Instance.GetCharacterID(UICharacterIDType.Fourth));
            }
        }
    }


}
