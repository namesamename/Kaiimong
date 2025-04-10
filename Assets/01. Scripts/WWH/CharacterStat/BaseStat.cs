using UnityEngine;
public class BaseStat : MonoBehaviour
{

    public float Value; // ���� ������ ��
    public float ValueMultiples = 1f; //����

    public virtual void SetStat(float amount) //���� ���ϱ�
    {
        if (amount < 0) return;
        Value = amount;
    }

    public virtual void Init(float amount) //�ʱ�ȭ
    {
        SetStat(amount);
    }

    public virtual float GetStat() //��������
    {
        return Value * ValueMultiples;
    }

    public void AddStat(float amount) //���ϱ�, ����
    {
        SetStat(Mathf.Max(0, Value + amount));
    }

    public void SetStatMultiples(float amount) //���� ���ϱ�
    {
        if (amount < 0) return;
        ValueMultiples = amount;
    }

    public void AddMultiples(float amount)// ���� ���ϰų� ������
    {
        SetStatMultiples(Mathf.Max(0, ValueMultiples + amount));
    }

    public virtual string GetValueToString()
    {
        return (Value * ValueMultiples).ToString("F0");
    }

}
