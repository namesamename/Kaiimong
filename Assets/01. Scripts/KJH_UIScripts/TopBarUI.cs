using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{

    private TextMeshProUGUI[] CountText;
    Button button;

    // �÷��̾��� ������

    private int playerstamina;
    private int playergold;
    private int playercrystal;
    //�÷��̾� ������ ������ ���� �ڵ� , playerdata.gold ���

    private void Start()
    {
        UpdateResource();
        ShowResource();
        // ���� �� �� �� �����ؼ� ���̰�


    }


    public void ButtonSet()
    {
        button = GetComponentInChildren<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => UIManager.Instance.ShowPopup("StaminaChargePOPUP"));
    }
    public void UpdateResource() // ���� �Ű������� �����ϰ� �޾ƿ� �����͸� ������ �̵���Ű��
    {
        playerstamina = CurrencyManager.Instance.GetCurrency(CurrencyType.Activity);
        playergold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        playercrystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
    }

    public void ShowResource()
    {
        CountText = GetComponentsInChildren<TextMeshProUGUI>();
        CountText[0].text = $"Stamina: {playerstamina} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Activity].MaxCount}";
        CountText[1].text = $"Gold: {playergold} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Gold].MaxCount}";
        CountText[2].text = $"Crystal: {playercrystal} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Dia].MaxCount}";
    }
}
