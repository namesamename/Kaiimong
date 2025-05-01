using UnityEngine;
using TMPro;

public class UIHistoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI grade;
    [SerializeField] private TextMeshProUGUI type;
    [SerializeField] private TextMeshProUGUI time;

    public void Setup(GatchaHistoryEntry entry)
    {
        characterName.text = entry.characterName;
        grade.text = entry.grade.ToString();
        type.text = entry.gatchaType.ToString();
        time.text = entry.timestamp;
    }
}
