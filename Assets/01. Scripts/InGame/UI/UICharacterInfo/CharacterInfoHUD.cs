using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;




public class CharacterInfoHUD : MonoBehaviour
{
    public CharacterInfoType CharacterInfoType;

    public async void InitialIze(Character character , CharacterSaveData saveData)
    {

        if(character == null)
        {
            Debug.Log("Characternull");
        }
        if(saveData == null)
        {
            Debug.Log("saveDatanull");
        }

        switch (CharacterInfoType)
        {

            case CharacterInfoType.Back:
                Button Backbutton = GetComponentInChildren<Button>();
                Backbutton.onClick.RemoveAllListeners();
                Backbutton.onClick.AddListener(() => SceneLoader.Instance.ChanagePreScene());

                transform.DOLocalMoveY(400, 1);
          
                break;
            case CharacterInfoType.Main:
                Button button = GetComponentInChildren<Button>();
                button.onClick.RemoveAllListeners();

                SceneLoader.Instance.RegisterSceneAction(SceneState.LobbyScene, ()=>AddressableManager.Instance.UnloadType(AddreassablesType.Illustration)); 
                button.onClick.AddListener(() => SceneLoader.Instance.ChangeScene(SceneState.LobbyScene));

                transform.DOLocalMoveY(400, 1);
                break;
            case CharacterInfoType.Skin:
                transform.DOLocalMoveY(400, 1);

                break;
            case CharacterInfoType.CharacterImage:
                Image images = GetComponent<Image>();
                images.sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.Illustration, character.ID);
                break;
            case CharacterInfoType.Name:
                TextMeshProUGUI[] textMeshPros = GetComponentsInChildren<TextMeshProUGUI>();
                Slider slider = GetComponentInChildren<Slider>();
                textMeshPros[0].text = GlobalDataTable.Instance.character.GetCharToID(saveData.ID).Name;
                textMeshPros[1].text = GlobalDataTable.Instance.character.GetCharToID(saveData.ID).Grade.ToString();
                textMeshPros[2].text = saveData.Love.ToString("N1");
                slider.value = saveData.Love / 100f;

                transform.DOLocalMoveX(-650, 1);
                break;
            case CharacterInfoType.Stat:
                TextMeshProUGUI[] textMeshPross = GetComponentsInChildren<TextMeshProUGUI>();
                Debug.Log("TextMeshProUGUI Count: " + textMeshPross.Length);
                textMeshPross[1].text = (character.Health + saveData.Level).ToString();
                textMeshPross[3].text = (character.Attack + saveData.Level).ToString();
                textMeshPross[5].text = (character.Defence + saveData.Level).ToString();
                textMeshPross[7].text = (character.Speed + saveData.Level).ToString();
                float Cir = (float)(character.CriticalPer + (saveData.Level* 0.01)) * 100;
                textMeshPross[9].text = $"{Cir}%";
                float CirAt = (float)(character.CriticalAttack + (saveData.Level * 0.01)) * 100;
                textMeshPross[11].text = $"{CirAt}%";

                transform.DOLocalMoveX(-650, 1);

                break;
             case CharacterInfoType.Dolpa:
                Button DolpaButton = GetComponentInChildren<Button>();
                Image[] Dolpaimages = GetComponentsInChildren<Image>();
                Dolpaimages[0].enabled = true;
                for (int i = 1; i < Dolpaimages.Length; i++)
                {
                    Dolpaimages[i].enabled = false;
                }

                for (int i = 1;i < saveData.Necessity+1; i++)
                {
                    Dolpaimages[i].enabled = true;
                }

                DolpaButton.onClick.RemoveAllListeners();
                DolpaButton.onClick.AddListener(async () => await UIManager.Instance.ShowPopup("breakthroughPopUP"));

                transform.DOLocalMoveY(-275, 1);
                break;
            case CharacterInfoType.Levelup:
                TextMeshProUGUI Level = GetComponentInChildren<TextMeshProUGUI>();
                Button LevelUpButton = GetComponentInChildren<Button>();
                LevelUpButton.onClick.RemoveAllListeners();
                Level.text = $"Level\n{saveData.Level}";
                LevelUpButton.onClick.AddListener(async () => await UIManager.Instance.ShowPopup("LevelupPopUp"));
                transform.DOLocalMoveX(450, 1);
                break;
            case CharacterInfoType.IngiUp:
                TextMeshProUGUI Ingi = GetComponentInChildren<TextMeshProUGUI>();
                Button IngiUpButton = GetComponentInChildren<Button>();
                IngiUpButton.onClick.RemoveAllListeners();
                Ingi.text = $"ÀÎÁö\n{saveData.Recognition}";
                IngiUpButton.onClick.AddListener(async () => await UIManager.Instance.ShowPopup("IngiPopUp"));
                transform.DOLocalMoveX(750, 1);
                break;
            case CharacterInfoType.ActiveSkill:
                Button[] buttons = GetComponentsInChildren<Button>();
                for(int i = 0; i < buttons.Length; i++)
                {
                    int index = i;
                    buttons[i].onClick.RemoveAllListeners();
                    buttons[i].onClick.AddListener(() => SetSkillPopup(index));
                }
                transform.DOLocalMoveX(600, 1);
                break;
            case CharacterInfoType.PassiveSkill:
                Image[] images1 = GetComponentsInChildren<Image>();
                for(int i = 0; saveData.Recognition > i; i++)
                {
                    images1[i].color = Color.white;
                }
                button = GetComponentInChildren<Button>();

                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(async () => await UIManager.Instance.ShowPopup("PassivePOPUP"));



                transform.DOLocalMoveX(600, 1);
                break;
            case CharacterInfoType.CEO:
                Button CEO = GetComponentInChildren<Button>();
                CEO.onClick.RemoveAllListeners();
                CEO.onClick.AddListener(() => { UIManager.Instance.characterIDType = UICharacterIDType.Lobby; UIManager.Instance.SetCharacterID(character.ID); });
                transform.DOLocalMoveY(400, 1);
                break;
        }



    }

    
    public async void SetSkillPopup(int index)
    {
        UIPOPUP game = await UIManager.Instance.ShowPopup("SkillInfoPopup");
        game.GetComponent<SkillInfoPoPUP>().SetPopup(GlobalDataTable.Instance.DataCarrier.GetCharacter(), index);
    }

}
