using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private Button[] button;

    public void SceneChange(int scene)
    {
        switch (scene) {
            case 1:SceneLoader.Instance.ChanagePreScene();
                break;
            case 2:SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
                break;        
    }      
    }
}
