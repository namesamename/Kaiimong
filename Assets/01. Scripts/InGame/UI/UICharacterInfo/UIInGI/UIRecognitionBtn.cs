using UnityEngine;
using UnityEngine.UI;

public class UIRecognitionBtn : MonoBehaviour, ISetPOPUp
{
    Button[] buttons;
    UIRecognitionPOPUP ingiPOPUP;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
        ingiPOPUP = FindAnyObjectByType<UIRecognitionPOPUP>();
    }

    public void Initialize()
    {

        buttons[0].onClick.RemoveAllListeners();
        buttons[1].onClick.RemoveAllListeners();

        if (ingiPOPUP.slot.IsIngiBreakOK(ingiPOPUP.slot.NeedTable) && GlobalDataTable.Instance.DataCarrier.GetSave().Recognition <= 3)
        {
            buttons[0].interactable = true;
            buttons[0].onClick.AddListener(UpGrade);
        }
        else
        {
            buttons[0].interactable = false;
        }

        buttons[1].onClick.AddListener(ingiPOPUP.Destroy);
    }


    public void UpGrade()
    {
        foreach(int ID in ingiPOPUP.slot.NeedTable.Keys)
        {
            if(ID == 0)
            {
                CurrencyManager.Instance.SetCurrency(CurrencyType.Gold, -ingiPOPUP.slot.NeedTable[ID]);
            }
            else if(ID !=  0 && ID != -1)
            {
                ItemManager.Instance.SetitemCount(ID, -ingiPOPUP.slot.NeedTable[ID]);
            }
           
        }
        GlobalDataTable.Instance.DataCarrier.GetSave().Recognition += 1;


        SaveDataBase.Instance.SaveSingleData(GlobalDataTable.Instance.DataCarrier.GetSave());


        ingiPOPUP.Initialize();

        CharacterInfoHUDManager.Instance.Initialize();

    }
}
