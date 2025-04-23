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
            GatchaResultHolder.results = results;

            foreach (Transform child in resultGrid)
                Destroy(child.gameObject);

            StartCoroutine(ShowResults());
        }
        else
        {
            Debug.LogWarning("DrawMore ����: executor �Ǵ� session�� null");
        }
    }


}
