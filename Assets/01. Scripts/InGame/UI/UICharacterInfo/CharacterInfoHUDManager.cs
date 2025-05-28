

using UnityEngine;

public class CharacterInfoHUDManager : Singleton<CharacterInfoHUDManager>
{
    private void Start()
    {
        if(!CurrencyManager.Instance.GetIsChartutorial())
        {
            TutorialManager.Instance.NextCharTutorialAsync();
        }
        Initialize();
    }
    public void Initialize()
    {
        CharacterInfoHUD[] characterInfos = GetComponentsInChildren<CharacterInfoHUD>();

        for (int i = 0; i < characterInfos.Length; i++)
        {
            characterInfos[i].InitialIze(GlobalDataTable.Instance.DataCarrier.GetCharacter(), GlobalDataTable.Instance.DataCarrier.GetSave());
        }
    }

}
