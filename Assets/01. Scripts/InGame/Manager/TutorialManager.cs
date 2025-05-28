using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TutorialManager : Singleton<TutorialManager>
{
    GameObject tutorialPrefab;
   public  GameObject CurPre;
    Canvas canvas;

    public Action TutorialAction;

    int Index = 1;
    int CharIndex = 1;
    public async void NextTutorial()
    {

        if(!CurrencyManager.Instance.GetIsTutorial())
        {
            if(CurPre != null)
            {
                CurPreDelete();
            }
            canvas = FindAnyObjectByType<Canvas>();
             tutorialPrefab = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.Tutorial, Index.ToString());
            CurPre = Instantiate(tutorialPrefab, canvas.transform);

            if (Index == 4)
            {
                CurPre.transform.SetSiblingIndex(1);
            }
            Index++;
        }
    }


    public async void NextCharTutorialAsync()
    {
        if (!CurrencyManager.Instance.GetIsTutorial())
        {
            if (CurPre != null)
            {
                CurPreDelete();
            }

            if(CharIndex == 18)
            {
                CurrencyManager.Instance.ClearCharTutorial();
                CharacterInfoHUDManager asd = FindAnyObjectByType< CharacterInfoHUDManager >();
                asd.Initialize();
                return;
            }
            canvas = FindAnyObjectByType<Canvas>();
            tutorialPrefab = await AddressableManager.Instance.LoadPrefabs(AddreassablesType.CharTutorial, Index.ToString());
            CurPre = Instantiate(tutorialPrefab, canvas.transform);


            CharIndex++;
        }
    }
    

    public int GetIndex()
    {
        return Index;
    }

    public void CurPreDelete()
    {
        if(CurPre != null) 
        Destroy(CurPre.gameObject);
    }
}
