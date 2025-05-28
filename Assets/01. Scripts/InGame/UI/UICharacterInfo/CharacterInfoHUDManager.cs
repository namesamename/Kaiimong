

using UnityEngine;

public class CharacterInfoHUDManager : Singleton<CharacterInfoHUDManager>
{
    private void Start()
    {
        if(!CurrencyManager.Instance.GetIsChartutorial())
        {
            Debug.Log("왜 안뜨는거냐");
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
