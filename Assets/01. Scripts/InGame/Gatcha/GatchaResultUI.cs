using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid;         // �������� ���� ��ġ
    [SerializeField] private Button retryButton;           // �� �̱� ��ư
    [SerializeField] private Button toLobbyButton;         // �κ� �̵� ��ư
    [SerializeField] private Button skipButton;            // ���� ��ŵ ��ư
    [SerializeField] private GameObject sCountTextObj;     // õ�� ī��Ʈ �ؽ�Ʈ
    [SerializeField] private Image specialEffectImage;     // S��� ����� �̹���

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
            Debug.LogError("CharacterResult ������ �ε� ����!");
            yield break;
        }

        foreach (var character in GatchaResultHolder.results)
        {
            var obj = Instantiate(prefab, resultGrid);
            var ui = obj.GetComponent<UICharacterResult>();
            ui.Setup(character);

            if (!isSkipped)
                yield return new WaitForSeconds(0.3f);  // ���� ����
        }

        FinishResultDisplay();
    }

    private void SkipResults()
    {
        if (isSkipped) return;

        isSkipped = true;
        StopAllCoroutines();

        // ���� ĳ���� UI ����
        foreach (Transform child in resultGrid)
        {
            Destroy(child.gameObject);
        }

        // ������ �ٽ� �ε�
        GameObject prefab = Resources.Load<GameObject>("GatchaPrefab/CharacterResult");
        if (prefab == null)
        {
            Debug.LogError("CharacterResult ������ �ε� ����!");
            return;
        }

        // ��ü ��� ��� ���
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
                Debug.LogWarning("��ȭ ���� �Ǵ� �̱� ����!");
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
            Debug.LogWarning("DrawMore ����: executor �Ǵ� session�� null");
        }
    }

    public void OnGoToLobby()
    {
        SceneLoader.Instance.ChangeScene(SceneState.PickupScene);
    }
}
