using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private SpriteRenderer spriteRenderer;

    private RuntimeAnimatorController runtimeController;
    private AnimationClip[] animationClips = new AnimationClip[7];
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

    public Sprite icon;
    public GameObject SelectEffect;
    public float AppearAnimationLength { get; private set; }
    private void Awake()
    {


    }

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        runtimeController = animator.runtimeAnimatorController;

        // 애니메이션 길이 계산
        if (animator != null)
        {
            // 등장 애니메이션 길이 계산 (Appear 애니메이션 사용)
            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //AppearAnimationLength = stateInfo.length;

            //animationClips = Resources.LoadAll<AnimationClip>("Character/이름");
            //ChangeAnimation();
        }


    }


    public void ChangeAnimation()
    {
        AnimatorOverrideController controller = new AnimatorOverrideController(runtimeController);
        controller["Appear"] = animationClips[0];
        controller["Idle"] = animationClips[1];
        controller["FirstSkill"] = animationClips[2];
        controller["SecondSkill"] = animationClips[3];
        controller["ThridSkill"] = animationClips[4];
        controller["hit"] = animationClips[5];
        controller["Death"] = animationClips[6];

        animator.runtimeAnimatorController = controller;

    }


}
