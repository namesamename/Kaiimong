using UnityEngine;

public class HealthStat : BaseStat
{
    public float CurHealth;


    public void SetHeathDefault()
    {
        CurHealth = Value;
    }

    public void Heal(float Amount)
    {
        CurHealth = Mathf.Min(CurHealth+Amount, Value);
    }
   
}
