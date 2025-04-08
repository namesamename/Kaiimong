using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupEvent : MonoBehaviour
{
    [SerializeField] private GameObject PopObject; 

    public void Popup()
    {
        PopObject.SetActive(true);
    }
    public void ClosePopup()
    {
        PopObject.SetActive(false);
    }
}
