using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngiBoard : MonoBehaviour, ISetPOPUp
{
    TextMeshProUGUI[] Textmesh;
    private void Awake()
    {
        Textmesh = GetComponentsInChildren<TextMeshProUGUI>();
    }

    public void Initialize()
    {
        int Rec = ImsiGameManager.Instance.GetCharacterSaveData().Recognition;
        int Id = ImsiGameManager.Instance.GetCharacterSaveData().ID;
        List<CharacterUpgradeTable> list = GlobalDataTable.Instance.Upgrade.GetRecoList(ImsiGameManager.Instance.GetCharacterSaveData().ID);
        Textmesh[0].text = Rec.ToString();
        SetText(Rec, list);
    }


    public void SetText(int Reco, List<CharacterUpgradeTable> list)
    {
        Textmesh[1].text = list[Reco].Name;
        Textmesh[2].text = list[Reco].Des;

    }


}
