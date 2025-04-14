using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class UserInformation : MonoBehaviour
{ 
    //할당할 오브젝트들 
    [SerializeField] private TextMeshProUGUI NickName;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI exp;
    [SerializeField] private Image expFillImage;
   
    //임시로 지정하는 데이터들 - 플레이어 데이터 
    [SerializeField] private string UserNickname;
    [SerializeField] private int UserLevel;
    [SerializeField] private int Userexp;
    [SerializeField] private int UserMaxexp;

    private void Start()
    {
        ShowUserInfo();
    }
    private void Update()
    {
        ShowUserInfo();

    }

    public void UserinfoUpdate() //추후 데이터를 받아 업데이트하는 메서드
    {
        //UserNickname = GameManager.player.name 예시
     }
    public void ShowUserInfo() //이름, 
    {
        NickName.text = UserNickname;
        Level.text = "LV." + UserLevel;
        exp.text = Userexp + " / " + UserMaxexp;
        float fillAmount = (float)Userexp / UserMaxexp;
        expFillImage.fillAmount = Mathf.Clamp01(fillAmount);
    }






}
