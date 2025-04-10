using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    // 싱글턴 초기화 및 DontDestroyOnLoad 처리
    protected void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public void ShowPopupPrefab(GameObject prefab)
    {
        Transform canvas = GameObject.Find("Canvas")?.transform;
        GameObject obj = Instantiate(prefab, canvas);
        RectTransform rect = obj.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = obj.GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            canvasGroup = obj.AddComponent<CanvasGroup>();
        }

        // 초기 상태 설정 (아래 + 투명)
        rect.anchoredPosition = new Vector2(0, -200f);
        canvasGroup.alpha = 0f;

        // 애니메이션: 올라오면서 나타나기
        rect.DOAnchorPos(Vector2.zero, 0.7f).SetEase(Ease.OutBack);
        canvasGroup.DOFade(1f, 0.7f).SetEase(Ease.InOutSine);
    }

    public void SceneLoader(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
