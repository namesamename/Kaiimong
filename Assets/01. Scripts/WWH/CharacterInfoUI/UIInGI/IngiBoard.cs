using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngiBoard : MonoBehaviour, ISetPOPUp
{
    TextMeshProUGUI[] Textmesh;
    private void Awake()
    {
        Textmesh = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Initialize()
    {
        
    }

}
