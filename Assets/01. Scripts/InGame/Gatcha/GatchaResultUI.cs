using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid; // �������� ���� ��ġ
    [SerializeField] private Button retryButton;   // �� �̱� ��ư
    [SerializeField] private Button toLobbyButton; // �κ� �̵� ��ư

    [SerializeField] private Image specialEffectImage;

    private void Start()
    {
        // ��ư�� ������ ������ ������ ��Ȱ��ȭ
        retryButton.interactable = false;
        toLobbyButton.interactable = false;
        toLobbyButton.onClick.RemoveAllListeners();
        toLobbyButton.onClick.AddListener(OnGoToLobby);



        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()  // ��� ĳ���͵��� ���������� �����ִ� ����
    {
        bool hasSGrade = false;
        foreach (var character in GatchaResultHolder.results)
        {
            if (character.Grade == Grade.S)
            {
                hasSGrade = true;
                break;
            }
        }
        if (hasSGrade)
        {
            specialEffectImage.gameObject.SetActive(true);  // ����� �̹��� ����
            yield return new WaitForSeconds(1.0f);         // 1�� ���
            specialEffectImage.gameObject.SetActive(false); // �ٽ� ����
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

            yield return new WaitForSeconds(0.3f); // ĳ���� ���� ��� ����
        }

        // ��� ĳ���� ������ ���� �� ��ư Ȱ��ȭ
        retryButton.interactable = true;
        toLobbyButton.interactable = true;


    }

    public void OnDrawMore()
    {
        var executor = FindObjectOfType<GatchaExecutor>();
        var session = GatchaResultHolder.session;

        if (executor != null && session != null)
        {
            var results = executor.DrawWithSession(session);

            // ��ȭ ���� ������ �������� ���
            if (results == null || results.Count == 0)
            {
                Debug.LogWarning("��ȭ ���� �Ǵ� �̱� ����!");
                UIManager.Instance.ShowPopup<PopupCurrencyLack>(); // ��ȭ ���� �˾� ǥ��
                return;
            }

            // ��� ���� �� UI �籸��
            GatchaResultHolder.results = results;

            // ���� ��� ������Ʈ ����
            foreach (Transform child in resultGrid)
            {
                Destroy(child.gameObject);
            }

            // ��ư �ٽ� ��װ� ���� �����
            retryButton.interactable = false;
            toLobbyButton.interactable = false;

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
