using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GatchaResultUI : MonoBehaviour
{
    [SerializeField] private Transform resultGrid; // 프리팹이 나올 위치
    [SerializeField] private Button retryButton;   // 더 뽑기 버튼
    [SerializeField] private Button toLobbyButton; // 로비 이동 버튼

    private void Start()
    {
        // 버튼은 연출이 끝나기 전까지 비활성화
        retryButton.interactable = false;
        toLobbyButton.interactable = false;

        StartCoroutine(ShowResults());
    }

    private IEnumerator ShowResults()  // 결과 캐릭터들을 순차적으로 보여주는 연출
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

            yield return new WaitForSeconds(0.3f); // 캐릭터 순차 출력 간격
        }

        // 모든 캐릭터 연출이 끝난 후 버튼 활성화
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

            // 재화 부족 등으로 실패했을 경우
            if (results == null || results.Count == 0)
            {
                Debug.LogWarning("재화 부족 또는 뽑기 실패!");
                UIManager.Instance.ShowPopup<PopupCurrencyLack>(); // 재화 부족 팝업 표시
                return;
            }

            // 결과 갱신 및 UI 재구성
            GatchaResultHolder.results = results;

            // 기존 결과 오브젝트 제거
            foreach (Transform child in resultGrid)
            {
                Destroy(child.gameObject);
            }

            // 버튼 다시 잠그고 연출 재시작
            retryButton.interactable = false;
            toLobbyButton.interactable = false;

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
        SceneManager.LoadScene("MainScene"); // 원하는 씬 이름으로 수정 가능
    }
}
