using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI returnActivityText;
    [SerializeField] private Image activityImage;
    public bool CanClick = false;

    void Start()
    {
        StageManager.Instance.OnLose += SetLoseUI;
    }

    private void Update()
    {
        //Debug.Log(CanClick);
        if (CanClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Time.timeScale = 1f;
                StageManager.Instance.ToStageSelectScene();
            }
        }
    }

    public void SetLoseUI()
    {
        CanClick = false;
        returnActivityText.text = $": 행동력 반환 +{StageManager.Instance.returnActivityPoints}";
        //activityImage =
    }

    public void UnSubscribeLoseUI()
    {
        CanClick = false;
        StageManager.Instance.OnLose -= SetLoseUI;
    }
}
