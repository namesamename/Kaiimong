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
        //playerstamina = stamina;
        //playergold = gold;
        //playercrystal = crystal;
    }

    public void ShowResource()
    {
        staminaText.text = $"Stamina: {playerstamina} / {maxplayerstamina}";
        goldText.text = $"Gold: {playergold}";
        crystalText.text = $"Crystal: {playercrystal}";
    }
}
