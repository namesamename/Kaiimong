using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGradeTable
{
    public Dictionary<int, CharacterUpgradeTable> RecoDic = new Dictionary<int, CharacterUpgradeTable>();
    public void Initialize()
    {
        CharacterUpgradeTable[] Reco = Resources.LoadAll<CharacterUpgradeTable>("Reco");
    
        foreach (CharacterUpgradeTable RecoInfo in Reco)
        {
            RecoDic[RecoInfo.ID] = RecoInfo;
        }
   
    }

    public CharacterUpgradeTable GetRecoInfoToID(int RecoID)
    {
        if (RecoDic.ContainsKey(RecoID) && RecoDic[RecoID] != null)
        {
            return RecoDic[RecoID];
        }
        else
        {
            Debug.Log("This ID is incorrect");
            return null;
        }
    }

    public List<CharacterUpgradeTable> GetRecoList(int CharacterID)
    {
        List<CharacterUpgradeTable> tables = new List<CharacterUpgradeTable>();
        foreach(CharacterUpgradeTable character in RecoDic.Values)
        {
            tables.Add(character);
        }
        List<CharacterUpgradeTable> CharacterList = tables.FindAll(x => x.CharacterID == CharacterID);

        if(CharacterList == null || CharacterList.Count < 3)
        {
            return null;
        }
        else
        {
            return CharacterList;
        }

     
    }

  
  

}
