using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BattleCamera : MonoBehaviour
{
    public CinemachineVirtualCamera MainViewCamera;
    public CinemachineVirtualCamera CharacterCamera;
    public CinemachineVirtualCamera TargetCamera;

    private void Awake()
    {
        StageManager.Instance.BattleCamera = this;        
    }

    public void ShowCharacter()
    {
        //Transform pos = CharacterCamera.transform;
        //CharacterCamera.transform.position = new Vector3(character.transform.position.x, pos.position.y, pos.position.z);
        CharacterCamera.Priority = 20;
        TargetCamera.Priority = 0;
        MainViewCamera.Priority = 0;
    }

    public void ShowEnemy()
    {
        //Transform pos = CharacterCamera.transform;
        //TargetCamera.transform.position = new Vector3(target.transform.position.x, pos.position.y, pos.position.z);
        TargetCamera.Priority = 20;
        CharacterCamera.Priority = 0;
        MainViewCamera.Priority = 0;

    }

    public void ShowMainView()
    {
        MainViewCamera.Priority = 20;
        CharacterCamera.Priority = 0;
        TargetCamera.Priority = 0;
    }
}
