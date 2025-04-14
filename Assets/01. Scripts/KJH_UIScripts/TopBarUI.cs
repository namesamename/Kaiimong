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

    public int stamina1;
    public int gold1;
    public int crystal1;



    private void Start()
    {
        UpdateResource(stamina1,gold1, crystal1); // ���� �� �� �� �����ؼ� ���̰�
    }

    public void UpdateResource(int stamina, int gold, int crystal) // ���� �Ű������� �����ϰ� �޾ƿ� �����͸� ������ �̵���Ű��
    {
        playerstamina = stamina;
        playergold = gold;
        playercrystal = crystal;
        ShowResource();
    }

    public void ShowResource()
    {
        staminaText.text = $"Stamina: {playerstamina} / {maxplayerstamina}";
        goldText.text = $"Gold: {playergold}";
        crystalText.text = $"Crystal: {playercrystal}";
    }
}
