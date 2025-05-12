using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileStatusMessage : MonoBehaviour
{
    TextMeshProUGUI[] text;
    Button button;

    private void Awake()
    {
        text = GetComponentsInChildren<TextMeshProUGUI>();
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
    }

    public void Initialize()
    {
        button.onClick.AddListener(SetBtn);
        SetText();
    }


    public async void SetBtn()
    {
       await UIManager.Instance.ShowPopup("IntroducePopup");
    }


    public void SetText()
    {
        if(UIManager.Instance.GetText() !=  string.Empty) 
        {
            text[0].text = UIManager.Instance.GetText();
        }
        else
        {
            text[0].text = "Hellow";
        }
        text[1].text = "Write";

    }

  

}
