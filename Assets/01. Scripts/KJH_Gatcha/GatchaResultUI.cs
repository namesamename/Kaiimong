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

            //  재화 부족 등으로 실패했을 경우
            if (results == null || results.Count == 0)
            {
                Debug.LogWarning("재화 부족 또는 뽑기 실패!");
                UIManager.Instance.ShowPopup<PopupCurrencyLack>(); // 재화 부족 팝업
                return;
            }

            GatchaResultHolder.results = results;

            //  기존 결과 오브젝트 제거
            foreach (Transform child in resultGrid)
            {
                Destroy(child.gameObject);
            }

            StopAllCoroutines(); // 중복 방지
            StartCoroutine(ShowResults());
        }
        else
        {
            Debug.LogWarning("DrawMore 실패: executor 또는 session이 null");
        }
    }



}
