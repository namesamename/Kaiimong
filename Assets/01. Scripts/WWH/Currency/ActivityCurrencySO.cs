using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ActivityCurrency", menuName = "Currency/Activity")]
public class ActivityCurrencySO : CurrencySO
{
    public int AutoRecoveryPerMinute;
    public int CurrentLevelMaxCount;

}
