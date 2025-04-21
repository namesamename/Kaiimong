using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LevelUPHUDType
{
    Effect,
    Stat,
    Max,
    Plus,
    Confirm,
    Minus,
    Min,
    LevelInfo
}



public class LevelupHUD : MonoBehaviour
{
    LevelUPHUDType levelUPHUD;

    public void Initailize()
    {
        switch (levelUPHUD)
        {
            case LevelUPHUDType.Effect:
                break;
            case LevelUPHUDType.Stat:
                break;
            case LevelUPHUDType.Max:
                break;
            case LevelUPHUDType.Plus:
                break;
            case LevelUPHUDType.Minus:
                break; 
            case LevelUPHUDType.Confirm:
                break;
            case LevelUPHUDType.Min:
                break;
            case LevelUPHUDType.LevelInfo:
                break;

        }



    }




}
