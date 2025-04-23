using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseLevelupInfo : MonoBehaviour
{
   

    public LevelUpPopUP popUP;

    private void Awake()
    {
        popUP = GetComponentInParent<LevelUpPopUP>();
    }


   

}
