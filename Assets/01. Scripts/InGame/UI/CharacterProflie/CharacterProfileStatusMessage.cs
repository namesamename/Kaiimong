using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileStatusMessage : MonoBehaviour
{
    TextMeshProUGUI[] text;
    Button button;


    string Introduce;

    private void Awake()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        button.onClick.RemoveAllListeners();
    }

    public void Initialize()
    {
        SetBtn();
    }


    public async void SetBtn()
    {
       await UIManager.Instance.ShowPopup("IntroducePopup");

        SetText();
    }


    public void SetText()
    {
        if(Introduce != string.Empty) 
        {
            text[0].text = Introduce;
        }
        else
        {
            text[0].text = "Hellow";
        }
        text[1].text = "Write";

    }


    public void SetIntroduce(string intro)
    {
        Introduce = intro;
    }
   


}
