using System.Collections;
using UnityEngine;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid; //프리펩이 나올 위치 레이아웃 지정

    private void Start()
    {
        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()  // 씬이 실행 되었을 때 가챠 보여주기를 진행
    {
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

            yield return new WaitForSeconds(0.3f); // 순차 출력
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
            Debug.LogWarning("DrawMore 실패: executor 또는 session이 null");
        }
    }


}
