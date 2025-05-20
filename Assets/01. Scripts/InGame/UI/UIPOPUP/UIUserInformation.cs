using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class UIUserInformation : MonoBehaviour
{ 
    //�Ҵ��� ������Ʈ�� 
    [SerializeField] private TextMeshProUGUI NickName;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI exp;
    [SerializeField] private Slider Slider;

    private Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
    }
    //�ӽ÷� �����ϴ� �����͵� - �÷��̾� ������ 
    private void Start()
    {
        ShowUserInfo();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.ProfileScene));

    }
    private void Update()
    {
        ShowUserInfo();

    }
    public void ShowUserInfo() //�̸�, 
    {
        NickName.text = CurrencyManager.Instance.GetUserName();
        Level.text = "LV." + CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel);
        exp.text =$"{CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP)}/{LevelUpSystem.PlayerNeedExp[CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)]}";
        float fillAmount = (float)CurrencyManager.Instance.GetCurrency(CurrencyType.UserEXP) / LevelUpSystem.PlayerNeedExp[CurrencyManager.Instance.GetCurrency(CurrencyType.UserLevel)];
        Slider.value = Mathf.Clamp01(fillAmount);
    }






}
