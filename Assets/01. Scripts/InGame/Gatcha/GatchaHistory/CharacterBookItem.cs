using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading.Tasks;

public class CharacterBookItem : MonoBehaviour
{
    [SerializeField] private Image iconImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private TextMeshProUGUI nameText;

    public async Task Setup(Character character)
    {
        nameText.text = character.Name;

        // ��� �׵θ� ����
        switch (character.Grade)
        {
            case Grade.S:
                borderImage.color = new Color32(0xFF, 0xE4, 0x00, 0xFF); // ���
                break;
            case Grade.A:
                borderImage.color = new Color32(0x6C, 0x00, 0xFF, 0xFF); // ����
                break;
            case Grade.B:
                borderImage.color = new Color32(0x00, 0x71, 0xC8, 0xFF); // �Ķ�
                break;
        }

        // ��������Ʈ �ε�
        Sprite loadedSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CharacterIcon, character.ID);
        iconImage.sprite = loadedSprite;
    }
}
