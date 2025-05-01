using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIRecognitionBoard : MonoBehaviour, ISetPOPUp
{
    TextMeshProUGUI[] Textmesh;
    private void Awake()
    {
        Textmesh = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Initialize()
    {
        int Rec = GlobalDataTable.Instance.DataCarrier.GetSave().Recognition;
        int Id = GlobalDataTable.Instance.DataCarrier.GetSave().ID;
        List<CharacterUpgradeTable> list = GlobalDataTable.Instance.Upgrade.GetRecoList(GlobalDataTable.Instance.DataCarrier.GetSave().ID);
        Textmesh[0].text = Rec.ToString();
        SetText(Rec, list);
    }


    public void SetText(int Reco, List<CharacterUpgradeTable> list)
    {

        if (Reco <= 2)
        {
            Textmesh[1].text = list[Reco].Name;
            Textmesh[2].text = list[Reco].Des;
        }
        else
        {
            Textmesh[1].text = "Max";
            Textmesh[2].text = "Max";
        }

    }


}
