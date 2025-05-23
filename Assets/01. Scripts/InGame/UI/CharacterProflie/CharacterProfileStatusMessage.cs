using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterProfileStatusMessage : MonoBehaviour
{
    TextMeshProUGUI text;
    Button button;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
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
            text.text = UIManager.Instance.GetText();
        }
        else
        {
            text.text = "Hellow";
        }

    }

  

}
