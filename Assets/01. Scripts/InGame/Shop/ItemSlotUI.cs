using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;

    public void Setup(Package package)
    {
        // 아이콘이 아직 없으므로 임시로 랜덤 색상 적용
        iconImage.color = GetRandomColor();
        countText.text = $"x{package.ItemCount}";
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
