using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopUIManager : Singleton<ShopUIManager>
{
    [Header("데이터")]
    public List<PackageGroup> packageGroups;
    public List<Shop> shopData;

    [Header("UI")]
    public Transform contentParent; // ScrollView > Content
    public GameObject packageGroupItemPrefab; // 연결한 PackageGroupItemUI 프리팹


    [SerializeField] private Button backbutton;
    [SerializeField] private Button lobbybutton;

    [SerializeField] private TextMeshProUGUI goldtext;
    [SerializeField] private TextMeshProUGUI crystaltext;
    public int gold;
    public int crystal;

    private void Start()
    {
        LoadPackages();
        SettingCurrency();
        SceneChange();
    }

    private void LoadPackages()
    {
        foreach (var group in packageGroups)
        {
            Shop shop = shopData.Find(s => s.name == $"Shop_{group.ShopID}");

            if (shop == null)
            {
                Debug.LogWarning($"Shop 데이터 없음: ShopID {group.ShopID}");
                continue;
            }

            GameObject go = Instantiate(packageGroupItemPrefab, contentParent);
            var ui = go.GetComponent<PackageGroupItemUI>();
            ui.Setup(group, shop);
        }
    }


    public void SceneChange()
    {
        backbutton.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));
        lobbybutton.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));

    }

    public void SettingCurrency()//티켓,보석 재화를 
    {
        gold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
        goldtext.text = $"{gold.ToKNumber()}";
        crystaltext.text = crystal.ToString();
    }
}
