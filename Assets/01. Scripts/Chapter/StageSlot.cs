using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public Stage Stage;

    private void Awake()
    {
    }

    public void Initialize(int Id)
    {
        //ChapterManager.Instance.InitializeStage(Id);
    }
}
