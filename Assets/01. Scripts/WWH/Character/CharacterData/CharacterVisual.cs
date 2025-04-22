using UnityEngine;
using System.Collections;

public enum SpriteType
{
    Icon,
    BattleSprite,
    Illustration,
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
    public Sprite icon;
    public Sprite BattleSprite;


    public GameObject SelectEffect;
    public float AppearAnimationLength;

    public void Initialize()
    {
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();



        //icon = Resources.Load<Sprite>($"Icon/{GetComponentInParent<CharacterCarrier>().CharacterSaveData.ID}");
        //BattleSprite = Resources.Load<Sprite>($"BattleSprite/{GetComponentInParent<CharacterCarrier>().CharacterSaveData.ID}");
        //Illustration = Resources.Load<Sprite>($"Illustration/{GetComponentInParent<CharacterCarrier>().CharacterSaveData.ID}");


        // 애니메이션 길이 계산
        if (animator != null)
        {
            runtimeController = animator.runtimeAnimatorController;
            // 등장 애니메이션 길이 계산 (Appear 애니메이션 사용)
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            AppearAnimationLength = stateInfo.length;
            //Debug.Log(AppearAnimationLength);
            animationClips = Resources.LoadAll<AnimationClip>($"Character/Silhum");
            //StartCoroutine(PlayAni());
        }
    }

    public void SetSprite(SpriteType sprite)
    {
        switch (sprite)
        {
            case SpriteType.Icon:
                spriteRenderer.sprite = icon;
                break;
            case SpriteType.BattleSprite:
                spriteRenderer.sprite = BattleSprite;
                break;
            case SpriteType.Illustration:
                spriteRenderer.sprite = Illustration; 
                break;
        }

    }
    public Sprite GetBattleSprite()
    {
        return BattleSprite;
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public Sprite GetIllustration()
    {
        return Illustration;
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
