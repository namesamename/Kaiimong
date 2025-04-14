using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonSceneLoader : MonoBehaviour
{ 
    public void SceneLoad(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }
}
