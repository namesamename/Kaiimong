using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid; // �������� ���� ��ġ
    [SerializeField] private Button retryButton;   // �� �̱� ��ư
    [SerializeField] private Button toLobbyButton; // �κ� �̵� ��ư
    [SerializeField] private GameObject sCountTextObj; // S��� ī��Ʈ �ؽ�Ʈ
    [SerializeField] private Image specialEffectImage; // S��� ����� �̹���
    [SerializeField] private Button skipButton; // ��� ���� ��ŵ ��ư

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
        skipButton.gameObject.SetActive(true);  // �׻� �������� ����
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

        // S��� ���� �� ����
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
            Debug.LogError("CharacterResult ������ �ε� ����!");
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

        // ��������Ʈ
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

        // ���� ������ ��� ����
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
                Debug.LogWarning("��ȭ ���� �Ǵ� �̱� ����!");
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
