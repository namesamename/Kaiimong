using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid; // 프리팹이 나올 위치
    [SerializeField] private Button retryButton;   // 더 뽑기 버튼
    [SerializeField] private Button toLobbyButton; // 로비 이동 버튼
    [SerializeField] private GameObject sCountTextObj; // S등급 카운트 텍스트
    [SerializeField] private Image specialEffectImage; // S등급 연출용 이미지
    [SerializeField] private Button skipButton; // 결과 연출 스킵 버튼

    private bool isSkipping = false;
    private Coroutine showResultRoutine;

    private AudioClip revealSfx;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.PlaySFX(revealSfx);
        }
    }
    private async void Start()
    {
        retryButton.interactable = false;
        toLobbyButton.interactable = false;
        skipButton.gameObject.SetActive(true);  // 항상 보여지게 수정
        sCountTextObj.SetActive(false);

        toLobbyButton.onClick.RemoveAllListeners();
        toLobbyButton.onClick.AddListener(OnGoToLobby);
        skipButton.onClick.AddListener(SkipShowResults);
        revealSfx = await AddressableManager.Instance.LoadAsset<AudioClip>(AddreassablesType.SoundEffectFx, 2);

        showResultRoutine = StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()
    {
        skipButton.interactable = true;

        // S등급 등장 시 연출
        bool hasSGrade = GatchaResultHolder.results.Exists(c => c.Grade == Grade.S);
        if (hasSGrade)
        {
            specialEffectImage.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            specialEffectImage.gameObject.SetActive(false);
        }

        GameObject prefab = Resources.Load<GameObject>("GatchaPrefab/CharacterResult");
        if (prefab == null)
        {
            Debug.LogError("CharacterResult 프리팹 로드 실패!");
            yield break;
        }

        foreach (var character in GatchaResultHolder.results)
        {   AudioManager.PlaySFX(revealSfx);
            var obj = Instantiate(prefab, resultGrid);
            var ui = obj.GetComponent<UICharacterResult>();
            ui.Setup(character);

            

            if (isSkipping) continue;
            yield return new WaitForSeconds(0.3f);
        }

        FinishResultDisplay();
    }

    private void FinishResultDisplay()
    {
        retryButton.interactable = true;
        toLobbyButton.interactable = true;
        sCountTextObj.SetActive(true);
        skipButton.gameObject.SetActive(false);

        // 등장이펙트
        sCountTextObj.transform.localScale = Vector3.zero;
        sCountTextObj.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
    }

    public void SkipShowResults()
    {
        if (showResultRoutine != null)
        {
            StopCoroutine(showResultRoutine);
        }

        isSkipping = true;

        // 기존 생성된 결과 제거
        foreach (Transform child in resultGrid)
        {
            Destroy(child.gameObject);
        }

        GameObject prefab = Resources.Load<GameObject>("GatchaPrefab/CharacterResult");
        foreach (var character in GatchaResultHolder.results)
        {
            var obj = Instantiate(prefab, resultGrid);
            var ui = obj.GetComponent<UICharacterResult>();
            ui.Setup(character);
        }

        FinishResultDisplay();
    }

    public async void OnDrawMore()
    {
        var executor = FindObjectOfType<GatchaExecutor>();
        var session = GatchaResultHolder.session;

        if (executor != null && session != null)
        {
            var results = await executor.DrawWithSession(session);

            if (results == null || results.Count == 0)
            {
                Debug.LogWarning("재화 부족 또는 뽑기 실패!");
                await UIManager.Instance.ShowPopup<PopupCurrencyLack>();
                return;
            }

            GatchaResultHolder.results = results;

            foreach (Transform child in resultGrid)
            {
                Destroy(child.gameObject);
            }

            isSkipping = false;
            retryButton.interactable = false;
            toLobbyButton.interactable = false;
            sCountTextObj.SetActive(false);
            skipButton.gameObject.SetActive(true);

            showResultRoutine = StartCoroutine(ShowResults());
        }
    }

    public void OnGoToLobby()
    {
        SceneLoader.Instance.ChangeScene(SceneState.PickupScene);
    }
}
