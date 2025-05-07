using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid;         // 프리팹이 나올 위치
    [SerializeField] private Button retryButton;           // 더 뽑기 버튼
    [SerializeField] private Button toLobbyButton;         // 로비 이동 버튼
    [SerializeField] private Button skipButton;            // 연출 스킵 버튼
    [SerializeField] private GameObject sCountTextObj;     // 천장 카운트 텍스트
    [SerializeField] private Image specialEffectImage;     // S등급 연출용 이미지

    private bool isSkipped = false;

    private void Start()
    {
        retryButton.interactable = false;
        toLobbyButton.interactable = false;
        sCountTextObj.SetActive(false);

        toLobbyButton.onClick.RemoveAllListeners();
        toLobbyButton.onClick.AddListener(OnGoToLobby);

        skipButton.onClick.RemoveAllListeners();
        skipButton.onClick.AddListener(SkipResults);
        skipButton.gameObject.SetActive(true);

        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()
    {
        bool hasSGrade = GatchaResultHolder.results.Exists(c => c.Grade == Grade.S);

        if (hasSGrade)
        {
            specialEffectImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.0f);
            specialEffectImage.gameObject.SetActive(false);
        }

        GameObject prefab = Resources.Load<GameObject>("GatchaPrefab/CharacterResult");
        if (prefab == null)
        {
            Debug.LogError("CharacterResult 프리팹 로드 실패!");
            yield break;
        }

        foreach (var character in GatchaResultHolder.results)
        {
            var obj = Instantiate(prefab, resultGrid);
            var ui = obj.GetComponent<UICharacterResult>();
            ui.Setup(character);

            if (!isSkipped)
                yield return new WaitForSeconds(0.3f);  // 연출 간격
        }

        FinishResultDisplay();
    }

    private void SkipResults()
    {
        if (isSkipped) return;

        isSkipped = true;
        StopAllCoroutines();

        // 기존 캐릭터 UI 제거
        foreach (Transform child in resultGrid)
        {
            Destroy(child.gameObject);
        }

        // 프리팹 다시 로드
        GameObject prefab = Resources.Load<GameObject>("GatchaPrefab/CharacterResult");
        if (prefab == null)
        {
            Debug.LogError("CharacterResult 프리팹 로드 실패!");
            return;
        }

        // 전체 결과 즉시 출력
        foreach (var character in GatchaResultHolder.results)
        {
            var obj = Instantiate(prefab, resultGrid);
            var ui = obj.GetComponent<UICharacterResult>();
            ui.Setup(character);
        }

        FinishResultDisplay();
    }

    private void FinishResultDisplay()
    {
        retryButton.interactable = true;
        toLobbyButton.interactable = true;
        sCountTextObj.SetActive(true);
        skipButton.gameObject.SetActive(false);
    }

    public async void OnDrawMore()
    {
        var executor = FindObjectOfType<GatchaExecutor>();
        var session = GatchaResultHolder.session;

        if (executor != null && session != null)
        {
            var results = executor.DrawWithSession(session);

            if (results == null || results.Result.Count == 0)
            {
                Debug.LogWarning("재화 부족 또는 뽑기 실패!");
                await UIManager.Instance.ShowPopup<PopupCurrencyLack>();
                return;
            }

            GatchaResultHolder.results = results.Result;

            foreach (Transform child in resultGrid)
            {
                Destroy(child.gameObject);
            }

            retryButton.interactable = false;
            toLobbyButton.interactable = false;
            sCountTextObj.SetActive(false);
            isSkipped = false;
            skipButton.gameObject.SetActive(true);

            StopAllCoroutines();
            StartCoroutine(ShowResults());
        }
        else
        {
            Debug.LogWarning("DrawMore 실패: executor 또는 session이 null");
        }
    }

    public void OnGoToLobby()
    {
        SceneLoader.Instance.ChangeScene(SceneState.PickupScene);
    }
}
