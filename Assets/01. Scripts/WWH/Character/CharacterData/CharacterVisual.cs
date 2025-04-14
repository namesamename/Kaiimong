using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterVisual : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private SpriteRenderer spriteRenderer;

    public RuntimeAnimatorController runtimeController;
    public AnimationClip[] animationClips = new AnimationClip[7];
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

    public Sprite icon;
    public GameObject SelectEffect;
    public float AppearAnimationLength { get; private set; }

    public void Initialize()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
  

        // 애니메이션 길이 계산
        if (animator != null)
        {
            runtimeController = animator.runtimeAnimatorController;
            // 등장 애니메이션 길이 계산 (Appear 애니메이션 사용)
            //AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            //AppearAnimationLength = stateInfo.length;

            animationClips = Resources.LoadAll<AnimationClip>("Character/SilHum");
            StartCoroutine(PlayAni());
        }


    }


    public IEnumerator PlayAni()
    {
  
        yield return new WaitForSeconds(1f);
        ChangeAnimation();
        yield return new WaitForEndOfFrame();
        var clips = animator.runtimeAnimatorController.animationClips;
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
