using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHistoryItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI grade;
    [SerializeField] private TextMeshProUGUI type;
    [SerializeField] private TextMeshProUGUI time;

    [SerializeField] private Image backgroundImage;

    public void Setup(GatchaHistoryEntry entry)
    {
        characterName.text = entry.characterName;
        grade.text = entry.grade.ToString();
        type.text = entry.gatchaType.ToString();
        time.text = entry.timestamp;

        switch (entry.grade)
        {
            case Grade.S:
                backgroundImage.color = new Color(1f, 0.85f, 0f); // Gold
                break;
            case Grade.A:
                backgroundImage.color = new Color(0.4f, 0.8f, 1f); // Sky Blue
                break;
            case Grade.B:
                backgroundImage.color = new Color(0.8f, 0.8f, 0.8f); // Light Gray
                break;
            default:
                backgroundImage.color = Color.white;
                break;
        }
    }
}
