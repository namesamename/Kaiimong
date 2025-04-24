using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStaminaChargePOPUP : MonoBehaviour
{
    Button[] Buttons;
    Image[] Images;
    TextMeshProUGUI[] textMesh;

    private void Awake()
    {
        Buttons = GetComponentsInChildren<Button>();
        Images = GetComponentsInChildren<Image>();
        textMesh = GetComponentsInChildren<TextMeshProUGUI>();

    }


}
