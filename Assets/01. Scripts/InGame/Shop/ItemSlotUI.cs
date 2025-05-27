using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TMP_Text countText;

    public void Setup(Package package)
    {
        // �������� ���� �����Ƿ� �ӽ÷� ���� ���� ����
        iconImage.color = GetRandomColor();
        countText.text = $"x{package.ItemCount}";
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
