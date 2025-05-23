
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class SkillEffect : MonoBehaviour, ISkillEffectable
{


    private void Start()
    {
        StartCoroutine(StartEffect());
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Play(List<CharacterCarrier> list)
    {
        StartCoroutine(StartEffect());
        
    }


    public IEnumerator StartEffect()
    {
        yield return new WaitForSeconds(3f);
        Destroy();
    }



    
   

    
}
