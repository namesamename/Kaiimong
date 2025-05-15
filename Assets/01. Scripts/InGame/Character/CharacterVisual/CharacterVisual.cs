using UnityEngine;
using System.Collections;
using System;
public class CharacterVisual : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private SpriteRenderer spriteRenderer;
    public RuntimeAnimatorController RuntimeController;
    public AnimationClip[] AnimationClips = new AnimationClip[7];
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }


    public Sprite BattleSprite;
    public Sprite Icon;
    public Sprite RecoSD;


    public GameObject SelectEffect;
    public float AppearAnimationLength;

    public Action action;


    public async void Initialize(int ID, CharacterType character)
    {

        try
        {
            animator = GetComponentInParent<Animator>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            switch (character)
            {
                case CharacterType.Friend:
                    if (this == null || !gameObject.activeInHierarchy) return;
                    
                    // 스프라이트 로드 시도
                    BattleSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.BattleSD, ID);
                    
                    if (this == null || !gameObject.activeInHierarchy) return;
                    Icon = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.CharacterIcon, ID);
                    
                    if (this == null || !gameObject.activeInHierarchy) return;
                    RecoSD = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.RecognitionSD, ID);

                    // 기본 스프라이트 설정
                    if (this != null && gameObject.activeInHierarchy && spriteRenderer != null)
                    {
                        if (BattleSprite != null)
                        {
                            spriteRenderer.sprite = BattleSprite;
                        }
                        else if (Icon != null)
                        {
                            spriteRenderer.sprite = Icon;
                        }
                    }
                    break;

                case CharacterType.Enemy:
                    if (this == null || !gameObject.activeInHierarchy) return;
                    BattleSprite = await AddressableManager.Instance.LoadAsset<Sprite>(AddreassablesType.EnemyBattleSD, ID);
                    if (this != null && gameObject.activeInHierarchy && spriteRenderer != null && BattleSprite != null)
                    {
                        spriteRenderer.sprite = BattleSprite;
                    }
                    break;
            }

            if (this == null || !gameObject.activeInHierarchy) return;

            // 애니메이션 설정
            if (animator != null)
            {
                RuntimeController = animator.runtimeAnimatorController;
                AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
                AppearAnimationLength = stateInfo.length;
                AnimationClips = Resources.LoadAll<AnimationClip>($"Character/Silhum");
                if (AnimationClips == null || AnimationClips.Length == 0)
                {
                    Debug.LogError($"Failed to load animation clips for Character/Silhum");
                }
                else
                {
                    Debug.Log($"Successfully loaded {AnimationClips.Length} animation clips");
                    StartCoroutine(PlayAni());
                }
            }

            action?.Invoke();
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in CharacterVisual.Initialize: {e.Message}\nStack Trace: {e.StackTrace}");
        }
    }

    public void SetSprite(SpriteType sprite)
    {
        if (this == null || !gameObject.activeInHierarchy)
        {
            Debug.LogWarning("CharacterVisual is null or inactive. Cannot set sprite.");
            return;
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogWarning("SpriteRenderer is null in SetSprite");
                return;
            }
        }

        try
        {
            // 스프라이트가 로드되었는지 확인
            bool isSpriteLoaded = false;
            switch (sprite)
            {
                case SpriteType.Icon:
                    isSpriteLoaded = Icon != null;
                    break;
                case SpriteType.RecoSD:
                    isSpriteLoaded = RecoSD != null;
                    break;
                case SpriteType.BattleSprite:
                    isSpriteLoaded = BattleSprite != null;
                    break;
            }

            if (isSpriteLoaded)
            {
                SetSpritefun(sprite);
            }
            else
            {
                // 스프라이트가 아직 로드되지 않은 경우, 초기화 완료 후 설정하도록 action에 추가
                action += () => 
                {
                    if (this != null && gameObject.activeInHierarchy)
                    {
                        SetSpritefun(sprite);
                    }
                };
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error in CharacterVisual.SetSprite: {e.Message}");
        }
    }

    
    public float GetAnimationLength(int index)
    {
        Debug.Log(AnimationClips[index].length);
        return AnimationClips[index].length;
    }

    public void SetSpritefun(SpriteType sprite)
    {
        if (this == null || !gameObject.activeInHierarchy)
        {
            Debug.LogWarning("CharacterVisual is null or inactive. Cannot set sprite.");
            return;
        }

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is null in SetSpritefun");
            return;
        }

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
