using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectSlot : MonoBehaviour
{
    [Header("UI Reference")]
    TextMeshProUGUI[] textMeshPros;
    Image[] images;
    Button button;

    CharacterSaveData characterSaveData;
    Character character;

    private void Awake()
    {
        textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
        images = GetComponentsInChildren<Image>();
        button = GetComponentInChildren<Button>();
    }
    public void SetSlot(CharacterSaveData saveData, Character character)                
    {
        characterSaveData = saveData;
        this.character = character;
        SetSlotColor(saveData, character);
        textMeshPros[1].text = character.Name;                // 캐릭터 이름 표시
        textMeshPros[0].text = $"Lv. {saveData.Level}";      // 캐릭터 레벨 표시

        button.onClick.RemoveAllListeners();

       button.onClick.AddListener(ChangeScene);


        transform.DOLocalRotate(Vector3.zero, 1f);

        //Debug.Log($"[SetSlot] 슬롯에 적용: {character.Name} (ID:{character.ID}) Lv.{saveData.Level} Grade:{character.Grade}");

       
    }
    public void ChangeScene()
    {

        GlobalDataTable.Instance.DataCarrier.SetSave(characterSaveData);
        GlobalDataTable.Instance.DataCarrier.SetCharacter(character);


        switch (UIManager.Instance.characterIDType)
        {

            case UICharacterIDType.Lobby:
                UIManager.Instance.SetCharacterID(character.ID);
                SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
                break;
            case UICharacterIDType.First:
            case UICharacterIDType.Second:
            case UICharacterIDType.Thrid:
            case UICharacterIDType.Fourth:
                UIManager.Instance.SetCharacterID(character.ID);
                SceneLoader.Instance.ChangeScene(SceneState.ProfileScene);
                break;
            case UICharacterIDType.None:
                SceneLoader.Instance.ChangeScene(SceneState.CharacterInfoScene);
                break;
            default:
                break;
        }
    }



    public async void SetSlotColor(CharacterSaveData saveData, Character character)
    {

        for (int i = 0; i < images.Length; i++) 
        {
            images[i].enabled = false;
        }


        switch (character.Grade)
        {
            case Grade.S:
                images[0].enabled = true;
                images[0].color = new Color(229f / 255f, 156f / 255f, 42f / 255f);
                break;
            case Grade.A:
                images[0].enabled = true;
                images[0].color = new Color(164f / 255f, 68f / 255f, 217f / 255f);
                break;
            case Grade.B:
                images[0].enabled = true;
                images[0].color = new Color(34f / 255f, 111f / 255f, 236f / 255f);
                break;
        }
        images[0].enabled = true;
        images[1].enabled = true;


        Sprite sprite = null;

        if (character.ID > 5)
        {
            sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, 1);
        }else
        {
            sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, character.ID);
        }
      

    
        images[1].sprite = sprite;
        images[10].enabled = true;
        for (int i = 0;i < saveData.Recognition; i++) 
        {
            images[i+2].enabled = true;
        }
        for (int i = 0; i < saveData.Necessity; i++)
        {
            images[i + 5].enabled = true;
        }



       



    }





}