using UnityEngine;
using System.Collections;

public enum SpriteType
{
    RecoSD,
    Recoilu,
    BattleSprite,
    Illustration,
    Icon,
}

public class CharacterVisual : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController runtimeController;
    public AnimationClip[] animationClips = new AnimationClip[7];
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

    public Sprite Illustration;
    public Sprite BattleSprite;
    public Sprite Icon;
    public Sprite RecoIlu;
    public Sprite RecoSD;


    public GameObject SelectEffect;
    public float AppearAnimationLength;


    private void Awake()
    {
        
    }

    public void Initialize(int ID , CharacterType character )
    {
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (character)
        {
            case CharacterType.Friend:
                //icon = Resources.Load<Sprite>(GlobalDatatable.instance.character.getid(ID).iconPath);
                //BattleSprite = Resources.Load<Sprite>(GlobalDatatable.instance.character.getid(ID).iconPath);
                //Illustration = Resources.Load<Sprite>(GlobalDatatable.instance.character.getid(ID).iconPath);
                // animationClips = Resources.LoadAll<AnimationClip>(GlobalDatatable.instance.character.getid(ID).iconPath);
                break;
            case CharacterType.Enemy:
                //icon = Resources.Load<Sprite>(GlobalDatatable.instance.character.getid(ID).iconPath);
                //BattleSprite = Resources.Load<Sprite>(GlobalDatatable.instance.character.getid(ID).iconPath);
                //Illustration = Resources.Load<Sprite>(GlobalDatatable.instance.character.getid(ID).iconPath);
                // animationClips = Resources.LoadAll<AnimationClip>(GlobalDatatable.instance.character.getid(ID).iconPath);
                break;
        }
        // 애니메이션 길이 계산
        if (animator != null)
        {
            runtimeController = animator.runtimeAnimatorController;
            // 등장 애니메이션 길이 계산 (Appear 애니메이션 사용)
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            AppearAnimationLength = stateInfo.length;
            //Debug.Log(AppearAnimationLength);
            //여기도 수정
            animationClips = Resources.LoadAll<AnimationClip>($"Character/Silhum");
            StartCoroutine(PlayAni());
        }
    }

    public float GetAnimationLength(int index)
    {
        return animationClips[index].length;
    }

    public void SetSprite(SpriteType sprite)
    {
        switch (sprite)
        {
            case SpriteType.Icon:
                spriteRenderer.sprite = Icon;
                break;
            case SpriteType.RecoSD:
                spriteRenderer.sprite = RecoSD;
                break;  
            case SpriteType.Recoilu:
                spriteRenderer.sprite = RecoIlu;
                break;
            case SpriteType.BattleSprite:
                spriteRenderer.sprite = BattleSprite;
                break;
            case SpriteType.Illustration:
                spriteRenderer.sprite = Illustration; 
                break;
        }

    }
    public Sprite Getincon()
    {
        if (BattleSprite != null)
        {
            return Icon;
        }
        else
        {
            return null;
        }

    }
    public Sprite GetBattleSprite()
    {
        if( BattleSprite != null)
        {
            return BattleSprite;
        }
        else
        {
            return null;
        }
        
    }
    public Sprite GetIRecoSD()
    {
        if( RecoSD != null)
        {
            return RecoSD;
        }
        else
        {
            return null;
        }
        
    }
    public Sprite GetIllustration()
    {
        if(Illustration != null)
        {
            return Illustration;
        }
        else
        {
            return null;
        }

    }
    public Sprite GetRecoIllustration()
    {
        if(RecoIlu != null)
        {
            return RecoIlu;
        }
        else
        {
           return  null;
        }
   
    }

    public IEnumerator PlayAni()
    {
        ChangeAnimation();
        yield return new WaitForEndOfFrame();
    }

    public void ChangeAnimation()
    {
        AnimatorOverrideController controller = new AnimatorOverrideController(runtimeController);
        controller["Appeared"] = animationClips[0];
        controller["Idle"] = animationClips[1];
        controller["FirstSkill"] = animationClips[2];
        controller["SecondSkill"] = animationClips[3];
        controller["ThirdSkill"] = animationClips[4];
        controller["Hit"] = animationClips[5];
        controller["Death"] = animationClips[6];
        animator.runtimeAnimatorController = controller;
    }


}
