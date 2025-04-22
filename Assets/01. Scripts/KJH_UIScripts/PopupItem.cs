using UnityEngine;
using TMPro;
using UnityEngine.UI;




public class PopupItem : UIPopup
{

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI information;
    [SerializeField] private TextMeshProUGUI getPath;
    [SerializeField] private TextMeshProUGUI itemQuantity;
    [SerializeField] private Image itemImage;

    //임시 테스트 코드
    [SerializeField] private string testname;
    [SerializeField] private string testinfo;
    [SerializeField] private string testgetPath;
    [SerializeField] private string testitemQuantity;
    [SerializeField] private Sprite testitemImage;


    private void Awake()
    {
        itemName.text = testname;
        information.text = testinfo;
        getPath.text = testgetPath;
        itemQuantity.text = testitemQuantity;
        itemImage.sprite = testitemImage;
    }


}
