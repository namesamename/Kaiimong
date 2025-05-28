using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ShopUIManager : Singleton<ShopUIManager>
{
    [Header("������")]
    public List<PackageGroup> packageGroups;
    public List<Shop> shopData;

    [Header("UI")]
    public Transform contentParent; // ScrollView > Content
    public GameObject packageGroupItemPrefab; // ������ PackageGroupItemUI ������


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
                Debug.LogWarning($"Shop ������ ����: ShopID {group.ShopID}");
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

    public void SettingCurrency()//Ƽ��,���� ��ȭ�� 
    {
        gold = CurrencyManager.Instance.GetCurrency(CurrencyType.Gold);
        crystal = CurrencyManager.Instance.GetCurrency(CurrencyType.Dia);
        goldtext.text = $"{gold.ToKNumber()}";
        crystaltext.text = crystal.ToString();
    }
}
