using UnityEngine;
public class BaseStat : MonoBehaviour
{

    public float Value; // 실제 스탯의 값
    public float ValueMultiples = 1f; //배율

    public virtual void SetStat(float amount) //스탯 정하기
    {
        if (amount < 0) return;
        Value = amount;
    }

    public virtual void Init(float amount) //초기화
    {
        SetStat(amount);
    }

    public virtual float GetStat() //가져오기
    {
        return Value * ValueMultiples;
    }

    public void AddStat(float amount) //더하기, 빼기
    {
        SetStat(Mathf.Max(0, Value + amount));
    }

    public void SetStatMultiples(float amount) //배율 정하기
    {
        if (amount < 0) return;
        ValueMultiples = amount;
    }

    public void AddMultiples(float amount)// 배율 곱하거나 나누기
    {
        SetStatMultiples(Mathf.Max(0, ValueMultiples + amount));
    }

    public virtual string GetValueToString()
    {
        return (Value * ValueMultiples).ToString("F0");
    }

}
