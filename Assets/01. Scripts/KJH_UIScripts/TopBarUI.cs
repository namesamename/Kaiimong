using UnityEngine;
using TMPro;

public class TopBarUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI crystalText;

   // 플레이어의 데이터
    [SerializeField] private int maxplayerstamina;
    [SerializeField] private int playerstamina;
    [SerializeField] private int playergold;
    [SerializeField] private int playercrystal;
    //플레이어 데이터 가지고 오는 코드 , playerdata.gold 등등

    private void Start()
    {
        UpdateResource();
        ShowResource();
        // 시작 시 한 번 갱신해서 보이게
    }

    public void UpdateResource() // 추후 매개변수를 제거하고 받아온 데이터를 가지고 이동시키기
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
