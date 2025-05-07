using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGatchaHistory : MonoBehaviour
{
    public async void PopupHistory()
    {
       await  UIManager.Instance.ShowPopup<PopupGatchaHistory>();
    }
}
