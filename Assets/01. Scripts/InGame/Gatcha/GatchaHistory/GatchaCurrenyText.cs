using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatchaCurrenyText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI crystaltxt;
    [SerializeField] private TextMeshProUGUI tickettxt;
    [SerializeField] private TextMeshProUGUI GatchaCount;

    public int crystal;
    public int ticket;

    // Start is called before the first frame update
    private void Start()
    {
        SettingCurrency();
    }

    public void SettingCurrency()
    {
        ticket = CurrencyManager.Instance.GetCurrency(CurrencyType.Gacha);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);

        tickettxt.text = ticket.ToString();
        crystaltxt.text =crystal.ToString();
        GatchaCount.text = GatchaManager.Instance.gatchaDrawCount.ToString() + " / 70¹øÂ° È¹µæ¿¡ Sµî±Þ È®Á¤!";
    }

}
