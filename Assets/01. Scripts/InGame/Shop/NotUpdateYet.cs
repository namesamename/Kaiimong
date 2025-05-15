using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotUpdateYet : MonoBehaviour
{
    
    public async void NotUpdateYetPopup()
    {
        await UIManager.Instance.ShowPopup("UpdateNoticePopUp");
    }
}
