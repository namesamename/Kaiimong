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


}
