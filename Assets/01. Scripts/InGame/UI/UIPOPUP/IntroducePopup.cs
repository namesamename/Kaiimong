
using TMPro;

using UnityEngine.UI;

public class IntroducePopup : UIPOPUP
{
    TMP_InputField input;

    Button[] buttons;


   

    CharacterProfileHUD hUD;

    private void Awake()
    {
        input = GetComponentInChildren<TMP_InputField>();
        buttons = GetComponentsInChildren<Button>();
       

       
    }


    private void Start()
    {

        hUD = FindAnyObjectByType<CharacterProfileHUD>();
        PlayShowAnimation();
        SetBtn();
    }


    public void SetBtn()
    {
        for (int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].onClick.RemoveAllListeners();
        }

        buttons[0].onClick.AddListener(Destroy);
        buttons[1].onClick.AddListener(Confirm);
        buttons[2].onClick.AddListener(Destroy);


    }


    public void Confirm()
    {
        UIManager.Instance.SetText(input.text);
        hUD.Initialize();
        Destroy();

       
    }




}
