using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI returnActivityText;
    [SerializeField] private Image activityImage;
    public bool CanClick = false;
    private bool setComplete = false;

    void Awake()
    {
        StageManager.Instance.OnLose -= SetLoseUI;
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
        if (!setComplete)
        {
            setComplete = true;
            CanClick = false;
            int returnPoint = (int)StageManager.Instance.returnActivityPoints;
            returnActivityText.text = $": 행동력 반환 +{returnPoint}";
            //activityImage =
        }
    }

    public void UnSubscribeLoseUI()
    {
        CanClick = false;
        StageManager.Instance.OnLose -= SetLoseUI;
    }
}
