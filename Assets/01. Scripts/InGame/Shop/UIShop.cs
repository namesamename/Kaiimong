using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private Button[] button;
    [SerializeField] private TextMeshProUGUI crystaltext;
    public int crystal;

    private void Start()
    {
        Settingcrystal();
    }
    public void SceneChange(int scene)
    {
        switch (scene) {
            case 1:SceneLoader.Instance.ChanagePreScene();
                break;
            case 2:SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);
                break;        
    }      
    }

    public void Settingcrystal()//티켓,보석 재화를 
    {
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
        crystaltext.text = "crystal = " + crystal.ToString();
    }
}
