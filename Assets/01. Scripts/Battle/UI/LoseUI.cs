using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
        if (CanClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                StageManager.Instance.ToStageSelectScene();
            }
        }
    }

    public void SetLoseUI()
    {
        CanClick = true;
        returnActivityText.text = $": 행동력 반환 +{StageManager.Instance.returnActivityPoints}";
        //activityImage =
    }

    public void UnSubscribeLoseUI()
    {
        CanClick = false;
        StageManager.Instance.OnLose -= SetLoseUI;
    }
}
