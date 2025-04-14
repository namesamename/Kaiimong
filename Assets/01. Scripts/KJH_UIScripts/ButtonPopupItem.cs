using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPopupItem : MonoBehaviour
{
    public void Popup(string prefabName)
    {

        UIManager.Instance.ShowPopup(prefabName);
    }
}
