using System.Collections;
using UnityEngine;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid; //�������� ���� ��ġ ���̾ƿ� ����

    private void Start()
    {
        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()  // ���� ���� �Ǿ��� �� ��í �����ֱ⸦ ����
    {
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

            yield return new WaitForSeconds(0.3f); // ���� ���
        }
    }

    public void OnDrawMore()
    {
        var executor = FindObjectOfType<GatchaExecutor>();
        var session = GatchaResultHolder.session;

        if (executor != null && session != null)
        {
            var results = executor.DrawWithSession(session);

            //  ��ȭ ���� ������ �������� ���
            if (results == null || results.Count == 0)
            {
                Debug.LogWarning("��ȭ ���� �Ǵ� �̱� ����!");
                UIManager.Instance.ShowPopup<PopupCurrencyLack>(); // ��ȭ ���� �˾�
                return;
            }

            GatchaResultHolder.results = results;

            //  ���� ��� ������Ʈ ����
            foreach (Transform child in resultGrid)
            {
                Destroy(child.gameObject);
            }

            StopAllCoroutines(); // �ߺ� ����
            StartCoroutine(ShowResults());
        }
        else
        {
            Debug.LogWarning("DrawMore ����: executor �Ǵ� session�� null");
        }
    }



}
