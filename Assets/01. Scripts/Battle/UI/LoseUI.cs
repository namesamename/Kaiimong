using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI returnActivityText;
    [SerializeField] private Image activityImage;

    void Start()
    {
        //activityImage =
    }

    public void ReturnActivityText(int num)
    {
        returnActivityText.text = $": 행동력 반환 +{num}";
    }
}
