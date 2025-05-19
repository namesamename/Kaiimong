using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    Image[] images;
    Button[] buttons;

    Transform StartButtoObject;
    Transform EndButtonObject;
    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        buttons = GetComponentsInChildren<Button>();

        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        buttons[0].onClick.AddListener(() => StartCoroutine(StartAni()));
        buttons[1].onClick.AddListener(OnApplicationQuit);


        StartButtoObject = transform.GetChild(3);
        EndButtonObject = transform.GetChild(4);
    }


    public IEnumerator StartAni()
    {
        StartButtoObject.gameObject.SetActive(false);
        EndButtonObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        setLight();
        yield return new WaitForSeconds(1f);
        setOff();
        yield return new WaitForSeconds(1f);
        setLight();
        yield return new WaitForSeconds(0.5f);
        setOff();
        yield return new WaitForSeconds(0.5f);
        setLight();
        yield return new WaitForSeconds(0.25f);
        setOff();
        yield return new WaitForSeconds(0.25f);
        setLight();
        yield return new WaitForSeconds(0.125f);
        setOff();
        yield return new WaitForSeconds(0.125f);

        setLight();
        yield return new WaitForSeconds(0.0625f);
        setOff();
        yield return new WaitForSeconds(0.0625f);

        setLight();
        yield return new WaitForSeconds(0.03125f);
        setOff();
        yield return new WaitForSeconds(0.03125f);

        setLight();
        yield return new WaitForSeconds(1f);
        SceneLoader.Instance.ChangeScene(SceneState.LobbyScene);

    }

    public void setLight()
    {
        images[0].enabled = false;
        images[1].enabled = true;
    }

    public void setOff()
    {
        images[0].enabled = true;
        images[1].enabled = false;
    }

    public void OnApplicationQuit()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }

}
