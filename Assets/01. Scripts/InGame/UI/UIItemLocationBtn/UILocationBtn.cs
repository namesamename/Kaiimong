using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILocationBtn : MonoBehaviour
{
    Button button;
    TextMeshProUGUI text;

    private void Awake()
    {
        button = GetComponent<Button>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void SetBtn(string Obtain, Action action)
    {
        button.onClick.RemoveAllListeners();

        button.onClick.AddListener(() => action());
        text.text = Obtain;


    }




}
