using UnityEngine;
using System.Collections;
public class CharacterVisual : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController RuntimeController;
    public AnimationClip[] AnimationClips = new AnimationClip[7];
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }


    Sprite BattleSprite;
    Sprite Icon;
    Sprite RecoSD;


    public GameObject SelectEffect;
    public float AppearAnimationLength;

    public void Initialize(int ID , CharacterType character )
    {
        animator = GetComponentInParent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch (character)
        {
            case CharacterType.Friend:
                BattleSprite = GlobalDataTable.Instance.Sprite.GetSpriteToID(ID, SpriteType.BattleSprite);
                Icon = GlobalDataTable.Instance.Sprite.GetSpriteToID(ID, SpriteType.Icon);
                RecoSD = GlobalDataTable.Instance.Sprite.GetSpriteToID(ID, SpriteType.RecoSD);
                // animationClips = Resources.LoadAll<AnimationClip>(GlobalDatatable.instance.character.getid(ID).iconPath);
                break;
            case CharacterType.Enemy:

                // animationClips = Resources.LoadAll<AnimationClip>(GlobalDatatable.instance.character.getid(ID).iconPath);
                break;
        }
        // 애니메이션 길이 계산
        if (animator != null)
        {
            RuntimeController = animator.runtimeAnimatorController;
            // 등장 애니메이션 길이 계산 (Appear 애니메이션 사용)
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            AppearAnimationLength = stateInfo.length;
            //Debug.Log(AppearAnimationLength);
            //여기도 수정
            AnimationClips = Resources.LoadAll<AnimationClip>($"Character/Silhum");
            StartCoroutine(PlayAni());
        }
    }

    public float GetAnimationLength(int index)
    {
        return AnimationClips[index].length;
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
            case SpriteType.BattleSprite:
                spriteRenderer.sprite = BattleSprite;
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


    public IEnumerator PlayAni()
    {
        ChangeAnimation();
        yield return new WaitForEndOfFrame();
    }

    public void ChangeAnimation()
    {
        AnimatorOverrideController controller = new AnimatorOverrideController(RuntimeController);
        controller["Appeared"] = AnimationClips[0];
        controller["Idle"] = AnimationClips[1];
        controller["FirstSkill"] = AnimationClips[2];
        controller["SecondSkill"] = AnimationClips[3];
        controller["ThirdSkill"] = AnimationClips[4];
        controller["Hit"] = AnimationClips[5];
        controller["Death"] = AnimationClips[6];
        animator.runtimeAnimatorController = controller;
    }


}
