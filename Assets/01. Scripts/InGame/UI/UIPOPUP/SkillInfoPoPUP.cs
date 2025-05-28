using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillInfoPoPUP : UIPOPUP
{
   
    private Image[] images = new Image[4];
    private TextMeshProUGUI[] SkillContext = new TextMeshProUGUI[2];
    private Button BackGroundButton;


    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
        SkillContext = GetComponentsInChildren<TextMeshProUGUI>();
        BackGroundButton = GetComponentInChildren<Button>();
        PlayShowAnimation();
    }

    public async void SetPopup(Character character, int index)
    {
        int ID = character.ID;
        ActiveSkill ActSkill = GlobalDataTable.Instance.skill.GetActSkillSOToID(ID + index);
        SkillContext[0].text = ActSkill.Name;
        SkillContext[1].text = ActSkill.Description;
        BackGroundButton.onClick.AddListener(Destroy);
        images[2].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.SkillIcon, ActSkill.ID);
    }

    public async void SetPopupToSkillAsync(ActiveSkill ActSkill) 
    {
        SkillContext[0].text = ActSkill.Name;
        SkillContext[1].text = ActSkill.Description;
        BackGroundButton.onClick.AddListener(Destroy);
        images[2].sprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.SkillIcon, ActSkill.ID);
    }











}
