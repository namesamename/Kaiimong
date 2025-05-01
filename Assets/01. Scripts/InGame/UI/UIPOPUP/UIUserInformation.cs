using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class UIUserInformation : MonoBehaviour
{ 
    //할당할 오브젝트들 
    [SerializeField] private TextMeshProUGUI NickName;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI exp;
    [SerializeField] private Slider Slider;
   
    //임시로 지정하는 데이터들 - 플레이어 데이터 
    private void Start()
    {
        ShowUserInfo();
    }
    private void Update()
    {
        ShowUserInfo();

    }
    public void ShowUserInfo() //이름, 
    {
        NickName.text = CurrencyManager.Instance.GetUserName();
        Level.text = "LV." + CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel);
        exp.text =$"{CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP)}/{LevelUpSystem.PlayerNeedExp[CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)]}";
        float fillAmount = (float)CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP) / LevelUpSystem.PlayerNeedExp[CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)];
        Slider.value = Mathf.Clamp01(fillAmount);
    }






}
