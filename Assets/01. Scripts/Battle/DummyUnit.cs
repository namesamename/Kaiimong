using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DummyUnit : MonoBehaviour
{
    public bool IsSelectable = false;
    public int Speed;

    private Animator animator;
    public Animator Animator {  get { return animator; } }
    
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

    public Sprite icon;
    public GameObject SelectEffect;

    public List<SkillData> skillDatas;
    
    // 애니메이션 길이를 저장하는 변수
    public float AppearAnimationLength { get; private set; }

    private void Awake()
    {       
       animator = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
       
       // 애니메이션 길이 계산
       if (animator != null)
       {
           // 등장 애니메이션 길이 계산 (Appear 애니메이션 사용)
           AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
           AppearAnimationLength = stateInfo.length;
       }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

}
