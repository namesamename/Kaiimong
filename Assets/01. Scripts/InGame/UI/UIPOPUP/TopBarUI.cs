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
        CountText = GetComponentsInChildren<TextMeshProUGUI>();
 


        CountText[0].text = $"�ൿ��: {playerstamina} / {CurrencyManager.Instance.GetCurrency(CurrencyType.CurMaxStamina)}";
        CountText[1].text = $"����: {playergold.ToKNumber()}";
        CountText[2].text = $"��������: {playercrystal}";
    }

}
