using UnityEngine;
using TMPro;

public class TopBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI crystalText;

   // �÷��̾��� ������
    [SerializeField] private int maxplayerstamina;
    [SerializeField] private int playerstamina;
    [SerializeField] private int playergold;
    [SerializeField] private int playercrystal;
    //�÷��̾� ������ ������ ���� �ڵ� , playerdata.gold ���

    private void Start()
    {
        UpdateResource();
        ShowResource();
        // ���� �� �� �� �����ؼ� ���̰�
    }

    public void UpdateResource() // ���� �Ű������� �����ϰ� �޾ƿ� �����͸� ������ �̵���Ű��
    {
        playerstamina = CurrencyManager.Instance.GetCurrency(CurrencyType.Activity);
        playergold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        playercrystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
    }

    public void ShowResource()
    {
        staminaText.text = $"Stamina: {playerstamina} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Activity].MaxCount}";
        goldText.text = $"Gold: {playergold} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Gold].MaxCount}";
        crystalText.text = $"Crystal: {playercrystal} / {GlobalDataTable.Instance.currency.CurrencyDic[CurrencyType.Dia].MaxCount}";
    }
}
