using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSceneLoader : MonoBehaviour
{
    public string scenename;
    public void SceneLoad()
    {
        UIManager.Instance.SceneLoader(scenename);
    }
}
