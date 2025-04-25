using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGatchaHistory : MonoBehaviour
{
    public void PopupHistory()
    {
        UIManager.Instance.ShowPopup<PopupGatchaHistory>();
    }
}
