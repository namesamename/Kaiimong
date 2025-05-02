using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{

    private TextMeshProUGUI[] CountText;
    Button button;

    // 플레이어의 데이터

    private int playerstamina;
    private int playergold;
    private int playercrystal;
    //플레이어 데이터 가지고 오는 코드 , playerdata.gold 등등

    private void Start()
    {
        UpdateResource();

        // 시작 시 한 번 갱신해서 보이게


    }

    public void ButtonSet()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => UIManager.Instance.ShowPopup("StaminaChargePOPUP"));
    }
    public void UpdateResource() // 추후 매개변수를 제거하고 받아온 데이터를 가지고 이동시키기
    {
        playerstamina = CurrencyManager.Instance.GetCurrency(CurrencyType.Activity);
        playergold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        playercrystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
        CountText = GetComponentsInChildren<TextMeshProUGUI>();
 


        CountText[0].text = $"행동력: {playerstamina} / {CurrencyManager.Instance.GetCurrency(CurrencyType.CurMaxStamina)}";
        CountText[1].text = $"엽전: {playergold.ToKNumber()}";
        CountText[2].text = $"유리구슬: {playercrystal}";
    }

}
