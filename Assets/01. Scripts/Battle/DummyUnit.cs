using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyUnit : MonoBehaviour
{
    public int Speed;

    private Animator animator;
    public Animator Animator {  get { return animator; } }
    
    // 애니메이션 길이를 저장하는 변수
    public float AppearAnimationLength { get; private set; }

    private void Awake()
    {       
       animator = GetComponent<Animator>();
       
       // 애니메이션 길이 계산
       if (animator != null)
       {
           // 등장 애니메이션 길이 계산 (기본 상태인 Idle 애니메이션 사용)
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
