using TMPro;
using UnityEngine;

public class StageSlot : MonoBehaviour
{
    public Stage Stage;
    public TextMeshPro StageName;

    private void Awake()
    {
        StageName = GetComponentInChildren<TextMeshPro>();
    }
}
