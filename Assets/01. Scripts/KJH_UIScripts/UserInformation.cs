using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class UserInformation : MonoBehaviour
{ 
    //�Ҵ��� ������Ʈ�� 
    [SerializeField] private TextMeshProUGUI NickName;
    [SerializeField] private TextMeshProUGUI Level;
    [SerializeField] private TextMeshProUGUI exp;
    [SerializeField] private Image expFillImage;
   
    //�ӽ÷� �����ϴ� �����͵� - �÷��̾� ������ 
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

    public void UserinfoUpdate() //���� �����͸� �޾� ������Ʈ�ϴ� �޼���
    {
        //UserNickname = GameManager.player.name ����
     }
    public void ShowUserInfo() //�̸�, 
    {
        NickName.text = UserNickname;
        Level.text = "LV." + UserLevel;
        exp.text = Userexp + " / " + UserMaxexp;
        float fillAmount = (float)Userexp / UserMaxexp;
        expFillImage.fillAmount = Mathf.Clamp01(fillAmount);
    }






}
