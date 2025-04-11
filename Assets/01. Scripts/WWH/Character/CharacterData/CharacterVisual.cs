using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    private Animator animator;
    public Animator Animator { get { return animator; } }
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    public Sprite icon;
    public GameObject SelectEffect;
    public float AppearAnimationLength { get; private set; }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // �ִϸ��̼� ���� ���
        if (animator != null)
        {
            // ���� �ִϸ��̼� ���� ��� (Appear �ִϸ��̼� ���)
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            AppearAnimationLength = stateInfo.length;
        }
    }


}
