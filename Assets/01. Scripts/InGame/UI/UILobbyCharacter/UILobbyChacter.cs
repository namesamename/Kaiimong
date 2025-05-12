using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyChacter : MonoBehaviour
{
    Image characterImage;
    Button[] buttons;
    TextMeshProUGUI[] textMeshProUGUI;
    Action LoadOk;


    List<string> Dialogue = new List<string>();
    private void Awake()
    {
        characterImage = GetComponent<Image>();
        buttons = GetComponentsInChildren<Button>();
        textMeshProUGUI = GetComponentsInChildren<TextMeshProUGUI>();

        characterImage.enabled = false;
    }



    private void Start()
    {
        LoadOk += SetText;
        LoadOk += SetButton;

        SetImage();
    }

    public async void SetImage()
    {
        characterImage.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, UIManager.Instance.GetCharacterID(UICharacterIDType.Lobby));
        characterImage.enabled = true;
        LoadOk?.Invoke();
    }


    public void SetText()
    {
        if (UIManager.Instance.GetCharacterID(UICharacterIDType.Lobby) != 0)
        {
            CharacterDialogue dialogue = GlobalDataTable.Instance.Dialogue.GetDialogue(UIManager.Instance.GetCharacterID(UICharacterIDType.Lobby));
            Dialogue.Add(dialogue.FirstDialogue);
            Dialogue.Add(dialogue.SecondDialogue);
            Dialogue.Add(dialogue.ThirdDialogue);
            Dialogue.Add(dialogue.FourthDialogue);
            Dialogue.Add(dialogue.FifthDialogue);
            textMeshProUGUI[0].text = Dialogue[0];
        }
        else
        {
            Debug.LogError("캐릭터가 대화텍스트가 존재하지 않음");
        }
    }

    public void SetButton()
    {
        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        buttons[0].onClick.AddListener(ClickImage);
        buttons[1].onClick.AddListener(CharacterSelect);

    }


    public void ClickImage()
    {
        if(Dialogue.Count > 0) 
        {
            textMeshProUGUI[0].text = Dialogue[UnityEngine.Random.Range(0, Dialogue.Count)];
        }
    }


    public void CharacterSelect()
    {
        UIManager.Instance.characterIDType = UICharacterIDType.Lobby;
        SceneLoader.Instance.ChangeScene(SceneState.CharacterSelectScene);

    }




}
